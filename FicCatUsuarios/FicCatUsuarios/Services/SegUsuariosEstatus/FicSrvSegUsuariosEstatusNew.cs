using FicCatUsuarios.Data;
using FicCatUsuarios.Helpers;
using FicCatUsuarios.Interfaces.SegUsuariosEstatus;
using FicCatUsuarios.Interfaces.SQLite;
using FicCatUsuarios.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using static FicCatUsuarios.Models.Usuarios.cat_estatus;
using Microsoft.EntityFrameworkCore;
namespace FicCatUsuarios.Services.SegUsuariosEstatus
{
    public class FicSrvSegUsuariosEstatusNew : IFicSrvSegUsuariosEstatusNew
    {
        private static readonly IFicAsyncLock ficMutex = new IFicAsyncLock();
        private  FicDBContext FicLoBDContext;

        public FicSrvSegUsuariosEstatusNew()
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
        }//CONSTRUCTOR

        public async Task FicMetGetNewRhCatTelefonos(seg_usuarios_estatus clave)
        {
            await FicMet();

            if (clave.Actual == "S")
            {               
                using (await ficMutex.LockAsync().ConfigureAwait(false))
                {
                    //var cls_list = FicLoBDContext.seg_usuarios_estatus.Where(c => cls_ids.Contains(c.IdClave)).ToList();
                    var cls_list2 = FicLoBDContext.seg_usuarios_estatus.Where(c => c.IdUsuario == clave.IdUsuario).ToList();
                    cls_list2.ForEach(a => a.Actual = "N");
                    FicLoBDContext.SaveChanges();
                }
            }
           
            await FicMet();
            var claves = await (from cla in FicLoBDContext.seg_usuarios_estatus where cla.IdUsuario == clave.IdUsuario select cla).AsNoTracking().ToListAsync();
            clave.IdCrtlEstatus = 1;
            if (claves.Count != 0)
            {
                var mx_id = claves.Max(x => x.IdCrtlEstatus);
                clave.IdCrtlEstatus += mx_id;
            }
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                FicLoBDContext.seg_usuarios_estatus.Add(clave);
                FicLoBDContext.SaveChanges();
            }
        }

        public async Task<IEnumerable<cat_estatus>> FicMetGetListRhCatEstatus(int id) {
            var estatus = await (from est in FicLoBDContext.cat_estatus where est.IdTipoEstatus==id select est).AsNoTracking().ToListAsync();
            return estatus;
        }
        public async Task FicMet()
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
        }
    }
}