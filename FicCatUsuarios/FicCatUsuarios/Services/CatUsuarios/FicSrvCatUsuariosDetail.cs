using FicCatUsuarios.Data;
using FicCatUsuarios.Helpers;
using FicCatUsuarios.Interfaces.SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using  FicCatUsuarios.Models.Usuarios;
using Microsoft.EntityFrameworkCore;
using FicCatUsuarios.Interfaces.CatUsuarios;
using System.Linq;

namespace FicCatUsuarios.Services.CatUsuarios
{
    public class FicSrvCatUsuariosDetail : IFicSrvCatUsuariosDetail
    {
        private static readonly IFicAsyncLock ficMutex = new IFicAsyncLock();
        private readonly FicDBContext FicLoBDContext;

        public FicSrvCatUsuariosDetail()
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
        }//CONSTRUCTOR
        
        public async Task<IEnumerable<rh_cat_personas>> FicMetGetListRhCatPersonas()
        {
            var personas = await (from per in FicLoBDContext.rh_cat_personas select per).AsNoTracking().ToListAsync();
            return personas;
        }
    }
}
