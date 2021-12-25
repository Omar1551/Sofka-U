using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace JuegoPreguntas.Models.DB
{
    public partial class Prueba_SofkaContext : DbContext
    {
        public Prueba_SofkaContext()
        {
        }

        public Prueba_SofkaContext(DbContextOptions<Prueba_SofkaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorium> Categoria { get; set; }
        public virtual DbSet<Participante> Participantes { get; set; }
        public virtual DbSet<Pregunta> Preguntas { get; set; }
        public virtual DbSet<Respuesta> Respuestas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-S74V9542\\SQLEXPRESS; Database=Prueba_Sofka; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.Property(e => e.IdCategoria).HasColumnName("Id_Categoria");

                entity.Property(e => e.ValPremioNivel)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("Val_Premio_Nivel");
            });

            modelBuilder.Entity<Participante>(entity =>
            {
                entity.HasKey(e => e.IdParticipante);

                entity.Property(e => e.IdParticipante).HasColumnName("Id_Participante");

                entity.Property(e => e.NombrePartcipante)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_Partcipante");

                entity.Property(e => e.Pregunta1).HasColumnName("Pregunta_1");

                entity.Property(e => e.Pregunta2).HasColumnName("Pregunta_2");

                entity.Property(e => e.Pregunta3).HasColumnName("Pregunta_3");

                entity.Property(e => e.Pregunta4).HasColumnName("Pregunta_4");

                entity.Property(e => e.Pregunta5).HasColumnName("Pregunta_5");

                entity.Property(e => e.Respuesta1).HasColumnName("Respuesta_1");

                entity.Property(e => e.Respuesta2).HasColumnName("Respuesta_2");

                entity.Property(e => e.Respuesta3).HasColumnName("Respuesta_3");

                entity.Property(e => e.Respuesta4).HasColumnName("Respuesta_4");

                entity.Property(e => e.Respuesta5).HasColumnName("Respuesta_5");

                entity.Property(e => e.TotalPremio)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("Total_Premio");
            });

            modelBuilder.Entity<Pregunta>(entity =>
            {
                entity.HasKey(e => e.IdPregunta);

                entity.Property(e => e.IdPregunta).HasColumnName("Id_Pregunta");

                entity.Property(e => e.DescPregunta)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Desc_Pregunta");

                entity.Property(e => e.IdCategoria).HasColumnName("Id_Categoria");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Pregunta)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Preguntas_Categoria");
            });

            modelBuilder.Entity<Respuesta>(entity =>
            {
                entity.HasKey(e => e.IdRespuesta);

                entity.Property(e => e.IdRespuesta).HasColumnName("Id_Respuesta");

                entity.Property(e => e.DescRespuesta)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Desc_Respuesta");

                entity.Property(e => e.IdPregunta).HasColumnName("Id_Pregunta");

                entity.Property(e => e.RespCorrecta).HasColumnName("Resp_Correcta");

                entity.HasOne(d => d.IdPreguntaNavigation)
                    .WithMany(p => p.Respuesta)
                    .HasForeignKey(d => d.IdPregunta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Respuestas_Preguntas");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
