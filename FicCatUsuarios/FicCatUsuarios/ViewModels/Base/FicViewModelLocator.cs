using FicCatUsuarios.Services.Navegacion;

using FicCatUsuarios.Views.CatUsuarios;

using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using FicCatUsuarios.Services.CatUsuarios;
using FicCatUsuarios.Interfaces.CatUsuarios;
using FicCatUsuarios.ViewModels.CatUsuarios;

using FicCatUsuarios.Services.SegExpiraClaves;
using FicCatUsuarios.Interfaces.SegExpiraClaves;
using FicCatUsuarios.ViewModels.SegExpiraClaves;

using FicCatUsuarios.Services.SegUsuariosEstatus;
using FicCatUsuarios.Interfaces.SegUsuariosEstatus;
using FicCatUsuarios.ViewModels.SegUsuariosEstatus;

using FicCatUsuarios.Interfaces.Navegacion;

namespace FicCatUsuarios.ViewModels.Base
{
    public class FicViewModelLocator
    {
        private static IContainer FicIContainer;

        public FicViewModelLocator()
        {
            //FIC: ContainerBuilder es una clase de la libreria de Autofac para poder ejecutar la interfaz en las diferentes plataformas 
            var FicContainerBuilder = new ContainerBuilder();

            //-------------------------------- VIEW MODELS ------------------------------------------------------
            //FIC: se procede a registrar las ViewModels para que se puedan mandar llamar en cualquier plataforma
            //---------------------------------------------------------------------------------------------------

            FicContainerBuilder.RegisterType<FicVmCatUsuariosList>();
            FicContainerBuilder.RegisterType<FicVmCatUsuariosNew>();
            FicContainerBuilder.RegisterType<FicVmCatUsuariosUpdate>();
            FicContainerBuilder.RegisterType<FicVmCatUsuariosDetail>();

            FicContainerBuilder.RegisterType<FicVmSegExpiraClavesList>();
            FicContainerBuilder.RegisterType<FicVmSegExpiraClavesNew>();
            FicContainerBuilder.RegisterType<FicVmSegExpiraClavesUpdate>();
            FicContainerBuilder.RegisterType<FicVmSegExpiraClavesDetail>();

            FicContainerBuilder.RegisterType<FicVmSegUsuariosEstatusList>();
            FicContainerBuilder.RegisterType<FicVmSegUsuariosEstatusNew>();
            FicContainerBuilder.RegisterType<FicVmSegUsuariosEstatusDetail>();




            /*FicContainerBuilder.RegisterType<FicVmCatEdificiosNuevo>();
            FicContainerBuilder.RegisterType<FicVmCatEdificiosUpdate>();
            FicContainerBuilder.RegisterType<FicVmExportarWebApi>();
            FicContainerBuilder.RegisterType<FicVmImportarWebApi>();
            FicContainerBuilder.RegisterType<FicVmCatEdificiosDetalle>();*/
            //FicContainerBuilder.RegisterType<FicVmInventarioConteoList>();
            //FicContainerBuilder.RegisterType<FicVmInventarioConteosItem>();
            //FicContainerBuilder.RegisterType<FicVmInventarioAcumuladoList>();
            ////------------------------- INTERFACE SERVICES OF THE VIEW MODELS -----------------------------------
            ////FIC: se procede a registrar la interface con la que se comunican las ViewModels con los Servicios 
            ////para poder ejecutar las tareas (metodos o funciones, etc) del servicio en cuestion.
            ////---------------------------------------------------------------------------------------------------///

            FicContainerBuilder.RegisterType<FicSrvNavigation>().As<IFicSrvNavigation>().SingleInstance();
            
            FicContainerBuilder.RegisterType<FicSrvCatUsuariosList>().As<IFicSrvCatUsuariosList>().SingleInstance();
            FicContainerBuilder.RegisterType<FicSrvCatUsuariosNew>().As<IFicSrvCatUsuariosNew>().SingleInstance();
            FicContainerBuilder.RegisterType<FicSrvCatUsuariosUpdate>().As<IFicSrvCatUsuariosUpdate>().SingleInstance();
            FicContainerBuilder.RegisterType<FicSrvCatUsuariosDetail>().As<IFicSrvCatUsuariosDetail>().SingleInstance();

            FicContainerBuilder.RegisterType<FicSrvSegExpiraClavesList>().As<IFicSrvSegExpiraClavesList>().SingleInstance();
            FicContainerBuilder.RegisterType<FicSrvSegExpiraClavesNew>().As<IFicSrvSegExpiraClavesNew>().SingleInstance();
            FicContainerBuilder.RegisterType<FicSrvSegExpiraClavesUpdate>().As<IFicSrvSegExpiraClavesUpdate>().SingleInstance();
            FicContainerBuilder.RegisterType<FicSrvSegExpiraClavesDetail>().As<IFicSrvSegExpiraClavesDetail>().SingleInstance();

            FicContainerBuilder.RegisterType<FicSrvSegUsuariosEstatusList>().As<IFicSrvSegUsuariosEstatusList>().SingleInstance();
            FicContainerBuilder.RegisterType<FicSrvSegUsuariosEstatusNew>().As<IFicSrvSegUsuariosEstatusNew>().SingleInstance();
            FicContainerBuilder.RegisterType<FicSrvSegUsuariosEstatusDetail>().As<IFicSrvSegUsuariosEstatusDetail>().SingleInstance();


            /*FicContainerBuilder.RegisterType<FicSrvCatEdificiosList>().As<IFicSrvCatEdificiosList>().SingleInstance();
            FicContainerBuilder.RegisterType<FicSrvCatEdificiosUpdate>().As<IFicSrvCatEdificiosUpdate>().SingleInstance();
            FicContainerBuilder.RegisterType<FicSrvExportarWebApi>().As<IFicSrvExportarWebApi>().SingleInstance();
            FicContainerBuilder.RegisterType<FicSrvImportarWebApi>().As<IFicSrvImportarWebApi>().SingleInstance();
            FicContainerBuilder.RegisterType<FicSrvCatEdificiosDetalle>().As<IFicSrvCatEdificiosDetalles>().SingleInstance();*/
            //FicContainerBuilder.RegisterType<FicSrvInventariosConteosItem>().As<IFicSrvInventariosConteosItem>().SingleInstance();
            //FicContainerBuilder.RegisterType<FicSrvInventariosConteoList>().As<IFicSrvInventariosConteoList>().SingleInstance();
            //FicContainerBuilder.RegisterType<FicSrvInventarioAcumuladoList>().As<IFicSrvInventarioAcumuladoList>().SingleInstance();

            //FIC: se asigna o se libera el contenedor
            //-------------------------------------------
            if (FicIContainer != null) FicIContainer.Dispose();

            FicIContainer = FicContainerBuilder.Build();
        }//CONSTRUCTOR

        //-------------------- CONTROL DE INVENTARIOS ------------------------
        //FIC: se manda llamar desde el backend de la View de List
        public FicVmCatUsuariosList FicVmCatUsuariosList
        {
            get { return FicIContainer.Resolve<FicVmCatUsuariosList>(); }
        }
        public FicVmCatUsuariosNew FicVmCatUsuariosNew
        {
            get { return FicIContainer.Resolve<FicVmCatUsuariosNew>(); }
        }
        public FicVmCatUsuariosUpdate FicVmCatUsuariosUpdate
        {
            get { return FicIContainer.Resolve<FicVmCatUsuariosUpdate>(); }
        }
        public FicVmCatUsuariosDetail FicVmCatUsuariosDetail
        {
            get { return FicIContainer.Resolve<FicVmCatUsuariosDetail>(); }
        }

        public FicVmSegExpiraClavesList FicVmSegExpiraClavesList
        {
            get { return FicIContainer.Resolve<FicVmSegExpiraClavesList>(); }
        }
        public FicVmSegExpiraClavesNew FicVmSegExpiraClavesNew
        {
            get { return FicIContainer.Resolve<FicVmSegExpiraClavesNew>(); }
        }
        public FicVmSegExpiraClavesUpdate FicVmSegExpiraClavesUpdate
        {
            get { return FicIContainer.Resolve<FicVmSegExpiraClavesUpdate>(); }
        }

        public FicVmSegUsuariosEstatusList FicVmSegUsuariosEstatusList
        {
            get { return FicIContainer.Resolve<FicVmSegUsuariosEstatusList>(); }
        }
        public FicVmSegUsuariosEstatusNew FicVmSegUsuariosEstatusNew
        {
            get { return FicIContainer.Resolve<FicVmSegUsuariosEstatusNew>(); }
        }



        public FicVmSegExpiraClavesDetail FicVmSegExpiraClavesDetail
        {
            get { return FicIContainer.Resolve<FicVmSegExpiraClavesDetail>(); }
        }
        public FicVmSegUsuariosEstatusDetail FicVmSegUsuariosEstatusDetail
        {
            get { return FicIContainer.Resolve<FicVmSegUsuariosEstatusDetail>(); }
        }



    }//CLASS
}//NAMESPACE
