using FicCatUsuarios.ViewModels.SegUsuariosEstatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FicCatUsuarios.Helpers;
using FicCatUsuarios.Views.SegUsuariosEstatus;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FicCatUsuarios.Data;
using FicCatUsuarios.Interfaces.SQLite;
using FicCatUsuarios.Models.Usuarios;

namespace FicCatUsuarios.Views.SegUsuariosEstatus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FicViSegUsuariosEstatusList : ContentPage
    {

        private static readonly IFicAsyncLock ficMutex = new IFicAsyncLock();
        private readonly FicDBContext FicLoDBContext;
        private readonly cat_usuarios persona;
        public FicViSegUsuariosEstatusList()
        {
            InitializeComponent();
            FicLoDBContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
            BindingContext = App.FicVmLocator.FicVmSegUsuariosEstatusList;
        }//CONSTRUCTOR

        public FicViSegUsuariosEstatusList(cat_usuarios FicNavigationContext)
        {
            InitializeComponent();
            FicLoDBContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
            persona = FicNavigationContext;
            BindingContext = App.FicVmLocator.FicVmSegUsuariosEstatusList;
        }//CONSTRUCTOR

        //Command="{Binding FicMetDeleteCommand}"
        protected async void FicMetDeleteCommand(object sender, EventArgs e)
        {
            var context = BindingContext as FicVmSegUsuariosEstatusList;
            if (context._SfDataGrid_SelectItem_Edificio == null)
            {
                await DisplayAlert("ATENCION", "NO SELECIONASTE NINGUN ITEM", "OK");
                return;
            }
            bool conf = await DisplayAlert("Cuidado", "¿Desea eliminar este elemento?", "Sí", "No");
            if (conf)
            {
                await FicMetRemoveEdificio(context._SfDataGrid_SelectItem_Edificio);
                context.FicSfDataGrid_ItemSource_CatEdificios.Remove(context._SfDataGrid_SelectItem_Edificio);
                var FicViewModel = BindingContext as FicVmSegUsuariosEstatusList;
                FicViewModel.NewEdExecute();
                //await FicMetRemoveEdificio(context._SfDataGrid_SelectItem_Edificio2);
                //(BindingContext as FicVmSegUsuariosEstatusList).FicSfDataGrid_ItemSource_CatEdificios2 = context.FicSfDataGrid_ItemSource_CatEdificios2;

            }

            dataGrid.View.Refresh();
        }

        public async Task FicMetRemoveEdificio(seg_usuarios_estatus item)
        {
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                FicLoDBContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                FicLoDBContext.SaveChanges();
            }
        }

        protected override void OnAppearing()
        {
            var FicViewModel = BindingContext as FicVmSegUsuariosEstatusList;
            if (FicViewModel != null)
            {
                FicViewModel.llenado(persona);
                FicViewModel.OnAppearing();
            }

        }//SE EJECUTA CUANDO SE ABRE LA VIEW I//CLASSE
    }//CLASSE

}//NAMESPACE