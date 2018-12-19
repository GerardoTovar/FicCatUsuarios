using System;
using FicCatUsuarios.Interfaces.Navegacion;
using FicCatUsuarios.ViewModels.Base;
using System.Windows.Input;
using  FicCatUsuarios.Models.Usuarios;
using FicCatUsuarios.Interfaces.SegUsuariosEstatus;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;

namespace FicCatUsuarios.ViewModels.SegUsuariosEstatus
{
    public class FicVmSegUsuariosEstatusNew : FicViewModelBase
    {
        private IFicSrvNavigation FicLoSrvNavigation;
        private IFicSrvSegUsuariosEstatusNew FicLoSrvApp;
        public ObservableCollection<cat_estatus> _Picker_ItemSource_CatEstatus;
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
        private seg_usuarios_estatus _Edificios;
        public seg_usuarios_estatus Edificio
        {
            get { return _Edificios; }
            set
            {
                _Edificios = value;
                RaisePropertyChanged();
            }
        }

        private ICommand BackNavigation;
        public ICommand BackNavgCommand
        {
            get { return BackNavigation = BackNavigation ?? new FicVmDelegateCommand(BackNavgExecute); }
        }


        private void BackNavgExecute()
        {
            //FicLoSrvNavigation.FicMetNavigateBack();
            FicLoSrvNavigation.FicMetNavigateTo<FicVmSegUsuariosEstatusList>(usu);
        }

        private ICommand AddEdificio;
        public ICommand FicMetAddCommand
        {
            get { return AddEdificio = AddEdificio ?? new FicVmDelegateCommand(AddEdificioExecute); }
        }
        private async void AddEdificioExecute()
        {
            List<string> noFillFields = new List<string>();//LISTA DE CAMPOS QUE NO FUERON LLENADOS

            //CONJUNTO DE VALIDACIONES DE CAMPOS NO LLENADOS O NO SELECCIONADOS
            #region
            if (Edificio.FechaEstatus.Year <= 1)
            {
                Edificio.FechaEstatus = DateTime.Now;
            }
            if (Edificio.Observacion == null)
            {
                noFillFields.Add("observaciones");
            }
            if (Picker_SelectItem_CatEstatus == null) {
                noFillFields.Add("estatus");
            }
            else
            {
                var fecha_temp = Edificio.FechaEstatus;
                var day = Int32.Parse(fecha_temp.Day.ToString());
                var month = Int32.Parse(fecha_temp.Month.ToString());
                var year = Int32.Parse(fecha_temp.Year.ToString());

                var hours = Int32.Parse(DateTime.Now.Hour.ToString());
                var mins = Int32.Parse(DateTime.Now.Minute.ToString());
                var sec = Int32.Parse(DateTime.Now.Second.ToString());
                var milisec = Int32.Parse(DateTime.Now.Millisecond.ToString());
                DateTime fecha = new DateTime(year, month, day, hours, mins, sec, milisec);
                Edificio.FechaEstatus = fecha;

            }
            #endregion

            //SI TODOS LOS CAMPOS PASARON LA VALIDACION ASIGNAN SE PREPARA LA INSERCION PARA EL OBJETO PERSONA
            if (noFillFields.Count == 0)
            {
                Edificio.IdEstatus = Picker_SelectItem_CatEstatus.IdEstatus;
                Edificio.IdTipoEstatus = Picker_SelectItem_CatEstatus.IdTipoEstatus;
                Edificio.UsuarioReg = "GERA";
                
                Edificio.FechaReg = DateTime.Now;
                Edificio.IdUsuario = usu.IdUsuario;

                
                if (Edificio.Actual == "True") { Edificio.Actual = "S"; };
                if (Edificio.Actual == null) { Edificio.Actual = "N"; };
                // if (Edificio.ClaveAutoSys == "True") { Edificio.ClaveAutoSys = "S"; };
                //if (Edificio.ClaveAutoSys == null) { Edificio.ClaveAutoSys = "N"; };
                if (Edificio.Activo == "True") { Edificio.Activo = "S"; };
                if (Edificio.Activo == null) { Edificio.Activo = "N"; };
                if (Edificio.Borrado == "True") { Edificio.Borrado = "S"; };
                if (Edificio.Borrado == null) { Edificio.Borrado = "N"; };
                await FicLoSrvApp.FicMetGetNewRhCatTelefonos(Edificio);
                FicLoSrvNavigation.FicMetNavigateTo<FicVmSegUsuariosEstatusList>(usu);
            }
            else {
                //LISTADO DE LOS CAMPOS QUE NO FUERON LLENADOS
                var empty_fields = "Los siguientes campos no fueron llenados: ";
                int i = 0;
                foreach (string field in noFillFields)
                {
                    i++;
                    if (i != noFillFields.Count() - 1)
                    {
                        if (i != noFillFields.Count())
                        {
                            empty_fields += field + ", ";
                        }
                        else
                        {
                            empty_fields += field + ".";
                        }
                    }
                    else
                    {
                        empty_fields += field + " y ";
                    }
                }
                //ALERT DIALOG DE CAMPOS FALTANTES
                await new Page().DisplayAlert("ALERTA", empty_fields.ToString(), "OK");
            }
        }

        public FicVmSegUsuariosEstatusNew(IFicSrvNavigation FicPaSrvNavigation, IFicSrvSegUsuariosEstatusNew FicPaSrvApp)
        {
            FicLoSrvNavigation = FicPaSrvNavigation;
            FicLoSrvApp = FicPaSrvApp;
            _Picker_ItemSource_CatEstatus = new ObservableCollection<cat_estatus>();
        }


        //=========================Item sources==============================//        

        public ObservableCollection<cat_estatus> Picker_ItemSource_CatEstatus
        {
            get
            {
                return _Picker_ItemSource_CatEstatus;
            }
        }//ESTE APUNTA ATRAVEZ DEL BindingContext AL PIKER OCUPACION DE LA VIEW


        public cat_estatus _Picker_SelectItem_CatEstatus;
        public cat_estatus Picker_SelectItem_CatEstatus
        {
            get { return _Picker_SelectItem_CatEstatus; }
            set
            {
                _Picker_SelectItem_CatEstatus = value;
                RaisePropertyChanged();
            }
        }//ESTE APUNTA A UN ITEM SELECCIONADO EN EL PICKER TIPO PERSONA DE LA VIEW



        public async void OnAppearing(object navigationContext)
        {
            //base.OnAppearing(navigationContext);
            try
            {
                var source_local_estatus = await FicLoSrvApp.FicMetGetListRhCatEstatus(1);
                if (source_local_estatus != null && _Picker_ItemSource_CatEstatus.Count == 0)
                {
                    foreach (cat_estatus estatus in source_local_estatus)
                    {
                        _Picker_ItemSource_CatEstatus.Add(estatus);
                    }
                }//LLENAR EL PICKER DE OCUPACIONES
            }
            catch (Exception e) {

            }
            _Edificios = new seg_usuarios_estatus();
        }
        public void llenado(cat_usuarios obj)
        {
            usu = new cat_usuarios();
            usu = obj;
        }

    }
}
