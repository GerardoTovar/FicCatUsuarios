using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Microsoft.EntityFrameworkCore;
using FicCatUsuarios.Models.Usuarios;

namespace FicCatUsuarios.Data
{
    public class FicDBContext : DbContext
    {
        private readonly string FicDataBasePath;

        public DbSet<cat_usuarios> cat_usuarios { get; set; }
        public DbSet<seg_usuarios_estatus> seg_usuarios_estatus { get; set; }
        
        public DbSet<seg_expira_claves> seg_expira_claves { get; set; }

        public DbSet<rh_cat_personas> rh_cat_personas { get; set; }
        public DbSet<cat_generales> cat_generales { get; set; }
        public DbSet<cat_institutos> cat_institutos { get; set; }
        public DbSet<cat_estatus> cat_estatus { get; set; }
        public DbSet<cat_tipos_estatus> cat_tipos_estatus { get; set; }

        public FicDBContext(string FicPaDataBasePath)
        {
            FicDataBasePath = FicPaDataBasePath;
            FicMetCrearDB();
        } //CONSTRUCTOR

        public async void FicMetCrearDB()
        {
            try
            {
                //Se crea la base de datos en base el al esquema 
                //await Database.EnsureDeletedAsync();
                //Nuevo metodo básico de Entity Framework que garantiza que exite la base de datos para el contexto
                //Si no existe, no se toma ninguna acción
                await Database.EnsureCreatedAsync();
            }
            catch (Exception e) {
                await new Page().DisplayAlert("ALERTA", e.Message.ToString(), "OK");
            }
        }//ESTE METODO CREA LA BASE DE DATOS

        protected async override void OnConfiguring(DbContextOptionsBuilder FicPaOptionsBuilder) {
            try
            {
                FicPaOptionsBuilder.UseSqlite($"Filename={FicDataBasePath}");
                FicPaOptionsBuilder.EnableSensitiveDataLogging();
            }
            catch (Exception e)
            {
                await new Page().DisplayAlert("ALERTA", e.Message.ToString(), "OK");
            }
        }//CONFIGURACION DE LA CONEXION

        

        protected async override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {

                //rh_cat_personas//
                #region rh_cat_personas
                modelBuilder
                    .Entity<rh_cat_personas>()
                    .HasKey(c => new { c.IdPersona });
                modelBuilder
                    .Entity<rh_cat_personas>()
                    .HasOne(s => s.cat_generales_estados)
                    .WithMany()
                    .HasForeignKey(s => new { s.IdGenEstadoCivil, s.IdTipoGenEstadoCivil });
                modelBuilder
                    .Entity<rh_cat_personas>()
                    .HasOne(s => s.cat_institutos)
                    .WithMany()
                    .HasForeignKey(s => new { s.IdInstituto });
                modelBuilder
                    .Entity<rh_cat_personas>()
                    .HasOne(s => s.cat_generales_ocupaciones)
                    .WithMany()
                    .HasForeignKey(s => new { s.IdGenOcupacion, s.IdTipoGenOcupacion });
                
                #endregion


                //cat_usuarios
                #region cat_usuarios
                modelBuilder
                    .Entity<cat_usuarios>()
                    .HasKey(c => new { c.IdUsuario });
                modelBuilder
                    .Entity<cat_usuarios>()
                    .HasOne(s => s.rh_cat_personas)
                    .WithMany()
                    .HasForeignKey(s => new { s.IdPersona});

                #endregion

                //seg_expira_claves
                #region seg_expira_claves
                modelBuilder
                    .Entity<seg_expira_claves>()
                    .HasKey(c => new { c.IdUsuario, c.IdClave });
                modelBuilder
                    .Entity<seg_expira_claves>()
                    .HasOne(s => s.cat_usuarios)
                    .WithMany()
                    .HasForeignKey(s => new { s.IdUsuario});
                #endregion

                //seg_usuarios_estatus
                #region seg_usuarios_estatus
                modelBuilder
                    .Entity<seg_usuarios_estatus>()
                    .HasKey(c => new { c.IdCrtlEstatus, c.IdUsuario});
                modelBuilder
                    .Entity<seg_usuarios_estatus>()
                    .HasOne(s => s.cat_usuarios)
                    .WithMany()
                    .HasForeignKey(s => new { s.IdUsuario });
                modelBuilder
                    .Entity<seg_usuarios_estatus>()
                    .HasOne(s => s.cat_estatus)
                    .WithMany()
                    .HasForeignKey(s => new { s.IdEstatus, s.IdTipoEstatus});               
                #endregion


                //===========================================catalogos==========================================//



                //cat_institutos
                #region cat_institutos
                modelBuilder
                    .Entity<cat_institutos>()
                    .HasKey(c => new { c.IdInstituto });
                modelBuilder
                    .Entity<cat_institutos>()
                    .HasOne(s => s.cat_Institutos)
                    .WithMany()
                    .HasForeignKey(s => new { s.IdInstitutoPadre });
                modelBuilder
                    .Entity<cat_institutos>()
                    .HasOne(s => s.cat_generales_giro)
                    .WithMany()
                    .HasForeignKey(s => new { s.IdGenGiro, s.IdTipoGenGiro });
                #endregion


                //cat_tipos_generales
                #region cat_tipos_generales
                modelBuilder
                    .Entity<cat_tipos_generales>()
                    .HasKey(c => new { c.IdTipoGeneral });
                #endregion


                //cat_generales
                modelBuilder.Entity<cat_generales>()
                    .HasKey(c => new { c.IdGeneral, c.IdTipoGeneral });
                modelBuilder
                   .Entity<cat_generales>()
                   .HasOne(s => s.cat_tipos_generales)
                   .WithMany()
                   .HasForeignKey(s => new { s.IdTipoGeneral });

                //cat_tipos_estatus
                #region
                modelBuilder
                    .Entity<cat_tipos_estatus>()
                    .HasKey(c => new { c.IdTipoEstatus });
                #endregion

                //cat_estatus
                #region
                modelBuilder
                    .Entity<cat_estatus>()
                    .HasKey(c => new { c.IdEstatus, c.IdTipoEstatus });
                modelBuilder
                    .Entity<cat_estatus>()
                    .HasOne(s => s.cat_tipos_estatus)
                    .WithMany()
                    .HasForeignKey(s => new { s.IdTipoEstatus });
                #endregion
               
            }
            catch (Exception e)
            {
                await new Page().DisplayAlert("ALERTA", e.Message.ToString(), "OK");
            }

        }//AL CREAR EL MODELO
    }
}
