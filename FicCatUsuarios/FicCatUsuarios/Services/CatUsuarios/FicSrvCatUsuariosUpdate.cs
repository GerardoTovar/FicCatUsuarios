using FicCatUsuarios.Interfaces.CatUsuarios;
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

namespace FicCatUsuarios.Services.CatUsuarios
{
    public class FicSrvCatUsuariosUpdate : IFicSrvCatUsuariosUpdate
    {
        private FicDBContext FicLoBDContext;
        private static readonly IFicAsyncLock ficMutex = new IFicAsyncLock();
        public FicSrvCatUsuariosUpdate()
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
        }//CONSTRUCTOR

        public async Task FicMetUpdateEdificio(cat_usuarios item)
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                FicLoBDContext.Entry(item).State = EntityState.Modified;
                FicLoBDContext.SaveChanges();
            }
        }

        public async Task<IEnumerable<rh_cat_personas>> FicMetGetListRhCatPersonas() {
            var personas = await (from per in FicLoBDContext.rh_cat_personas select per).AsNoTracking().ToListAsync();
            return personas;
        }

    }
}