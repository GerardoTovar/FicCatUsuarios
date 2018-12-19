using FicCatUsuarios.Data;
using FicCatUsuarios.Helpers;
using FicCatUsuarios.Interfaces.SegExpiraClaves;
using FicCatUsuarios.Interfaces.SQLite;
using FicCatUsuarios.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace FicCatUsuarios.Services.SegExpiraClaves
{
    public class FicSrvSegExpiraClavesNew : IFicSrvSegExpiraClavesNew
    {
        private static readonly IFicAsyncLock ficMutex = new IFicAsyncLock();
        private  FicDBContext FicLoBDContext;

        public FicSrvSegExpiraClavesNew()
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
        }//CONSTRUCTOR

        public async Task FicMetGetNewRhCatTelefonos(seg_expira_claves clave)
        {
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
                    var cls_list = FicLoBDContext.seg_expira_claves.Where(c => cls_ids.Contains(c.IdClave) ).ToList();
                    var cls_list2 = FicLoBDContext.seg_expira_claves.Where(c => c.IdUsuario == clave.IdUsuario).ToList();
                    cls_list2.ForEach(a => a.Actual = "N");
                    FicLoBDContext.SaveChanges();
                }
            }                

            await FicMet();
            var claves = await (from cla in FicLoBDContext.seg_expira_claves where cla.IdUsuario==clave.IdUsuario select cla).AsNoTracking().ToListAsync();
            clave.IdClave = 1;
            if (claves.Count != 0)
            {
                var mx_id = claves.Max(x => x.IdClave);
                clave.IdClave += mx_id;
            }
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                FicLoBDContext.seg_expira_claves.Add(clave);
                FicLoBDContext.SaveChanges();
            }
        }
        public async Task FicMet()
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
        }
    }
}
