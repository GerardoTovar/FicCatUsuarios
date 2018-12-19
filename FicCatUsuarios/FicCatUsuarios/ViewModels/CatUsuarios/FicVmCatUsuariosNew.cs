using System;
using FicCatUsuarios.Interfaces.Navegacion;
using FicCatUsuarios.ViewModels.Base;
using System.Windows.Input;
using  FicCatUsuarios.Models.Usuarios;
using FicCatUsuarios.Interfaces.CatUsuarios;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;

namespace FicCatUsuarios.ViewModels.CatUsuarios
{
    public class FicVmCatUsuariosNew : FicViewModelBase
    {
        private IFicSrvNavigation FicLoSrvNavigation;
        public ObservableCollection<AutoCompletePersona> _FicSourceAutocompletePersonas;
        private IFicSrvCatUsuariosNew FicLoSrvApp;

        private cat_usuarios _Edificios;
        public cat_usuarios Edificio
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
            FicLoSrvNavigation.FicMetNavigateTo<FicVmCatUsuariosList>(null);
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
            if (Edificio.FechaAlta.Year <= 1)
            {
                Edificio.FechaAlta = DateTime.Now;
            }
            if (_AutocompleteSelectedPersona == null) {
                noFillFields.Add("persona");
            }
            if (Edificio.Usuario == null) {
                noFillFields.Add("usuario");
            }
            else
            {
                var fecha_temp = Edificio.FechaAlta;
                var day = Int32.Parse(fecha_temp.Day.ToString());
                var month = Int32.Parse(fecha_temp.Month.ToString());
                var year = Int32.Parse(fecha_temp.Year.ToString());

                var hours = Int32.Parse(DateTime.Now.Hour.ToString());
                var mins = Int32.Parse(DateTime.Now.Minute.ToString());
                var sec = Int32.Parse(DateTime.Now.Second.ToString());
                var milisec = Int32.Parse(DateTime.Now.Millisecond.ToString());
                DateTime fecha = new DateTime(year, month, day, hours, mins, sec, milisec);
                Edificio.FechaAlta = fecha;

            }
            #endregion

            //SI TODOS LOS CAMPOS PASARON LA VALIDACION ASIGNAN SE PREPARA LA INSERCION PARA EL OBJETO PERSONA
            if (noFillFields.Count == 0)
            {
                Edificio.UsuarioReg = "GERA";
                Edificio.UsuarioMod = Edificio.UsuarioReg;
                Edificio.IdPersona = AutocompleteSelectedPersona.IdPersona;
                Edificio.FechaUltMod = DateTime.Now;
                Edificio.FechaReg = DateTime.Now;

                if (Edificio.Conectado == "True") { Edificio.Conectado = "S"; };
                if (Edificio.Conectado == null) { Edificio.Conectado = "N"; };
                if (Edificio.Expira == "True") { Edificio.Expira = "S"; };
                if (Edificio.Expira == null)   { Edificio.Expira = "N"; };
                if (Edificio.Activo == "True") { Edificio.Activo = "S"; };
                if (Edificio.Activo == null)   { Edificio.Activo = "N"; };
                if (Edificio.Borrado == "True"){ Edificio.Borrado = "S";};
                if (Edificio.Borrado == null)  { Edificio.Borrado = "N";};
                await FicLoSrvApp.FicMetGetNewRhCatTelefonos(Edificio);
                FicLoSrvNavigation.FicMetNavigateTo<FicVmCatUsuariosList>(null);
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

        //=========================Item sources==============================//        

        public ObservableCollection<AutoCompletePersona> FicSourceAutocompletePersonas
        {
            get
            {
                return _FicSourceAutocompletePersonas;
            }
        }//ESTE APUNTA ATRAVEZ DEL BindingContext AL AUTOCOMPLETE PERSONA DE LA VIEW

        

        //=========================Item selected==============================//     

        public AutoCompletePersona _AutocompleteSelectedPersona;
        public AutoCompletePersona AutocompleteSelectedPersona
        {
            get { return _AutocompleteSelectedPersona; }
            set
            {
                _AutocompleteSelectedPersona = value;
                RaisePropertyChanged();
            }
        }//ESTE APUNTA A UN ITEM SELECCIONADO EN EL AUTOCOMPLETE PERSONA DE LA VIEW






        public FicVmCatUsuariosNew(IFicSrvNavigation FicPaSrvNavigation, IFicSrvCatUsuariosNew FicPaSrvApp)
        {
            FicLoSrvNavigation = FicPaSrvNavigation;
            FicLoSrvApp = FicPaSrvApp;
            _FicSourceAutocompletePersonas = new ObservableCollection<AutoCompletePersona>();           
        }

        public async void OnAppearing(object navigationContext)
        {

            //base.OnAppearing(navigationContext);
            _Edificios = new cat_usuarios();
            Edificio.FechaAlta = new DateTime();
            Edificio.NumIntentos = 0;            
            try
            {
                var source_local_personas = await FicLoSrvApp.FicMetGetListRhCatPersonas();
                if (source_local_personas != null && _FicSourceAutocompletePersonas.Count == 0)
                {                    
                    foreach (rh_cat_personas persona in source_local_personas)
                    {                        
                        var persona_temp = new AutoCompletePersona() { IdPersona = persona.IdPersona, NombreCompleto = persona.Nombre +" "+persona.ApPaterno+" "+persona.ApMaterno };
                        _FicSourceAutocompletePersonas.Add(persona_temp);                        
                    }
                }//LLENAR EL AUTOCOMPLETE DE PERSONAS
            }
            catch (Exception e)
            {

            }            
        }      
    }
}


