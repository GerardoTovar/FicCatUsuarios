using FicCatUsuarios.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FicCatUsuarios.Interfaces.CatUsuarios
{
    public interface IFicSrvCatUsuariosDetail
    {
        
        Task<IEnumerable<rh_cat_personas>> FicMetGetListRhCatPersonas();//GET GENERALES        
    }
}
