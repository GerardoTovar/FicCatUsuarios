using FicCatUsuarios.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FicCatUsuarios.Interfaces.CatUsuarios
{
    public interface IFicSrvCatUsuariosList
    {
        Task<IEnumerable<cat_usuarios>> FicMetGetListCatUsuarios();//READ
    }
}
