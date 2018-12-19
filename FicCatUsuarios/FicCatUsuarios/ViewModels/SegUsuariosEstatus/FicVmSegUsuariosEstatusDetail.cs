using FicCatUsuarios.Interfaces.Navegacion;
using FicCatUsuarios.Interfaces.SegUsuariosEstatus;
using FicCatUsuarios.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using  FicCatUsuarios.Models.Usuarios;

namespace FicCatUsuarios.ViewModels.SegUsuariosEstatus
{
    public class FicVmSegUsuariosEstatusDetail : FicViewModelBase
    {        
        private IFicSrvNavigation FicLoSrvNavigation;
        private IFicSrvSegUsuariosEstatusDetail FicLoSrvApp;        

        private cat_usuarios _usu;
        public cat_usuarios usu
        {
            get { return _usu; }
            set
            {
                _usu = value;
                RaisePropertyChanged();
            }
        }
        private seg_usuarios_estatus2 _Persona;
        public seg_usuarios_estatus2 Persona
        {
            get { return _Persona; }
            set
            {
                _Persona = value;
                RaisePropertyChanged();
            }
        }

        public FicVmSegUsuariosEstatusDetail(IFicSrvNavigation FicPaSrvNavigation, IFicSrvSegUsuariosEstatusDetail FicPaSrvApp)
        {
            FicLoSrvNavigation = FicPaSrvNavigation;
            FicLoSrvApp = FicPaSrvApp;
        }

        private ICommand BackNavigation;
        public ICommand BackNavgCommand
        {
            get { return BackNavigation = BackNavigation ?? new FicVmDelegateCommand(BackNavgExecute); }
        }
        private void BackNavgExecute()
        {

            FicLoSrvNavigation.FicMetNavigateTo<FicVmSegUsuariosEstatusList>(usu);
        }



        public async void OnAppearing()
        {

            try
            {

            }
            catch (Exception e)
            {
                await new Page().DisplayAlert("ALERTA", e.Message.ToString(), "OK");
            }

        }//SOBRECARGA AL METODO OnAppearing() DE LA VIEW 

        public void llenado(seg_usuarios_estatus2 item)
        {
            Persona = new seg_usuarios_estatus2();
            Persona = item;

        }//SOBRECARGA AL METODO OnAppearing() DE LA VIEW 
        public void llenado2(cat_usuarios obj)
        {
            usu = new cat_usuarios();
            usu = obj;
        }


    }//CLASS }



}//NAMESPACE