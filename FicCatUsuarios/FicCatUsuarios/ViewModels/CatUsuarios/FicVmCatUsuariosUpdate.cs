using System;
using System.Collections.Generic;
using System.Text;
using FicCatUsuarios.ViewModels.Base;
using FicCatUsuarios.Models.Usuarios;
using System.Windows.Input;
using FicCatUsuarios.Interfaces.Navegacion;
using FicCatUsuarios.Interfaces.CatUsuarios;
using FicCatUsuarios.Services.CatUsuarios;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace FicCatUsuarios.ViewModels.CatUsuarios
{
    public class FicVmCatUsuariosUpdate : FicViewModelBase
    {
        private IFicSrvNavigation FicLoSrvNavigation;
        private IFicSrvCatUsuariosUpdate FicLoSrvApp;
        
        public ObservableCollection<AutoCompletePersona> _FicSourceAutocompletePersonas;
        private bool _boll1;
        public bool boll1 { get { return _boll1; } set { _boll1 = value; RaisePropertyChanged(); } }
        private bool _boll2;
        public bool boll2 { get { return _boll2; } set { _boll2 = value; RaisePropertyChanged(); } }
        private bool _boll3;
        public bool boll3 { get { return _boll3; } set { _boll3 = value; RaisePropertyChanged(); } }
        private bool _boll4;
        public bool boll4 { get { return _boll4; } set { _boll4 = value; RaisePropertyChanged(); } }

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

        private ICommand UpdateEdificio;
        public ICommand FicMetUpdateCommand
        {
            get { return UpdateEdificio = UpdateEdificio ?? new FicVmDelegateCommand(UpdateEdificioExecuteAsync); }
        }
        private async void UpdateEdificioExecuteAsync()
        {
            List<string> noFillFields = new List<string>();//LISTA DE CAMPOS QUE NO FUERON LLENADOS

            //CONJUNTO DE VALIDACIONES DE CAMPOS NO LLENADOS O NO SELECCIONADOS
            #region
            if (Edificio.FechaAlta.Year <= 1)
            {
                Edificio.FechaAlta = DateTime.Now;
            }
            if (_AutocompleteSelectedPersona == null)
            {
                noFillFields.Add("persona");
            }
            if (Edificio.Usuario == null)
            {
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
                Edificio.IdPersona = AutocompleteSelectedPersona.IdPersona;
                Edificio.FechaUltMod = DateTime.Now;
                if (boll1 == true) { Edificio.Conectado = "S"; };
                if (boll1 == false) { Edificio.Conectado = "N"; };
                if (boll2 == true) { Edificio.Expira = "S"; };
                if (boll2 == false) { Edificio.Expira = "N"; };
                if (boll3 == true) { Edificio.Activo = "S"; };
                if (boll3 == false) { Edificio.Activo = "N"; };
                if (boll4 == true) { Edificio.Borrado = "S"; };
                if (boll4 == false) { Edificio.Borrado = "N"; };
                await FicLoSrvApp.FicMetUpdateEdificio(Edificio);
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


        public FicVmCatUsuariosUpdate(IFicSrvNavigation FicPaSrvNavigation, IFicSrvCatUsuariosUpdate FicPaSrvApp)
        {
            FicLoSrvNavigation = FicPaSrvNavigation;
            FicLoSrvApp = FicPaSrvApp;
            _FicSourceAutocompletePersonas = new ObservableCollection<AutoCompletePersona>();
        }



        public async void OnAppearing(object navigationContext)
        {
            //base.OnAppearing(navigationContext);
            
            try
            {
                var source_local_personas = await FicLoSrvApp.FicMetGetListRhCatPersonas();
                if (source_local_personas != null && _FicSourceAutocompletePersonas.Count == 0)
                {                    
                    foreach (rh_cat_personas persona in source_local_personas)
                    {
                        
                        var persona_temp = new AutoCompletePersona() { IdPersona = persona.IdPersona, NombreCompleto = persona.Nombre +" "+persona.ApPaterno+" "+persona.ApMaterno };
                        _FicSourceAutocompletePersonas.Add(persona_temp);
                        if (Edificio.IdPersona == persona.IdPersona)
                        {
                            AutocompleteSelectedPersona = persona_temp;
                        }
                    }
                }//LLENAR EL AUTOCOMPLETE DE PERSONAS
            }
            catch (Exception e)
            {

            }            
        }
        public void llenado(cat_usuarios obj)
        {
            Edificio = new cat_usuarios();
            Edificio = obj;
            if (Edificio.Conectado == "S") { boll1 = true; };
            if (Edificio.Conectado == "N") { boll1 = false; };
            if (Edificio.Expira == "S") { boll2 = true; };
            if (Edificio.Expira == "N") { boll2 = false; };
            if (Edificio.Activo == "S") { boll3 = true; };
            if (Edificio.Activo == "N") { boll3 = false; };
            if (Edificio.Borrado == "S") { boll4 = true; };
            if (Edificio.Borrado == "N") { boll4 = false; };

        }


    }
}
