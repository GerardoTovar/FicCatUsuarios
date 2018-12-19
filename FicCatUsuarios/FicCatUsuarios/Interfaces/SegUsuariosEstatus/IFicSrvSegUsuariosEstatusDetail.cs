using FicCatUsuarios.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FicCatUsuarios.Interfaces.SegUsuariosEstatus
{
    public interface IFicSrvSegUsuariosEstatusDetail
    {
        Task FicMetGetDetailRhCatPersonas(seg_usuarios_estatus item);// CREATE
        //Task<IEnumerable<cat_generales>> FicMetGetListRhCatGenerales(int id);//GET GENERALES
       // Task<IEnumerable<cat_institutos>> FicMetGetListRhCatInstitutos();//GET INSTITUTOS
    }
}
