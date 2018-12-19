using System;
using FicCatUsuarios.Interfaces.Navegacion;
using FicCatUsuarios.ViewModels.Base;
using System.Windows.Input;
using  FicCatUsuarios.Models.Usuarios;
using FicCatUsuarios.Interfaces.SegExpiraClaves;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace FicCatUsuarios.ViewModels.SegExpiraClaves
{
    public class FicVmSegExpiraClavesNew : FicViewModelBase
    {
        private IFicSrvNavigation FicLoSrvNavigation;
        private IFicSrvSegExpiraClavesNew FicLoSrvApp;
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
        private seg_expira_claves _Edificios;
        public seg_expira_claves Edificio
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
            FicLoSrvNavigation.FicMetNavigateTo<FicVmSegExpiraClavesList>(usu);
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
            if (Edificio.FechaExpiraFin.Year <= 1)
            {
                Edificio.FechaExpiraFin = DateTime.Now;
            }
            else
            {
                var fecha_temp = Edificio.FechaExpiraFin;
                var day = Int32.Parse(fecha_temp.Day.ToString());
                var month = Int32.Parse(fecha_temp.Month.ToString());
                var year = Int32.Parse(fecha_temp.Year.ToString());

                var hours = Int32.Parse(DateTime.Now.Hour.ToString());
                var mins = Int32.Parse(DateTime.Now.Minute.ToString());
                var sec = Int32.Parse(DateTime.Now.Second.ToString());
                var milisec = Int32.Parse(DateTime.Now.Millisecond.ToString());
                DateTime fecha = new DateTime(year, month, day, hours, mins, sec, milisec);
                Edificio.FechaExpiraFin = fecha;
            }
            if (Edificio.FechaExpiraIni.Year <= 1)
            {
                Edificio.FechaExpiraIni = DateTime.Now;
            }
            else {
                var fecha_temp = Edificio.FechaExpiraIni;
                var day = Int32.Parse(fecha_temp.Day.ToString());
                var month = Int32.Parse(fecha_temp.Month.ToString());
                var year = Int32.Parse(fecha_temp.Year.ToString());

                var hours = Int32.Parse(DateTime.Now.Hour.ToString());
                var mins = Int32.Parse(DateTime.Now.Minute.ToString());
                var sec = Int32.Parse(DateTime.Now.Second.ToString());
                var milisec = Int32.Parse(DateTime.Now.Millisecond.ToString());
                DateTime fecha = new DateTime(year, month, day, hours, mins, sec, milisec);
                Edificio.FechaExpiraIni = fecha;
            }
            if (Edificio.Clave == null)
            {
                noFillFields.Add("clave");
            }
            
            #endregion

            //SI TODOS LOS CAMPOS PASARON LA VALIDACION ASIGNAN SE PREPARA LA INSERCION PARA EL OBJETO PERSONA
            if (noFillFields.Count == 0)
            {
                if (Edificio.FechaExpiraFin < Edificio.FechaExpiraIni)
                {
                    await new Page().DisplayAlert("ALERTA", "La fecha final " + Edificio.FechaExpiraFin.ToString() + " es menor que la fecha inicial " + Edificio.FechaExpiraIni.ToString(), "OK");
                }
                else if (Edificio.FechaExpiraFin == Edificio.FechaExpiraIni) {
                    await new Page().DisplayAlert("ALERTA", "La fecha final " + Edificio.FechaExpiraFin.ToString() + " es igual que la fecha inicial " + Edificio.FechaExpiraIni.ToString(), "OK");
                }
                else {
                    Edificio.UsuarioReg = "GERA";
                    //Edificio.UsuarioMod = Edificio.UsuarioReg;
                    //Edificio.FechaUltMod = DateTime.Now;
                    Edificio.FechaReg = DateTime.Now;
                    Edificio.IdUsuario = usu.IdUsuario;
                    //Edificio.FechaAlta = DateTime.Now;
                    if (Edificio.Actual == "True") { Edificio.Actual = "S"; };
                    if (Edificio.Actual == null) { Edificio.Actual = "N"; };
                    if (Edificio.ClaveAutoSys == "True") { Edificio.ClaveAutoSys = "S"; };
                    if (Edificio.ClaveAutoSys == null) { Edificio.ClaveAutoSys = "N"; };
                    if (Edificio.Activo == "True") { Edificio.Activo = "S"; };
                    if (Edificio.Activo == null) { Edificio.Activo = "N"; };
                    if (Edificio.Borrado == "True") { Edificio.Borrado = "S"; };
                    if (Edificio.Borrado == null) { Edificio.Borrado = "N"; };
                    await FicLoSrvApp.FicMetGetNewRhCatTelefonos(Edificio);
                    FicLoSrvNavigation.FicMetNavigateTo<FicVmSegExpiraClavesList>(usu);
                }                
            }
            else
            {
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

        public FicVmSegExpiraClavesNew(IFicSrvNavigation FicPaSrvNavigation, IFicSrvSegExpiraClavesNew FicPaSrvApp)
        {
            FicLoSrvNavigation = FicPaSrvNavigation;
            FicLoSrvApp = FicPaSrvApp;
        }

        public override void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);
            _Edificios = new seg_expira_claves();
        }
        public void llenado(cat_usuarios obj)
        {
            usu = new cat_usuarios();
            usu = obj;
        }

    }
}

