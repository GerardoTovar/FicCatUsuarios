using FicCatUsuarios.Models.Usuarios;
using FicCatUsuarios.ViewModels.CatUsuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FicCatUsuarios.Views.CatUsuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FicViCatUsuariosUpdate : ContentPage
    {
        private cat_usuarios FicLoParameter;
        public FicViCatUsuariosUpdate()
        {
            InitializeComponent();
            BindingContext = App.FicVmLocator.FicVmCatUsuariosUpdate;
        }


        public FicViCatUsuariosUpdate(cat_usuarios FicParameter)
        {
            InitializeComponent();
            FicLoParameter = FicParameter;
            BindingContext = App.FicVmLocator.FicVmCatUsuariosUpdate;
        }

        public void Handle_ValueChanged(object sender, Syncfusion.SfNumericUpDown.XForms.ValueEventArgs e)
        {
            //(BindingContext as FicVmRhCatPersonasNew).Persona.Prioridad = Int16.Parse(e.Value.ToString());
        }


        protected override void OnAppearing()
        {
            var FicViewModel = BindingContext as FicVmCatUsuariosUpdate;
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