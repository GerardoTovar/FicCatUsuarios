using FicCatUsuarios.Interfaces.Navegacion;
using FicCatUsuarios.Interfaces.SegExpiraClaves;
using FicCatUsuarios.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using  FicCatUsuarios.Models.Usuarios;

namespace FicCatUsuarios.ViewModels.SegExpiraClaves
{
    public class FicVmSegExpiraClavesDetail : FicViewModelBase
    {
        public ObservableCollection<seg_expira_claves> _FicSfDataGrid_ItemSource_CatAlumno;

        private IFicSrvNavigation FicLoSrvNavigation;
        private IFicSrvSegExpiraClavesDetail FicLoSrvApp;
        private IFicSrvNavigation IFicSrvNavigation;
        private IFicSrvSegExpiraClavesDetail IFicSrvCatUsuariosDetail;

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
        private seg_expira_claves _Persona;
        public seg_expira_claves Persona
        {
            get { return _Persona; }
            set
            {
                _Persona = value;
                RaisePropertyChanged();
            }
        }

        public FicVmSegExpiraClavesDetail(IFicSrvNavigation FicPaSrvNavigation, IFicSrvSegExpiraClavesDetail FicPaSrvApp)
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

            FicLoSrvNavigation.FicMetNavigateTo<FicVmSegExpiraClavesList>(usu);
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

        public void llenado(seg_expira_claves item)
        {
            Persona = new seg_expira_claves();
            Persona = item;

        }//SOBRECARGA AL METODO OnAppearing() DE LA VIEW 
        public void llenado2(cat_usuarios obj)
        {
            usu = new cat_usuarios();
            usu = obj;
        }


    }//CLASS }



}//NAMESPACE