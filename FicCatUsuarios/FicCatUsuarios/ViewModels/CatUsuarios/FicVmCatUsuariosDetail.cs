using FicCatUsuarios.Interfaces.Navegacion;
using FicCatUsuarios.Interfaces.CatUsuarios;
using FicCatUsuarios.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using  FicCatUsuarios.Models.Usuarios;

namespace FicCatUsuarios.ViewModels.CatUsuarios
{
    public class FicVmCatUsuariosDetail : FicViewModelBase
    {
        public ObservableCollection<cat_usuarios> _FicSfDataGrid_ItemSource_CatAlumno;

        private IFicSrvNavigation FicLoSrvNavigation;
        private IFicSrvCatUsuariosDetail FicLoSrvApp;
        public ObservableCollection<AutoCompletePersona> _FicSourceAutocompletePersonas;
        private IFicSrvNavigation IFicSrvNavigation;
        private IFicSrvCatUsuariosDetail IFicSrvCatUsuariosDetail;

        private cat_usuarios _Persona;
        public cat_usuarios Persona
        {
            get { return _Persona; }
            set
            {
                _Persona = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<AutoCompletePersona> FicSourceAutocompletePersonas
        {
            get
            {
                return _FicSourceAutocompletePersonas;
            }
        }//ESTE APUNTA ATRAVEZ DEL BindingContext AL AUTOCOMPLETE PERSONA DE LA VIEW

        private AutoCompletePersona _usu;
        public AutoCompletePersona usu
        {
            get { return _usu; }
            set
            {
                _usu = value;
                RaisePropertyChanged();
            }
        }

        public FicVmCatUsuariosDetail(IFicSrvNavigation FicPaSrvNavigation, IFicSrvCatUsuariosDetail FicPaSrvApp)
        {
            FicLoSrvNavigation = FicPaSrvNavigation;
            FicLoSrvApp = FicPaSrvApp;
            _FicSourceAutocompletePersonas = new ObservableCollection<AutoCompletePersona>();
        }

        private ICommand BackNavigation;
        public ICommand BackNavgCommand
        {
            get { return BackNavigation = BackNavigation ?? new FicVmDelegateCommand(BackNavgExecute); }
        }
        private void BackNavgExecute()
        {

            FicLoSrvNavigation.FicMetNavigateTo<FicVmCatUsuariosList>(null);
        }



        public async void OnAppearing()
        {

            try
            {
                var source_local_personas = await FicLoSrvApp.FicMetGetListRhCatPersonas();
                if (source_local_personas != null && _FicSourceAutocompletePersonas.Count == 0)
                {
                    foreach (rh_cat_personas persona in source_local_personas)
                    {
                        var persona_temp = new AutoCompletePersona() { IdPersona = persona.IdPersona, NombreCompleto = persona.Nombre + " " + persona.ApPaterno + " " + persona.ApMaterno };
                        _FicSourceAutocompletePersonas.Add(persona_temp);
                        if (persona_temp.IdPersona == Persona.IdPersona)
                        {
                            usu = persona_temp;
                        }
                    }
                }//LLENAR EL AUTOCOMPLETE DE PERSONAS
            }
            catch (Exception e)
            {
                await new Page().DisplayAlert("ALERTA", e.Message.ToString(), "OK");
            }

        }//SOBRECARGA AL METODO OnAppearing() DE LA VIEW 

        public  void llenado(cat_usuarios item)
        {
            Persona = new cat_usuarios();
            Persona = item;

        }//SOBRECARGA AL METODO OnAppearing() DE LA VIEW 


    }//CLASS }



}//NAMESPACE