using FicCatUsuarios.Data;
using FicCatUsuarios.Helpers;
using FicCatUsuarios.Interfaces.CatUsuarios;
using FicCatUsuarios.Interfaces.SQLite;
using FicCatUsuarios.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

using Microsoft.EntityFrameworkCore;

namespace FicCatUsuarios.Services.CatUsuarios
{
    public class FicSrvCatUsuariosNew : IFicSrvCatUsuariosNew
    {
        private static readonly IFicAsyncLock ficMutex = new IFicAsyncLock();
        private  FicDBContext FicLoBDContext;

        public FicSrvCatUsuariosNew()
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
        }//CONSTRUCTOR

        public async Task FicMetGetNewRhCatTelefonos(cat_usuarios usuario)
        {
            await FicMet();
            var usuarios = await (from usu in FicLoBDContext.cat_usuarios select usu).AsNoTracking().ToListAsync();
            usuario.IdUsuario = 1;
            if (usuarios.Count != 0)
            {
                var mx_id = usuarios.Max(x => x.IdUsuario);
                usuario.IdUsuario += mx_id;
            }

            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                FicLoBDContext.cat_usuarios.Add(usuario);
                FicLoBDContext.SaveChanges();
            }
        }

        public async Task<IEnumerable<rh_cat_personas>> FicMetGetListRhCatPersonas() {
            var personas = await (from per in FicLoBDContext.rh_cat_personas select per).AsNoTracking().ToListAsync();
            return personas;
        }

        public async Task FicMet( )
        {
            FicLoBDContext =  new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
        }
    }
}
