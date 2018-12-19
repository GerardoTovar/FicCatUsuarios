using System;
using System.Collections.Generic;
using System.Text;

namespace FicCatUsuarios.Interfaces.Navegacion
{
    public interface IFicSrvNavigation
    {
        /*METODOS PARA LA NAVEGACION ENTRE VIEWS DE LA APP*/
        void FicMetNavigateTo<FicTDestinationViewModel>(object FicNavigationContext = null, object FicNavigationContext2 = null);
        void FicMetNavigateTo<FicTDestinationViewModel>(object FicNavigationContext = null);
        void FicMetNavigateTo(Type FicDestinationType, object FicNavigationContext = null);
        void FicMetNavigateBack();
    }//INTERFACE
}//NAMESPACE