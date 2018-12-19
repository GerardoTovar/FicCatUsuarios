using System;
using System.Collections.Generic;
using System.Text;
using FicCatUsuarios.Models.Usuarios;
using System.Threading.Tasks;
using FicCatUsuarios.Data;
namespace FicCatUsuarios.Interfaces.SegUsuariosEstatus
{
    public interface IFicSrvSegUsuariosEstatusUpdate
    {
        Task FicMetUpdateEdificio(seg_usuarios_estatus ficPa_eva_Cat_Edificios);
    }
}
