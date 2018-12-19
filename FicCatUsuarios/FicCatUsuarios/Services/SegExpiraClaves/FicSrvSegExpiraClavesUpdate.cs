using FicCatUsuarios.Interfaces.SegExpiraClaves;
using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using FicCatUsuarios.Data;
using FicCatUsuarios.Interfaces.SQLite;
using FicCatUsuarios.Helpers;
using FicCatUsuarios.Models.Usuarios;

namespace FicCatUsuarios.Services.SegExpiraClaves
{
    public class FicSrvSegExpiraClavesUpdate : IFicSrvSegExpiraClavesUpdate
    {
        private FicDBContext FicLoBDContext;
        private static readonly IFicAsyncLock ficMutex = new IFicAsyncLock();
        public FicSrvSegExpiraClavesUpdate()
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
        }//CONSTRUCTOR

        public async Task FicMetUpdateEdificio(seg_expira_claves clave)
        {

            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());

            if (clave.Actual == "S")
            {
                var cls = await (from cla in FicLoBDContext.seg_expira_claves where cla.IdUsuario == clave.IdUsuario select cla).AsNoTracking().ToListAsync();
                var cls_ids = new int[cls.Count];
                int i = 0;
                foreach (seg_expira_claves c in cls)
                {
                    cls_ids[i] = c.IdClave;
                    i++;
                }

                using (await ficMutex.LockAsync().ConfigureAwait(false))
                {
                    var cls_list = FicLoBDContext.seg_expira_claves.Where(c => cls_ids.Contains(c.IdClave)).ToList();
                    var cls_list2 = FicLoBDContext.seg_expira_claves.Where(c => c.IdUsuario == clave.IdUsuario).ToList();
                    cls_list2.ForEach(a => a.Actual = "N");
                    FicLoBDContext.SaveChanges();
                }
            }
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());

            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                FicLoBDContext.Entry(clave).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                FicLoBDContext.SaveChanges();
            }
        }

    }
}
