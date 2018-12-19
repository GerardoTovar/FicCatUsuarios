using System;
using System.Collections.Generic;
using System.Text;
using FicCatUsuarios.Models.Usuarios;
using System.Threading.Tasks;
using FicCatUsuarios.Data;
namespace FicCatUsuarios.Interfaces.CatUsuarios
{
    public interface IFicSrvCatUsuariosUpdate
    {
        Task FicMetUpdateEdificio(cat_usuarios ficPa_eva_Cat_Edificios);
        Task<IEnumerable<rh_cat_personas>> FicMetGetListRhCatPersonas();//GET PERSONAS
    }
}
