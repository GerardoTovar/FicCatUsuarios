using System;
using System.Collections.Generic;
using System.Text;
using FicCatUsuarios.Models.Usuarios;
using System.Threading.Tasks;
using FicCatUsuarios.Data;
namespace FicCatUsuarios.Interfaces.SegExpiraClaves
{
    public interface IFicSrvSegExpiraClavesUpdate
    {
        Task FicMetUpdateEdificio(seg_expira_claves ficPa_eva_Cat_Edificios);
    }
}

