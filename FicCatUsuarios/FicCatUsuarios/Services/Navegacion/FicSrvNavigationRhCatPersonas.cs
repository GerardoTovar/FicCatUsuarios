using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using FicCatUsuarios.Views.CatUsuarios;
using FicCatUsuarios.Interfaces.Navegacion;
using FicCatUsuarios.ViewModels.CatUsuarios;

using FicCatUsuarios.Views.SegExpiraClaves;
using FicCatUsuarios.ViewModels.SegExpiraClaves;

using FicCatUsuarios.Views.SegUsuariosEstatus;
using FicCatUsuarios.ViewModels.SegUsuariosEstatus;


namespace FicCatUsuarios.Services.Navegacion
{
    public class FicSrvNavigation : IFicSrvNavigation
    {
        private IDictionary<Type, Type> FicViewModelRouting = new Dictionary<Type, Type>()
        { 
            //AQUI SE HACE UNA UNION ENTRE LA VM Y VI DE CADA VIEW DE LA APP
            { typeof(FicVmCatUsuariosList),typeof(FicViCatUsuariosList) },
            { typeof(FicVmCatUsuariosNew),typeof(FicViCatUsuariosNew) },
            { typeof(FicVmCatUsuariosUpdate),typeof(FicViCatUsuariosUpdate) },
            { typeof(FicVmCatUsuariosDetail),typeof(FicViCatUsuariosDetail) },

            { typeof(FicVmSegExpiraClavesList),typeof(FicViSegExpiraClavesList) },
            { typeof(FicVmSegExpiraClavesNew),typeof(FicViSegExpiraClavesNew) },
            { typeof(FicVmSegExpiraClavesUpdate),typeof(FicViSegExpiraClavesUpdate) },
            { typeof(FicVmSegExpiraClavesDetail),typeof(FicViSegExpiraClavesDetail) },

            { typeof(FicVmSegUsuariosEstatusList),typeof(FicViSegUsuariosEstatusList) },
            { typeof(FicVmSegUsuariosEstatusNew),typeof(FicViSegUsuariosEstatusNew) },
            { typeof(FicVmSegUsuariosEstatusDetail),typeof(FicViSegUsuariosEstatusDetail) },
            
            //{ typeof(FicVmCatEdificiosUpdate),typeof(FicViCatEdificiosUpdate) },
            //{ typeof(FicVmCatEdificiosDetalle),typeof(FicViCatEdificiosDetalle) },
            //{ typeof(FicVmInventarioConteoList),typeof(FicViInventarioConteoList) },
            //{ typeof(FicVmInventarioConteosItem),typeof(FicViInventarioConteosItem) },
            //{ typeof(FicVmInventarioAcumuladoList),typeof(FicViInventarioAcumuladoList)},
            //{typeof(FicVmImportarWebApi), typeof(FicViImportarWebApi)},
           // {typeof(FicVmExportarWebApi), typeof(FicViExportarWebApi)}
        };

        #region METODOS DE IMPLEMENTACION DE LA INTERFACE -> IFicSrvNavigationInventario
        public void FicMetNavigateTo<FicTDestinationViewModel>(object FicNavigationContext = null)
        {
            Type FicPageType = FicViewModelRouting[typeof(FicTDestinationViewModel)];
            var FicPage = Activator.CreateInstance(FicPageType, FicNavigationContext) as Page;

            if (FicPage != null)
            {
                var mdp = Application.Current.MainPage as MasterDetailPage;
                mdp.Detail.Navigation.PushAsync(FicPage);
            }
        }
        public void FicMetNavigateTo<FicTDestinationViewModel>(object FicNavigationContext = null, object FicNavigationContext2 = null)
        {
            Type FicPageType = FicViewModelRouting[typeof(FicTDestinationViewModel)];
            var FicPage = Activator.CreateInstance(FicPageType, FicNavigationContext, FicNavigationContext2) as Page;

            if (FicPage != null)
            {
                var mdp = Application.Current.MainPage as MasterDetailPage;
                mdp.Detail.Navigation.PushAsync(FicPage);
            }
        }

        public void FicMetNavigateTo(Type FicDestinationType, object FicNavigationContext = null)
        {
            Type FicPageType = FicViewModelRouting[FicDestinationType];
            var FicPage = Activator.CreateInstance(FicPageType, FicNavigationContext) as Page;

            if (FicPage != null)
            {
                var mdp = Application.Current.MainPage as MasterDetailPage;
                mdp.Detail.Navigation.PushAsync(FicPage);
            }
        }

        public void FicMetNavigateBack()
        {
            Application.Current.MainPage = new NavigationPage();
        }
        #endregion

    }//CLASS
}//NAMESPACE
