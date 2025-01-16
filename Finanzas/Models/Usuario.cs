using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Finanzas.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Finanzas.Models;

public partial class Usuario
{

    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(12, ErrorMessage = "El nombre no puede exceder los 12 caracteres.")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El mail es obligatorio.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "La clave es obligatoria.")]
    [MinLength(8,ErrorMessage = "Minimo 8 caracteres capo")]
    public string Clave { get; set; } = null!;
    
    public Usuario() { 
   
    }
    public Usuario(string email, string nombre, string clave)
    {
        Email = email;
        Nombre = nombre;
        Clave = clave;
    }

    public static bool RegisterUser(FinanzasAppContext context, string email, string nombre, string clave)
    {

        try
        {
            context.Database.BeginTransaction();
            context.Database.ExecuteSqlRaw($"Insert into Usuarios(Nombre, Email, Clave) VALUES('{nombre}', '{email}', '{clave}')");
            context.Database.CommitTransaction();
            return true;
        }
        catch (Exception ex)
        {
            context.Database.RollbackTransaction();
            return false;
        } 
    }

    public static Usuario? LoginUser(FinanzasAppContext context, string username, string Clave)
    {
        var usuario = context.Usuarios.FirstOrDefault(x => x.Nombre == username);
        if (usuario != null)
        {
            if (HashHelper.HashPassword(Clave) == usuario.Clave)
            {
                return usuario;
            } else
            {
                return null;
            }
            
        }
        return usuario;

    }

    public static int VerificarDatosRegister(FinanzasAppContext context, string Email, string nombre)
    {
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(nombre))
        {
            return 400;
        }
        var usuario = context.Usuarios.FirstOrDefault(x => x.Email == Email || x.Nombre == nombre);
        if (usuario != null)
        {
            return 422;
        }
        return 200;
    }

    public static List<(int IdMoneda, decimal MontoTotal, string cuentaNombre)> DevuelvoMontoTotalXCuenta(FinanzasAppContext context, int IdUsuario)
    {
        return context.CuentasPorUsuarios
            .Where(x => x.IdUsuario == IdUsuario)
            .Join(
                context.Cuentas, // The table to join
                cuentaUsuario => cuentaUsuario.IdCuenta, // Key in CuentasPorUsuarios
                cuenta => cuenta.Id, // Key in Cuentas
                (cuentaUsuario, cuenta) => new // Select both tables
                {
                    cuentaUsuario.IdMoneda,
                    cuentaUsuario.MontoTotal,
                    CuentaNombre = cuenta.Nombre // Assuming 'Nombre' is the column name in Cuentas
                }
            )
            .AsEnumerable() // Switch to client-side for parsing
            .GroupBy(c => new { c.IdMoneda, c.CuentaNombre }) // Group by IdMoneda and CuentaNombre
            .Select(group =>
            {
                decimal totalMonto = group.Sum(c =>
                {
                    decimal monto;
                    return decimal.TryParse(c.MontoTotal, out monto) ? monto : 0;
                });

                return (IdMoneda: group.Key.IdMoneda, MontoTotal: totalMonto, CuentaNombre: group.Key.CuentaNombre);
            })
            .ToList(); // Convert to list

    }

    internal static bool VerificarCuentasVinculadasUsuario(FinanzasAppContext context, int v)
    {
        return context.CuentasPorUsuarios.Any(x => x.IdUsuario == v);
    }
}
