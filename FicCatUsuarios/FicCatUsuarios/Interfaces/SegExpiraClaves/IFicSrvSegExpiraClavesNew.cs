using System;
using System.Collections.Generic;
using System.Text;
using FicCatUsuarios.Models.Usuarios;
using System.Threading.Tasks;
using FicCatUsuarios.Data;
namespace FicCatUsuarios.Interfaces.SegExpiraClaves
{
    public interface IFicSrvSegExpiraClavesNew
    {
        Task FicMetGetNewRhCatTelefonos(seg_expira_claves item);// CREATE
    }
}


