using FicCatUsuarios.Interfaces.SegUsuariosEstatus;
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

namespace FicCatUsuarios.ViewModels.SegUsuariosEstatus
{
    public class FicVmSegUsuariosEstatusList : INotifyPropertyChanged
    {
        public ObservableCollection<seg_usuarios_estatus> _FicSfDataGrid_ItemSource_CatEdificios;
        public ObservableCollection<seg_usuarios_estatus2> _FicSfDataGrid_ItemSource_CatEdificios2;
        public seg_usuarios_estatus _FicSfDataGrid_SelectItem_CatEdificios;
        //private ICommand _FicMetAddEdificioICommand, _FicMetAcumuladosICommand;
        private IFicSrvNavigation IFicSrvNavigation;
        private IFicSrvSegUsuariosEstatusList IFicSrvSegUsuariosEstatusList;        
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

        public FicVmSegUsuariosEstatusList(IFicSrvNavigation IFicSrvNavigation, IFicSrvSegUsuariosEstatusList IFicSrvSegUsuariosEstatusList)
        {
            this.IFicSrvNavigation = IFicSrvNavigation; this.IFicSrvSegUsuariosEstatusList = IFicSrvSegUsuariosEstatusList;
            _FicSfDataGrid_ItemSource_CatEdificios = new ObservableCollection<seg_usuarios_estatus>();
            _FicSfDataGrid_ItemSource_CatEdificios2 = new ObservableCollection<seg_usuarios_estatus2>();

        }//CONSTRUCTOR

        public ObservableCollection<seg_usuarios_estatus> FicSfDataGrid_ItemSource_CatEdificios
        {
            get
            {
                return _FicSfDataGrid_ItemSource_CatEdificios;
            }
        }//ESTE APUNTA ATRAVEZ DEL BindingContext AL GRID DE LA VIEW

        public ObservableCollection<seg_usuarios_estatus2> FicSfDataGrid_ItemSource_CatEdificios2
        {
            get
            {
                return _FicSfDataGrid_ItemSource_CatEdificios2;
            }
        }//ESTE APUNTA ATRAVEZ DEL BindingContext AL GRID DE LA VIEW


        public seg_usuarios_estatus _SfDataGrid_SelectItem_Edificio;
        public seg_usuarios_estatus FicSfDataGrid_SelectItem_CatEdificios
        {
            get { return _SfDataGrid_SelectItem_Edificio; }
            set
            {
                _SfDataGrid_SelectItem_Edificio = value;
                RaisePropertyChanged();
            }
        }//ESTE APUNTA A UN ITEM SELECCIONADO EN EL GRID DE LA VIEW

        public seg_usuarios_estatus2 _SfDataGrid_SelectItem_Edificio2;
        public seg_usuarios_estatus2 FicSfDataGrid_SelectItem_CatEdificios2
        {
            get { return _SfDataGrid_SelectItem_Edificio2; }
            set
            {
                _SfDataGrid_SelectItem_Edificio2 = value;
                seg_usuarios_estatus temp = new seg_usuarios_estatus() {
                    Activo = _SfDataGrid_SelectItem_Edificio2.Activo,
                    Actual = _SfDataGrid_SelectItem_Edificio2.Actual,
                    Borrado = _SfDataGrid_SelectItem_Edificio2.Borrado,
                    FechaEstatus = _SfDataGrid_SelectItem_Edificio2.FechaEstatus,
                    FechaReg = _SfDataGrid_SelectItem_Edificio2.FechaReg,
                    IdCrtlEstatus = _SfDataGrid_SelectItem_Edificio2.IdCrtlEstatus,
                    IdEstatus = _SfDataGrid_SelectItem_Edificio2.IdEstatus,
                    IdTipoEstatus = _SfDataGrid_SelectItem_Edificio2.IdTipoEstatus,
                    IdUsuario = _SfDataGrid_SelectItem_Edificio2.IdUsuario,
                    Observacion = _SfDataGrid_SelectItem_Edificio2.Observacion,
                    UsuarioReg = _SfDataGrid_SelectItem_Edificio2.UsuarioReg
                };
                _SfDataGrid_SelectItem_Edificio = temp;

                RaisePropertyChanged();
            }
        }//ESTE APUNTA A UN ITEM SELECCIONADO EN EL GRID DE LA VIEW



        public seg_usuarios_estatus SfDataGrid_SelectItem_Edificio
        {
            get { return _SfDataGrid_SelectItem_Edificio; }
            set
            {
                _SfDataGrid_SelectItem_Edificio = value;
                RaisePropertyChanged();
            }
        }

        /*private ICommand UpdateEdificio;
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
                IFicSrvNavigation.FicMetNavigateTo<FicVmSegUsuariosEstatusUpdate>(_SfDataGrid_SelectItem_Edificio, Edificio);
            }
            
        }*/


        private ICommand DetalleEdificio;
        public ICommand FicMetDetalleCommand
        {

            get { return DetalleEdificio = DetalleEdificio ?? new FicVmDelegateCommand(DetalleEdificioExecute); }
        }
        private void DetalleEdificioExecute()
        {            
            if (_SfDataGrid_SelectItem_Edificio2 == null)
            {
                new Page().DisplayAlert("ATENCION", "NO SELECIONASTE NINGUN ITEM", "OK");
                return;
            }
            else
            {                                                 
                IFicSrvNavigation.FicMetNavigateTo<FicVmSegUsuariosEstatusDetail>(FicSfDataGrid_SelectItem_CatEdificios2, Edificio);
            }

        }

        private ICommand NewEdificio;
        public ICommand FicMetNewCommand
        {
            get { return NewEdificio = NewEdificio ?? new FicVmDelegateCommand(NewEdificioExecute); }
        }
        private void NewEdificioExecute()
        {

            IFicSrvNavigation.FicMetNavigateTo<FicVmSegUsuariosEstatusNew>(Edificio);
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

        public void NewEdExecute()
        {

            IFicSrvNavigation.FicMetNavigateTo<FicVmSegUsuariosEstatusList>(Edificio);
        }

        public async void OnAppearing()
        {
            try
            {
                var source_local_inv = await IFicSrvSegUsuariosEstatusList.FicMetGetListSegExpiraClaves(Edificio.IdUsuario);
                if (source_local_inv != null && _FicSfDataGrid_ItemSource_CatEdificios.Count == 0)
                {
                    var source_local_estatus = await IFicSrvSegUsuariosEstatusList.FicMetGetListRhCatEstatus(1);
                    var source_local_tipo_estatus = await IFicSrvSegUsuariosEstatusList.FicMetGetListRhCatTipoEstatus(1);
                    foreach (seg_usuarios_estatus inv in source_local_inv)
                    {
                        _FicSfDataGrid_ItemSource_CatEdificios.Add(inv);
                        foreach (cat_tipos_estatus tipo in source_local_tipo_estatus) {
                            foreach (cat_estatus est in source_local_estatus)
                            {
                                if (est.IdEstatus == inv.IdEstatus)
                                {
                                    seg_usuarios_estatus2 temp = new seg_usuarios_estatus2()
                                    {
                                        Activo = inv.Activo,
                                        Actual = inv.Actual,
                                        Borrado = inv.Borrado,
                                        FechaEstatus = inv.FechaEstatus,
                                        FechaReg = inv.FechaReg,
                                        IdCrtlEstatus = inv.IdCrtlEstatus,
                                        Estatus = est.DesEstatus,
                                        IdEstatus = inv.IdEstatus,
                                        TipoEstatus = tipo.DesTipoEstatus,
                                        IdTipoEstatus = tipo.IdTipoEstatus,
                                        IdUsuario = inv.IdUsuario,
                                        Observacion = inv.Observacion,
                                        UsuarioReg = inv.UsuarioReg
                                    };
                                    _FicSfDataGrid_ItemSource_CatEdificios2.Add(temp);

                                }

                            }
                        }                                                
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
