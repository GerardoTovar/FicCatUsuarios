using FicCatUsuarios.Interfaces.CatUsuarios;
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
using FicCatUsuarios.ViewModels.SegExpiraClaves;
using FicCatUsuarios.ViewModels.SegUsuariosEstatus;

namespace FicCatUsuarios.ViewModels.CatUsuarios
{
    public class FicVmCatUsuariosList : INotifyPropertyChanged
    {
        public ObservableCollection<cat_usuarios> _FicSfDataGrid_ItemSource_CatEdificios;
        public cat_usuarios _FicSfDataGrid_SelectItem_CatEdificios;        
        private IFicSrvNavigation IFicSrvNavigation;
        private IFicSrvCatUsuariosList IFicSrvCatUsuariosList;


        public FicVmCatUsuariosList(IFicSrvNavigation IFicSrvNavigation, IFicSrvCatUsuariosList IFicSrvCatUsuariosList)
        {
            this.IFicSrvNavigation = IFicSrvNavigation; this.IFicSrvCatUsuariosList = IFicSrvCatUsuariosList;
            _FicSfDataGrid_ItemSource_CatEdificios = new ObservableCollection<cat_usuarios>();

        }//CONSTRUCTOR

        public ObservableCollection<cat_usuarios> FicSfDataGrid_ItemSource_CatEdificios
        {
            get
            {
                return _FicSfDataGrid_ItemSource_CatEdificios;
            }
        }//ESTE APUNTA ATRAVEZ DEL BindingContext AL GRID DE LA VIEW
        public cat_usuarios _SfDataGrid_SelectItem_Edificio;

        public cat_usuarios FicSfDataGrid_SelectItem_CatEdificios
        {
            get { return _SfDataGrid_SelectItem_Edificio; }
            set
            {
                _SfDataGrid_SelectItem_Edificio = value;
                RaisePropertyChanged();
            }
        }//ESTE APUNTA A UN ITEM SELECCIONADO EN EL GRID DE LA VIEW

        public cat_usuarios SfDataGrid_SelectItem_Edificio
        {
            get { return _SfDataGrid_SelectItem_Edificio; }
            set
            {
                _SfDataGrid_SelectItem_Edificio = value;
                RaisePropertyChanged();
            }
        }

        private ICommand ListEstatus;
        public ICommand FicMetListEstatusCommand
        {

            get { return ListEstatus = ListEstatus ?? new FicVmDelegateCommand(ListEstatusExecute); }
        }
        private void ListEstatusExecute()
        {
            if (_SfDataGrid_SelectItem_Edificio == null)
            {
                new Page().DisplayAlert("ATENCION", "NO SELECIONASTE NINGUN ITEM", "OK");
                return;
            }
            else
            {
                IFicSrvNavigation.FicMetNavigateTo<FicVmSegUsuariosEstatusList>(_SfDataGrid_SelectItem_Edificio);
            }

        }

        private ICommand Listcontraseñas;
        public ICommand FicMetListcopntraseñasCommand
        {

            get { return Listcontraseñas = Listcontraseñas ?? new FicVmDelegateCommand(ListcontraseñasExecute); }
        }
        private void ListcontraseñasExecute()
        {
            if (_SfDataGrid_SelectItem_Edificio == null)
            {
                new Page().DisplayAlert("ATENCION", "NO SELECIONASTE NINGUN ITEM", "OK");
                return;
            }
            else
            {
                IFicSrvNavigation.FicMetNavigateTo<FicVmSegExpiraClavesList>(_SfDataGrid_SelectItem_Edificio);
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
                IFicSrvNavigation.FicMetNavigateTo<FicVmCatUsuariosUpdate>(_SfDataGrid_SelectItem_Edificio);
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
                IFicSrvNavigation.FicMetNavigateTo<FicVmCatUsuariosDetail>(_SfDataGrid_SelectItem_Edificio);
            }

        }

        private ICommand NewEdificio;
        public ICommand FicMetNewCommand
        {
            get { return NewEdificio = NewEdificio ?? new FicVmDelegateCommand(NewEdificioExecute); }
        }
        private void NewEdificioExecute()
        {

           IFicSrvNavigation.FicMetNavigateTo<FicVmCatUsuariosNew>(null);
        }


        public async void OnAppearing()
        {
            try
            {
                var source_local_inv = await IFicSrvCatUsuariosList.FicMetGetListCatUsuarios();
                if (source_local_inv != null && _FicSfDataGrid_ItemSource_CatEdificios.Count == 0)
                {
                    foreach (cat_usuarios inv in source_local_inv)
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