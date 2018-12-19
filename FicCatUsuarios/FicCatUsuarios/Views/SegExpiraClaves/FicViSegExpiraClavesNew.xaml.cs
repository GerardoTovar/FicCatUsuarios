using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FicCatUsuarios.ViewModels.SegExpiraClaves;
using FicCatUsuarios.Models.Usuarios;

namespace FicCatUsuarios.Views.SegExpiraClaves
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FicViSegExpiraClavesNew : ContentPage
    {
        private cat_usuarios FicLoParameter;
        public FicViSegExpiraClavesNew()
        {
            InitializeComponent();
            BindingContext = App.FicVmLocator.FicVmSegExpiraClavesNew;
        }


        public FicViSegExpiraClavesNew(cat_usuarios FicParameter)
        {
            InitializeComponent();
            FicLoParameter = FicParameter;
            BindingContext = App.FicVmLocator.FicVmSegExpiraClavesNew;
        }

        public void Handle_ValueChanged(object sender, Syncfusion.SfNumericUpDown.XForms.ValueEventArgs e)
        {
            //(BindingContext as FicVmRhCatPersonasNew).Persona.Prioridad = Int16.Parse(e.Value.ToString());
        }

        private void DatePicker_OnDataSelected(object sender, DateChangedEventArgs e)
        {
            var fecha = e.NewDate.ToString();
            (BindingContext as FicVmSegExpiraClavesNew).Edificio.FechaExpiraIni = DateTime.Parse(fecha);
        }
        private void DatePicker_OnDataSelected2(object sender, DateChangedEventArgs e)
        {
            var fecha = e.NewDate.ToString();
            (BindingContext as FicVmSegExpiraClavesNew).Edificio.FechaExpiraFin = DateTime.Parse(fecha);
        }


        protected override void OnAppearing()
        {
            var FicViewModel = BindingContext as FicVmSegExpiraClavesNew;
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