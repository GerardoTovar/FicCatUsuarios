using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FicCatUsuarios.ViewModels.SegUsuariosEstatus;
using FicCatUsuarios.Models.Usuarios;

namespace FicCatUsuarios.Views.SegUsuariosEstatus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FicViSegUsuariosEstatusNew : ContentPage
    {
        private cat_usuarios FicLoParameter;
        public FicViSegUsuariosEstatusNew()
        {
            InitializeComponent();
            BindingContext = App.FicVmLocator.FicVmSegUsuariosEstatusNew;
        }


        public FicViSegUsuariosEstatusNew(cat_usuarios FicParameter)
        {
            InitializeComponent();
            FicLoParameter = FicParameter;
            BindingContext = App.FicVmLocator.FicVmSegUsuariosEstatusNew;
        }

        public void Handle_ValueChanged(object sender, Syncfusion.SfNumericUpDown.XForms.ValueEventArgs e)
        {
            //(BindingContext as FicVmRhCatPersonasNew).Persona.Prioridad = Int16.Parse(e.Value.ToString());
        }

        private void DatePicker_OnDataSelected(object sender, DateChangedEventArgs e)
        {
            var fecha = e.NewDate.ToString();
            (BindingContext as FicVmSegUsuariosEstatusNew).Edificio.FechaEstatus = DateTime.Parse(fecha);
        }


        protected override void OnAppearing()
        {
            var FicViewModel = BindingContext as FicVmSegUsuariosEstatusNew;
            if (FicViewModel != null)
            {
                FicViewModel.llenado(FicLoParameter);
                FicViewModel.OnAppearing(FicLoParameter);
            }

        }//SE EJECUTA CUANDO SE ABRE LA VIEW I//CLASSE



        protected void FicMetNavigateBack(object sender, EventArgs e)
        {

            Application.Current.MainPage = new NavigationPage();
        }

    }
}