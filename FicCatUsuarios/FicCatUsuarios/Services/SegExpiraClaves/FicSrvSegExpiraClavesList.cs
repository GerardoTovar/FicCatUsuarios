using FicCatUsuarios.Interfaces.SegExpiraClaves;
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
namespace FicCatUsuarios.Services.SegExpiraClaves
{
    public class FicSrvSegExpiraClavesList : IFicSrvSegExpiraClavesList
    {
        private readonly FicDBContext FicLoBDContext;

        public FicSrvSegExpiraClavesList()
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
        }//CONSTRUCTOR

        public async Task<IEnumerable<seg_expira_claves>> FicMetGetListSegExpiraClaves(int IdUsuario)
        {
            return await (from claves in FicLoBDContext.seg_expira_claves where claves.IdUsuario==IdUsuario select claves).AsNoTracking().ToListAsync();
        }
    }
}