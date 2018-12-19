using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FicCatUsuarios.ViewModels.CatUsuarios;

namespace FicCatUsuarios.Views.CatUsuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FicViCatUsuariosNew : ContentPage
    {
        private object FicLoParameter;
        public FicViCatUsuariosNew()
        {
            InitializeComponent();
            BindingContext = App.FicVmLocator.FicVmCatUsuariosNew;
        }


        public FicViCatUsuariosNew(object FicParameter)
        {
            InitializeComponent();
            FicLoParameter = FicParameter;
            BindingContext = App.FicVmLocator.FicVmCatUsuariosNew;
        }

        public void Handle_ValueChanged(object sender, Syncfusion.SfNumericUpDown.XForms.ValueEventArgs e)
        {
            //(BindingContext as FicVmRhCatPersonasNew).Persona.Prioridad = Int16.Parse(e.Value.ToString());
        }

        private void DatePicker_OnDataSelected(object sender, DateChangedEventArgs e)
        {
            
                                 
            (BindingContext as FicVmCatUsuariosNew).Edificio.FechaAlta = DateTime.Parse(e.NewDate.ToString());
        }
        



        protected override void OnAppearing()
        {
            var FicViewModel = BindingContext as FicVmCatUsuariosNew;
            if (FicViewModel != null)
            {
                FicViewModel.OnAppearing(FicLoParameter);
            }

        }//SE EJECUTA CUANDO SE ABRE LA VIEW I//CLASSE



        protected void FicMetNavigateBack(object sender, EventArgs e)
        {

            Application.Current.MainPage = new NavigationPage();
        }

    }
}