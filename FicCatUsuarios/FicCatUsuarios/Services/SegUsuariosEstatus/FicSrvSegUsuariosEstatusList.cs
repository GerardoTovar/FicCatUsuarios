using FicCatUsuarios.Interfaces.SegUsuariosEstatus;
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
namespace FicCatUsuarios.Services.SegUsuariosEstatus
{
    public class FicSrvSegUsuariosEstatusList : IFicSrvSegUsuariosEstatusList
    {
        private readonly FicDBContext FicLoBDContext;

        public FicSrvSegUsuariosEstatusList()
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
        }//CONSTRUCTOR

        public async Task<IEnumerable<seg_usuarios_estatus>> FicMetGetListSegExpiraClaves(int IdUsuario)
        {
            var usrs = await (from usuarios in FicLoBDContext.seg_usuarios_estatus where usuarios.IdUsuario==IdUsuario select usuarios).AsNoTracking().ToListAsync();
            return usrs;
        }

        public async Task<IEnumerable<cat_estatus>> FicMetGetListRhCatEstatus(int id)
        {
            var estatus = await (from est in FicLoBDContext.cat_estatus where est.IdTipoEstatus == id select est).AsNoTracking().ToListAsync();
            return estatus;
        }

        public async Task<IEnumerable<cat_tipos_estatus>> FicMetGetListRhCatTipoEstatus(int id) {
            var tipo_estatus = await (from tipo_est in FicLoBDContext.cat_tipos_estatus where tipo_est.IdTipoEstatus == id select tipo_est).AsNoTracking().ToListAsync();
            return tipo_estatus;
        }
    }
}