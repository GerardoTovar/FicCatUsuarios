using FicCatUsuarios.Interfaces.CatUsuarios;
using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using FicCatUsuarios.Data;
using FicCatUsuarios.Models.Usuarios;
using FicCatUsuarios.Interfaces.SQLite;
using FicCatUsuarios.Helpers;
namespace FicCatUsuarios.Services.CatUsuarios
{
    public class FicSrvCatUsuariosList : IFicSrvCatUsuariosList
    {
        private readonly FicDBContext FicLoBDContext;

        public FicSrvCatUsuariosList()
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
        }//CONSTRUCTOR

        public async Task<IEnumerable<cat_usuarios>> FicMetGetListCatUsuarios()
        {
            return await (from inv in FicLoBDContext.cat_usuarios select inv).AsNoTracking().ToListAsync();
        }
    }
}
