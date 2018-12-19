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
using FicCatUsuarios.Interfaces.SegUsuariosEstatus;

namespace FicCatUsuarios.Services.SegUsuariosEstatus
{
    public class FicSrvSegUsuariosEstatusDetail : IFicSrvSegUsuariosEstatusDetail
    {
        private static readonly IFicAsyncLock ficMutex = new IFicAsyncLock();
        private readonly FicDBContext FicLoBDContext;

        public FicSrvSegUsuariosEstatusDetail()
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
        }//CONSTRUCTOR


        public async Task FicMetGetDetailRhCatPersonas(seg_usuarios_estatus item)
        {

        }
    }
}
