using FicCatUsuarios.ViewModels.SegExpiraClaves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using  FicCatUsuarios.Models.Usuarios;

namespace FicCatUsuarios.Views.SegExpiraClaves
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FicViSegExpiraClavesDetail : ContentPage
    {
        private cat_usuarios FicLoParameter2;
        private seg_expira_claves FicLoParameter;
        public FicViSegExpiraClavesDetail()
        {
            InitializeComponent();
            BindingContext = App.FicVmLocator.FicVmSegExpiraClavesDetail;
        }


        public FicViSegExpiraClavesDetail(seg_expira_claves FicParameter)
        {
            InitializeComponent();
            FicLoParameter = FicParameter;
            BindingContext = App.FicVmLocator.FicVmSegExpiraClavesDetail;
        }

        public FicViSegExpiraClavesDetail(seg_expira_claves FicParameter, cat_usuarios usu)
        {
            InitializeComponent();
            FicLoParameter = FicParameter;
            FicLoParameter2 = usu;
            BindingContext = App.FicVmLocator.FicVmSegExpiraClavesDetail;
        }



        protected override void OnAppearing()
        {
            var FicViewModel = BindingContext as FicVmSegExpiraClavesDetail;
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