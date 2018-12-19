using FicCatUsuarios.ViewModels.SegExpiraClaves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FicCatUsuarios.Helpers;
using FicCatUsuarios.Views.SegExpiraClaves;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FicCatUsuarios.Data;
using FicCatUsuarios.Interfaces.SQLite;
using FicCatUsuarios.Models.Usuarios;

namespace FicCatUsuarios.Views.SegExpiraClaves
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FicViSegExpiraClavesList : ContentPage
    {

        private static readonly IFicAsyncLock ficMutex = new IFicAsyncLock();
        private readonly FicDBContext FicLoDBContext;
        private readonly cat_usuarios persona;
        public FicViSegExpiraClavesList()
        {
            InitializeComponent();
            FicLoDBContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
            BindingContext = App.FicVmLocator.FicVmSegExpiraClavesList;
        }//CONSTRUCTOR

        public FicViSegExpiraClavesList(cat_usuarios FicNavigationContext)
        {
            InitializeComponent();
            FicLoDBContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDataBasePath());
            persona = FicNavigationContext;
            BindingContext = App.FicVmLocator.FicVmSegExpiraClavesList;
        }//CONSTRUCTOR

        //Command="{Binding FicMetDeleteCommand}"
        protected async void FicMetDeleteCommand(object sender, EventArgs e)
        {
            var context = BindingContext as FicVmSegExpiraClavesList;
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
            }

            dataGrid.View.Refresh();
        }

        public async Task FicMetRemoveEdificio(seg_expira_claves item)
        {
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                FicLoDBContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                FicLoDBContext.SaveChanges();
            }
        }

        protected override void OnAppearing()
        {
            var FicViewModel = BindingContext as FicVmSegExpiraClavesList;
            if (FicViewModel != null)
            {
                FicViewModel.llenado(persona);
                FicViewModel.OnAppearing();
            }

        }//SE EJECUTA CUANDO SE ABRE LA VIEW I//CLASSE
    }//CLASSE

}//NAMESPACE