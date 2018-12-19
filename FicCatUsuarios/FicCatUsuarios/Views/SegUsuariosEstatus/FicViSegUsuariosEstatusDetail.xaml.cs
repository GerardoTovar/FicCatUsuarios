using FicCatUsuarios.ViewModels.SegUsuariosEstatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using  FicCatUsuarios.Models.Usuarios;

namespace FicCatUsuarios.Views.SegUsuariosEstatus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FicViSegUsuariosEstatusDetail : ContentPage
    {
        private cat_usuarios FicLoParameter2;
        private seg_usuarios_estatus2 FicLoParameter;
        public FicViSegUsuariosEstatusDetail()
        {
            InitializeComponent();
            BindingContext = App.FicVmLocator.FicVmCatUsuariosDetail;
        }


        public FicViSegUsuariosEstatusDetail(seg_usuarios_estatus2 FicParameter)
        {
            InitializeComponent();
            FicLoParameter = FicParameter;
            BindingContext = App.FicVmLocator.FicVmSegUsuariosEstatusDetail;
        }

        public FicViSegUsuariosEstatusDetail(seg_usuarios_estatus2 FicParameter, cat_usuarios usu)
        {
            InitializeComponent();
            FicLoParameter = FicParameter;
            FicLoParameter2 = usu;
            BindingContext = App.FicVmLocator.FicVmSegUsuariosEstatusDetail;
        }



        protected override void OnAppearing()
        {
            var FicViewModel = BindingContext as FicVmSegUsuariosEstatusDetail;
            if (FicViewModel != null)
            {

                FicViewModel.OnAppearing(FicLoParameter);
                FicViewModel.llenado(FicLoParameter);
                FicViewModel.llenado2(FicLoParameter2);
            }

        }//SE EJECUTA CUANDO SE ABRE LA VIEW I//CLASSE



        protected void FicMetNavigateBack(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage();
        }

    }
}