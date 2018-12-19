using System;
using System.Collections.Generic;
using System.Text;
using FicCatUsuarios.ViewModels.Base;
using FicCatUsuarios.Models.Usuarios;
using System.Windows.Input;
using FicCatUsuarios.Interfaces.Navegacion;
using FicCatUsuarios.Interfaces.SegExpiraClaves;
using FicCatUsuarios.Services.SegExpiraClaves;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;

namespace FicCatUsuarios.ViewModels.SegExpiraClaves
{
    public class FicVmSegExpiraClavesUpdate : FicViewModelBase
    {
        private IFicSrvNavigation FicLoSrvNavigation;
        private IFicSrvSegExpiraClavesUpdate FicLoSrvApp;

        private bool _boll1;
        public bool boll1{get { return _boll1; }set{_boll1 = value;RaisePropertyChanged();}}
        private bool _boll2;
        public bool boll2 { get { return _boll2; } set { _boll2 = value; RaisePropertyChanged(); } }
        private bool _boll3;
        public bool boll3 { get { return _boll3; } set { _boll3 = value; RaisePropertyChanged(); } }
        private bool _boll4;
        public bool boll4 { get { return _boll4; } set { _boll4 = value; RaisePropertyChanged(); } }


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
        public seg_expira_claves _Edificios;
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

        private ICommand UpdateEdificio;
        public ICommand FicMetUpdateCommand
        {
            get { return UpdateEdificio = UpdateEdificio ?? new FicVmDelegateCommand(UpdateEdificioExecute); }
        }
        private async void UpdateEdificioExecute()
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
            else
            {
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
                else if (Edificio.FechaExpiraFin == Edificio.FechaExpiraIni)
                {
                    await new Page().DisplayAlert("ALERTA", "La fecha final " + Edificio.FechaExpiraFin.ToString() + " es igual que la fecha inicial " + Edificio.FechaExpiraIni.ToString(), "OK");
                }
                else
                {
                    if (boll1 == true) { Edificio.Actual = "S"; };
                    if (boll1 == false) { Edificio.Actual = "N"; };
                    if (boll2 == true) { Edificio.ClaveAutoSys = "S"; };
                    if (boll2 == false) { Edificio.ClaveAutoSys = "N"; };
                    if (boll3 == true) { Edificio.Activo = "S"; };
                    if (boll3 == false) { Edificio.Activo = "N"; };
                    if (boll4 == true) { Edificio.Borrado = "S"; };
                    if (boll4 == false) { Edificio.Borrado = "N"; };
                    await FicLoSrvApp.FicMetUpdateEdificio(Edificio);
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

        public FicVmSegExpiraClavesUpdate(IFicSrvNavigation FicPaSrvNavigation, IFicSrvSegExpiraClavesUpdate FicPaSrvApp)
        {
            FicLoSrvNavigation = FicPaSrvNavigation;
            FicLoSrvApp = FicPaSrvApp;
        }



        public override void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);
            Edificio = new seg_expira_claves();
        }
        public void llenado(seg_expira_claves obj)
        {
            Edificio = obj;

            if (Edificio.Actual == "S") { boll1 = true;  };
            if (Edificio.Actual == "N") { boll1 = false; };
            if (Edificio.ClaveAutoSys == "S") { boll2 = true; };
            if (Edificio.ClaveAutoSys == "N") { boll2 = false; };
            if (Edificio.Activo == "S") { boll3 = true; };
            if (Edificio.Activo == "N") { boll3 = false; };
            if (Edificio.Borrado == "S") { boll4 = true; };
            if (Edificio.Borrado == "N") { boll4 = false; };


        }
        public void llenado2(cat_usuarios obj)
        {
            usu = new cat_usuarios();
            usu = obj;
        }


    }
}