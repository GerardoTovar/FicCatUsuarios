using FicCatUsuarios.ViewModels.Base;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FicCatUsuarios
{
    public partial class App : Application
    {
        private static FicViewModelLocator FicLocalVmLocator;

        public static FicViewModelLocator FicVmLocator
        {
            get
            {
                return FicLocalVmLocator = FicLocalVmLocator ?? new FicViewModelLocator();
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new Views.Navegacion.FicMasterPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
