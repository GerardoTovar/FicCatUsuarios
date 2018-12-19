using FicCatUsuarios.ViewModels.CatUsuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using  FicCatUsuarios.Models.Usuarios;

namespace FicCatUsuarios.Views.CatUsuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FicViCatUsuariosDetail : ContentPage
    {
        private cat_usuarios persona;
        private rh_cat_personas FicLoParameter2;
        public FicViCatUsuariosDetail()
        {
            InitializeComponent();
            BindingContext = App.FicVmLocator.FicVmCatUsuariosDetail;
        }


        public FicViCatUsuariosDetail(cat_usuarios FicParameter)
        {
            InitializeComponent();
            persona = FicParameter;
            BindingContext = App.FicVmLocator.FicVmCatUsuariosDetail;
        }

        public FicViCatUsuariosDetail(cat_usuarios FicParameter,rh_cat_personas usu )
        {
            InitializeComponent();
            persona = FicParameter;
            FicLoParameter2 = usu;
            BindingContext = App.FicVmLocator.FicVmSegExpiraClavesUpdate;
        }


        protected override void OnAppearing()
        {
            var FicViewModel = BindingContext as FicVmCatUsuariosDetail;
            if (FicViewModel != null)
            {
                FicViewModel.llenado(persona);
                FicViewModel.OnAppearing();
            }

        }//SE EJECUTA CUANDO SE ABRE LA VIEW I//CLASSE



        protected void FicMetNavigateBack(object sender, EventArgs e)
        {

            Application.Current.MainPage = new NavigationPage();
        }

    }
}