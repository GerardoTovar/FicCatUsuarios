using System;
using System.Collections.Generic;
using System.Text;
using FicCatUsuarios.Models.Usuarios;
using System.Threading.Tasks;
using FicCatUsuarios.Data;
namespace FicCatUsuarios.Interfaces.SegUsuariosEstatus
{
    public interface IFicSrvSegUsuariosEstatusNew
    {
        Task FicMetGetNewRhCatTelefonos(seg_usuarios_estatus item);// CREATE
        Task<IEnumerable<cat_estatus>> FicMetGetListRhCatEstatus(int id);//GET ESTATUS
    }
}
