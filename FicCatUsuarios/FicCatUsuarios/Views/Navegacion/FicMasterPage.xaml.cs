
using FicCatUsuarios.Views.CatUsuarios;
using System;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FicCatUsuarios.Views.Navegacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FicMasterPage : MasterDetailPage
    {
        public FicMasterPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }//CONSTRUCTOR

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var FicItemMenu = e.SelectedItem as FicMasterPageMenuItem;
                if (FicItemMenu == null)
                    return;

                var FicPagina = FicItemMenu.FicPageName as string;
                switch (FicPagina)
                {
                    case "FicViCatUIsuariosList":
                        FicItemMenu.TargetType = typeof(FicViCatUsuariosList);
                        break;
                    /*case "FicViRhCatEmpleadosList":
                        FicItemMenu.TargetType = typeof(FicViRhCatEmpleadosList);
                        break;
                    case "FicViRhCatatedraticosList":
                        FicItemMenu.TargetType = typeof(FicViRhCatatedraticosList);
                        break;*/
                    //case "FicViExportarWebApi":
                    //    FicItemMenu.TargetType = typeof(FicViExportarWebApi);
                    //    break;
                    default:
                        break;
                }

                object[] FicObjeto = new object[1];
                //FIC: Sin enviar parametro
                var FicPageOpen = (Page)Activator.CreateInstance(FicItemMenu.TargetType);
                //var FicPageOpen = Activator.CreateInstance(typeof(FicViInventarioList)) as Page;

                //FIC: Enviando como parametro un objeto vacio
                FicPageOpen.Title = FicItemMenu.Title;

                Detail = new NavigationPage(FicPageOpen);
                IsPresented = false;
                MasterPage.ListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                new Page().DisplayAlert("ERROR", ex.Message.ToString(), "OK");
            }
        }//AL SELECCIONAR UN ITEM DE DE LA LISTA

    }//CLASS
}//NAMESPACE