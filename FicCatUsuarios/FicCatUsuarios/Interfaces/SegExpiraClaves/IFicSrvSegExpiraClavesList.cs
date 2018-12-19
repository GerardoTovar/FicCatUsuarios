using FicCatUsuarios.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FicCatUsuarios.Interfaces.SegExpiraClaves
{
    public interface IFicSrvSegExpiraClavesList
    {
        Task<IEnumerable<seg_expira_claves>> FicMetGetListSegExpiraClaves(int id);//READ
    }
}
