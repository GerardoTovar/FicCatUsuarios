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
using FicCatUsuarios.Interfaces.SegExpiraClaves;

namespace FicCatUsuarios.Services.SegExpiraClaves
{
    public class FicSrvSegExpiraClavesDetail : IFicSrvSegExpiraClavesDetail
    {
        private static readonly IFicAsyncLock ficMutex = new IFicAsyncLock();
        private readonly FicDBContext FicLoBDContext;

        public FicSrvSegExpiraClavesDetail()
        {
            FicLoBDContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
        }//CONSTRUCTOR       
    }
}
