namespace Modelo
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Entidades;

    public  class DBContolOficceContext : DbContext
    {
        public DBContolOficceContext()
            : base("name=DBContolOficceContext")
        {
        }

        public  DbSet<Consumibles> Consumibles { get; set; }
        public  DbSet<Documentos_enviados> Documentos_enviados { get; set; }
        public  DbSet<Documentos_internos> Documentos_internos { get; set; }
        public  DbSet<Documentos_recibidos> Documentos_recibidos { get; set; }
        public  DbSet<Electronicos> Electronicos { get; set; }
        public  DbSet<Marca_electronicos> Marca_electronicos { get; set; }
        public  DbSet<Marca_mobiliario> Marca_mobiliario { get; set; }
        public  DbSet<Mobiliarios> Mobiliarios { get; set; }
        public  DbSet<Solicitudes> Solicitudes { get; set; }
        public  DbSet<Tipo_consumible> Tipo_consumible { get; set; }
        public  DbSet<Tipo_electronico> Tipo_electronico { get; set; }
        public  DbSet<Tipo_mobiliario> Tipo_mobiliario { get; set; }
        public  DbSet<Tipo_solicitudes> Tipo_solicitudes { get; set; }
        public  DbSet<Tipos_documento> Tipos_documento { get; set; }
        public  DbSet<Tipos_documento_interno> Tipos_documento_interno { get; set; }
        public  DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Evitamos que pluralize el nombre de las identidades
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            

            #region Codigo generado por entity que no me interesa
            /*
            modelBuilder.Entity<Marca_electronicos>()
                .HasMany(e => e.Electronicos)
                .WithOptional(e => e.Marca_electronicos)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Marca_mobiliario>()
                .HasMany(e => e.Mobiliarios)
                .WithOptional(e => e.Marca_mobiliario)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Tipos_documento>()
                .HasMany(e => e.Documentos_enviados)
                .WithOptional(e => e.Tipos_documento)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Tipos_documento_interno>()
                .HasMany(e => e.Documentos_internos)
                .WithOptional(e => e.Tipos_documento_interno)
                .HasForeignKey(e => e.Id_tipo_documento)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Consumibles)
                .WithRequired(e => e.Usuarios)
                .HasForeignKey(e => e.Usuario_recibe);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Documentos_enviados)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.Usuario_registra)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Documentos_internos)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.Usuario_registra)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Documentos_recibidos)
                .WithRequired(e => e.Usuarios)
                .HasForeignKey(e => e.Usuario_recibe);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Electronicos)
                .WithRequired(e => e.Usuarios)
                .HasForeignKey(e => e.Usuario_registra);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Mobiliarios)
                .WithRequired(e => e.Usuarios)
                .HasForeignKey(e => e.Usuario_registra);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Solicitudes)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.Usuario_registra)
                .WillCascadeOnDelete();
             */
            #endregion
        }
    }
}
