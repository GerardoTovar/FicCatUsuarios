﻿
using FicCatUsuarios.Views.CatUsuarios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FicCatUsuarios.Views.Navegacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FicMasterPageMaster : ContentPage
    {
        public ListView ListView;

        public FicMasterPageMaster()
        {
            InitializeComponent();

            BindingContext = new FicMasterPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class FicMasterPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<FicMasterPageMenuItem> MenuItems { get; }

            public FicMasterPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<FicMasterPageMenuItem>(new[]
                {
                    new FicMasterPageMenuItem { Id = 0, Title = "Lista de Usuarios",
                                                Icon ="ficAlmacen20x20.png",
                                                FicPageName ="FicViCatUsuariosList",
                                                TargetType = typeof(FicViCatUsuariosList)
                                                },
                    /*new FicMasterPageMenuItem { Id = 0, Title = "Lista de Empleados",
                                                Icon ="ficAlmacen20x20.png",
                                                FicPageName ="FicViRhCatEmpleadosList",
                                                TargetType = typeof(FicViRhCatEmpleadosList)
                                                },
                    new FicMasterPageMenuItem { Id = 0, Title = "Lista de Catedraticos",
                                                Icon ="ficAlmacen20x20.png",
                                                FicPageName ="FicViRhCatatedraticosList",
                                                TargetType = typeof(FicViRhCatatedraticosList)
                                                },*/
                    //new FicMasterPageMenuItem { Id = 0, Title = "Exportar Web Api",
                    //                            Icon ="ficAlmacen20x20.png",
                    //                            FicPageName ="FicViExportarWebApi",
                    //                            TargetType = typeof(FicViExportarWebApi)
                    //                            }

                });
            }//CONSTRUCTOR

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }//CLASS FicMasterPageMasterViewModel
    }//CLASS FicMasterPageMaster
}//NAMESPACE