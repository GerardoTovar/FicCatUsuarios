using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FicCatUsuarios.Models.Usuarios
{
    public class cat_usuarios
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdUsuario { get; set; }
        public int IdPersona { get; set; }
        public rh_cat_personas rh_cat_personas { get; set; }
        [StringLength(1)]
        public string Expira { get; set; }
        [StringLength(1)]
        public string Conectado { get; set; }
        public DateTime FechaAlta { get; set; }
        public Int16 NumIntentos { get; set; }
        [StringLength(50)]
        public string Usuario { get; set; }

        [StringLength(1)]
        public string Activo { get; set; }
        public Nullable<DateTime> FechaReg { get; set; }
        [StringLength(20)]
        public string UsuarioReg { get; set; }
        public Nullable<DateTime> FechaUltMod { get; set; }
        [StringLength(20)]
        public string UsuarioMod { get; set; }
        [StringLength(1)]
        public string Borrado { get; set; }

    }

    public class seg_expira_claves
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        
        public int IdClave { get; set; }
        public int IdUsuario { get; set; }
        public cat_usuarios cat_usuarios { get; set; }
        public DateTime FechaExpiraIni { get; set; }
        public DateTime FechaExpiraFin { get; set; }
        [StringLength(1)]
        public string Actual { get; set; }
        [StringLength(1)]
        public string ClaveAutoSys { get; set; }
        [StringLength(50)]
        public string Clave { get; set; }

        [StringLength(1)]
        public string Activo { get; set; }
        public Nullable<DateTime> FechaReg { get; set; }
        [StringLength(20)]
        public string UsuarioReg { get; set; }                       
        [StringLength(1)]
        public string Borrado { get; set; }
    }

    public class seg_usuarios_estatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]        
        public int IdCrtlEstatus { get; set; }
        public int IdUsuario { get; set; }
        public cat_usuarios cat_usuarios { get; set; }
        public DateTime FechaEstatus { get; set; }
        public Int16 IdTipoEstatus { get; set; }        
        public Int16 IdEstatus { get; set; }
        public cat_estatus cat_estatus { get; set; }
        [StringLength(500)]
        public string Observacion { get; set; }
        [StringLength(1)]
        public string Actual { get; set; }

        [StringLength(1)]
        public string Activo { get; set; }
        public Nullable<DateTime> FechaReg { get; set; }
        [StringLength(20)]
        public string UsuarioReg { get; set; }
        [StringLength(1)]
        public string Borrado { get; set; }
    }

    public class seg_usuarios_estatus2
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCrtlEstatus { get; set; }
        public int IdUsuario { get; set; }
        public cat_usuarios cat_usuarios { get; set; }
        public DateTime FechaEstatus { get; set; }
        [StringLength(100)]
        public string Estatus { get; set; }
        public Int16 IdEstatus { get; set; }
        [StringLength(100)]
        public string TipoEstatus { get; set; }
        public Int16 IdTipoEstatus { get; set; }
        [StringLength(500)]
        public string Observacion { get; set; }
        [StringLength(1)]
        public string Actual { get; set; }

        [StringLength(1)]
        public string Activo { get; set; }
        public Nullable<DateTime> FechaReg { get; set; }
        [StringLength(20)]
        public string UsuarioReg { get; set; }
        [StringLength(1)]
        public string Borrado { get; set; }
    }




    //===========================================catalogos================================================//



    public class rh_cat_personas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdPersona { get; set; }
        public Nullable<Int16> IdInstituto { get; set; }
        public cat_institutos cat_institutos { get; set; }
        [StringLength(20)]
        public string NumControl { get; set; }
        [StringLength(100)]
        public string Nombre { get; set; }
        [StringLength(60)]
        public string ApPaterno { get; set; }
        [StringLength(60)]
        public string ApMaterno { get; set; }
        [StringLength(15)]
        public string RFC { get; set; }
        [StringLength(25)]
        public string CURP { get; set; }
        public Nullable<DateTime> FechaNac { get; set; }
        [StringLength(1)]
        public string TipoPersona { get; set; }
        [StringLength(1)]
        public string Sexo { get; set; }
        [StringLength(255)]
        public string RutaFoto { get; set; }
        [StringLength(20)]
        public string Alias { get; set; }
        public Nullable<Int16> IdTipoGenOcupacion { get; set; }
        public Nullable<Int16> IdGenOcupacion { get; set; }
        public cat_generales cat_generales_ocupaciones { get; set; }
        public Nullable<Int16> IdTipoGenEstadoCivil { get; set; }
        public Nullable<Int16> IdGenEstadoCivil { get; set; }
        public cat_generales cat_generales_estados { get; set; }
        [StringLength(1)]
        public string Activo { get; set; }
        public Nullable<DateTime> FechaReg { get; set; }
        [StringLength(20)]
        public string UsuarioReg { get; set; }
        public Nullable<DateTime> FechaUltMod { get; set; }
        [StringLength(20)]
        public string UsuarioMod { get; set; }
        [StringLength(1)]
        public string Borrado { get; set; }
    }



    public class cat_institutos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int16 IdInstituto { get; set; }
        [StringLength(50)]
        public string DesInstituto { get; set; }
        [StringLength(20)]
        public string Alias { get; set; }
        [StringLength(1)]
        public string Matriz { get; set; }
        public Nullable<Int16> IdInstitutoPadre { get; set; }
        public cat_institutos cat_Institutos { get; set; }        
        public Nullable<Int16> IdTipoGenGiro { get; set; }
        public Nullable<Int16> IdGenGiro { get; set; }
        public cat_generales cat_generales_giro { get; set; }

        [StringLength(1)]
        public string Activo { get; set; }
        public Nullable<DateTime> FechaReg { get; set; }
        [StringLength(20)]
        public string UsuarioReg { get; set; }
        public Nullable<DateTime> FechaUltMod { get; set; }
        [StringLength(20)]
        public string UsuarioMod { get; set; }
        [StringLength(1)]
        public string Borrado { get; set; }
    }


    public class cat_tipos_generales
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int16 IdTipoGeneral { get; set; }
        [StringLength(100)]
        public string DesTipo { get; set; }

        [StringLength(1)]
        public string Activo { get; set; }
        public Nullable<DateTime> FechaReg { get; set; }
        [StringLength(20)]
        public string UsuarioReg { get; set; }
        public Nullable<DateTime> FechaUltMod { get; set; }
        [StringLength(20)]
        public string UsuarioMod { get; set; }
        [StringLength(1)]
        public string Borrado { get; set; }
        
    }

    public class cat_generales
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]       
        public Int16 IdGeneral { get; set; }        
        public Int16 IdTipoGeneral { get; set; }
        public cat_tipos_generales cat_tipos_generales { get; set; }
        [StringLength(20)]
        public string Clave { get; set; }
        [StringLength(100)]
        public string DesGeneral { get; set; }
        [StringLength(50)]
        public string LlaveClasifica { get; set; }
        [StringLength(50)]
        public string Referencia { get; set; }

        [StringLength(1)]
        public string Activo { get; set; }
        public Nullable<DateTime> FechaReg { get; set; }
        [StringLength(20)]
        public string UsuarioReg { get; set; }
        public Nullable<DateTime> FechaUltMod { get; set; }
        [StringLength(20)]
        public string UsuarioMod { get; set; }
        [StringLength(1)]
        public string Borrado { get; set; }                
    }

    public class cat_tipos_estatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int16 IdTipoEstatus { get; set; }
        [StringLength(30)]
        public string DesTipoEstatus { get; set; }

        [StringLength(1)]
        public string Activo { get; set; }
        public Nullable<DateTime> FechaReg { get; set; }
        [StringLength(20)]
        public string UsuarioReg { get; set; }
        public Nullable<DateTime> FechaUltMod { get; set; }
        [StringLength(20)]
        public string UsuarioMod { get; set; }
        [StringLength(1)]
        public string Borrado { get; set; }
    }

    public class cat_estatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int16 IdEstatus { get; set; }
        public Int16 IdTipoEstatus { get; set; }
        public cat_tipos_estatus cat_tipos_estatus { get; set; }
        [StringLength(50)]
        public string Clave { get; set; }
        [StringLength(30)]
        public string DesEstatus { get; set; }

        [StringLength(1)]
        public string Activo { get; set; }
        public Nullable<DateTime> FechaReg { get; set; }
        [StringLength(20)]
        public string UsuarioReg { get; set; }
        public Nullable<DateTime> FechaUltMod { get; set; }
        [StringLength(20)]
        public string UsuarioMod { get; set; }
        [StringLength(1)]
        public string Borrado { get; set; }

    }



    //Clase para llenar combos o PICKERS
    public class AutoCompletePersona
    {
        public int IdPersona { get; set; }
        [StringLength(250)]
        public string NombreCompleto { get; set; }

    }//CLASE PARA LOS PICKERS ESTATICOS, TIPO PERSONA, SEXO    

}
