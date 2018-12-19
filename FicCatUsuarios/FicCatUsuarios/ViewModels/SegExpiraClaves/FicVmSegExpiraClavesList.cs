using FicCatUsuarios.Interfaces.SegExpiraClaves;
using FicCatUsuarios.Interfaces.Navegacion;
using FicCatUsuarios.Models.Usuarios;
using FicCatUsuarios.ViewModels.Base;

using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using FicCatUsuarios.ViewModels.CatUsuarios;

namespace FicCatUsuarios.ViewModels.SegExpiraClaves
{
    public class FicVmSegExpiraClavesList : INotifyPropertyChanged
    {
        public ObservableCollection<seg_expira_claves> _FicSfDataGrid_ItemSource_CatEdificios;
        public seg_expira_claves _FicSfDataGrid_SelectItem_CatEdificios;
        //private ICommand _FicMetAddEdificioICommand, _FicMetAcumuladosICommand;
        private IFicSrvNavigation IFicSrvNavigation;
        private IFicSrvSegExpiraClavesList IFicSrvSegExpiraClavesList;
        public cat_usuarios _Edificios;
        public cat_usuarios Edificio
        {
            get { return _Edificios; }
            set
            {
                _Edificios = value;
                RaisePropertyChanged();
            }
        }

        public FicVmSegExpiraClavesList(IFicSrvNavigation IFicSrvNavigation, IFicSrvSegExpiraClavesList IFicSrvSegExpiraClavesList)
        {
            this.IFicSrvNavigation = IFicSrvNavigation; this.IFicSrvSegExpiraClavesList = IFicSrvSegExpiraClavesList;
            _FicSfDataGrid_ItemSource_CatEdificios = new ObservableCollection<seg_expira_claves>();

        }//CONSTRUCTOR

        public ObservableCollection<seg_expira_claves> FicSfDataGrid_ItemSource_CatEdificios
        {
            get
            {
                return _FicSfDataGrid_ItemSource_CatEdificios;
            }
        }//ESTE APUNTA ATRAVEZ DEL BindingContext AL GRID DE LA VIEW
        public seg_expira_claves _SfDataGrid_SelectItem_Edificio;

        public seg_expira_claves FicSfDataGrid_SelectItem_CatEdificios
        {
            get { return _SfDataGrid_SelectItem_Edificio; }
            set
            {
                _SfDataGrid_SelectItem_Edificio = value;
                RaisePropertyChanged();
            }
        }//ESTE APUNTA A UN ITEM SELECCIONADO EN EL GRID DE LA VIEW

        public seg_expira_claves SfDataGrid_SelectItem_Edificio
        {
            get { return _SfDataGrid_SelectItem_Edificio; }
            set
            {
                _SfDataGrid_SelectItem_Edificio = value;
                RaisePropertyChanged();
            }
        }

        private ICommand UpdateEdificio;
        public ICommand FicMetUpdateCommand
        {

            get { return UpdateEdificio = UpdateEdificio ?? new FicVmDelegateCommand(UpdateEdificioExecute); }
        }
        private void UpdateEdificioExecute()
        {
            if (_SfDataGrid_SelectItem_Edificio == null)
            {
                new Page().DisplayAlert("ATENCION", "NO SELECIONASTE NINGUN ITEM", "OK");
                return;
            }
            else
            {
                IFicSrvNavigation.FicMetNavigateTo<FicVmSegExpiraClavesUpdate>(_SfDataGrid_SelectItem_Edificio, Edificio);
            }

        }


        private ICommand DetalleEdificio;
        public ICommand FicMetDetalleCommand
        {

            get { return DetalleEdificio = DetalleEdificio ?? new FicVmDelegateCommand(DetalleEdificioExecute); }
        }
        private void DetalleEdificioExecute()
        {
            if (_SfDataGrid_SelectItem_Edificio == null)
            {
                new Page().DisplayAlert("ATENCION", "NO SELECIONASTE NINGUN ITEM", "OK");
                return;
            }
            else
            {
                IFicSrvNavigation.FicMetNavigateTo<FicVmSegExpiraClavesDetail>(_SfDataGrid_SelectItem_Edificio, Edificio);
            }

        }

        private ICommand NewEdificio;
        public ICommand FicMetNewCommand
        {
            get { return NewEdificio = NewEdificio ?? new FicVmDelegateCommand(NewEdificioExecute); }
        }
        private void NewEdificioExecute()
        {

            IFicSrvNavigation.FicMetNavigateTo<FicVmSegExpiraClavesNew>(Edificio);
        }
        private ICommand BackNavigation;
        public ICommand BackNavgCommand
        {
            get { return BackNavigation = BackNavigation ?? new FicVmDelegateCommand(BackNavgExecute); }
        }


        private void BackNavgExecute()
        {
            //FicLoSrvNavigation.FicMetNavigateBack();
            IFicSrvNavigation.FicMetNavigateTo<FicVmCatUsuariosList>(null);
        }


        public async void OnAppearing()
        {
            try
            {
                var source_local_inv = await IFicSrvSegExpiraClavesList.FicMetGetListSegExpiraClaves(Edificio.IdUsuario);
                if (source_local_inv != null && _FicSfDataGrid_ItemSource_CatEdificios.Count == 0)
                {
                    foreach (seg_expira_claves inv in source_local_inv)
                    {
                        _FicSfDataGrid_ItemSource_CatEdificios.Add(inv);
                    }
                }//LLENAR EL GRID
            }
            catch (Exception e)
            {
                await new Page().DisplayAlert("ALERTA", e.Message.ToString(), "OK");
            }
        }//SOBRE CARGA AL METODO OnAppearing() DE LA VIEW 
        public void llenado(cat_usuarios obj)
        {
            Edificio = new cat_usuarios();
            Edificio = obj;
        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }//CLASS 
}//NAMESPACE
