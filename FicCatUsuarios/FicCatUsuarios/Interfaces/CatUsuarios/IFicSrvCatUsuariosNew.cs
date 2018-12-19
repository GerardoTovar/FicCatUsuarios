
using System;
using System.Collections.Generic;
using System.Text;
using FicCatUsuarios.Models.Usuarios;
using System.Threading.Tasks;
using FicCatUsuarios.Data;
namespace FicCatUsuarios.Interfaces.CatUsuarios
{
    public interface IFicSrvCatUsuariosNew
    {
        Task FicMetGetNewRhCatTelefonos(cat_usuarios item);// CREATE
        Task<IEnumerable<rh_cat_personas>> FicMetGetListRhCatPersonas();//GET PERSONAS
    }
}

