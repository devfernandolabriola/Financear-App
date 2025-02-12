using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Finanzas.Models.Context;

public partial class FinanzasAppContext : DbContext
{
    public FinanzasAppContext()
    {
    }

    public FinanzasAppContext(DbContextOptions<FinanzasAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cuenta> Cuentas { get; set; }

    public virtual DbSet<CuentasPorUsuario> CuentasPorUsuarios { get; set; }

    public virtual DbSet<Moneda> Monedas { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
                //=> optionsBuilder.UseSqlServer("Data Source=NEAR;Initial Catalog=FinanzasApp;Integrated Security=True;TrustServerCertificate=true");
                => optionsBuilder.UseSqlServer("Data Source=DESKTOP-IALGCQJ;Initial Catalog=FinanzasApp;Integrated Security=True;TrustServerCertificate=true");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3213E83F599A700C");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cuentas__3214EC07FD2FADB5");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<CuentasPorUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CuentasP__3214EC079D87A7D8");

            entity.ToTable("CuentasPorUsuario");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Moneda>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Monedas__3213E83FA8FBA932");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Nombre).HasColumnType("text");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movimien__3213E83F752F5AE2");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC071C1875CD");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
