using FicCatUsuarios.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FicCatUsuarios.Interfaces.SegUsuariosEstatus
{
    public interface IFicSrvSegUsuariosEstatusList
    {
        Task<IEnumerable<seg_usuarios_estatus>> FicMetGetListSegExpiraClaves(int id);//READ
        Task<IEnumerable<cat_estatus>> FicMetGetListRhCatEstatus(int id);//GET ESTATUS
        Task<IEnumerable<cat_tipos_estatus>> FicMetGetListRhCatTipoEstatus(int id);//GET TIPO ESTATUS
    }
}

