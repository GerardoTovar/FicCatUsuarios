using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FicCatUsuarios.Interfaces.SQLite;
using FicCatUsuarios.UWP.SQLite;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(FicConfigSQLiteUWP))]  
namespace FicCatUsuarios.UWP.SQLite
{
    public class FicConfigSQLiteUWP :IFicConfigSQLite
    {
        public string FicGetDataBasePath()
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, FicAppSettings.FicDataBaseName);
        }//TRAE LA RUTA FISICA DONDE SE GUARDA LA BASE DE DATOS DE UWP
    }//CLASS
}//NAMESPACE

