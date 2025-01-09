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

    public static Usuario? LoginUser(FinanzasAppContext context,string Email, string Clave)
    {
        var usuario = context.Usuarios.FirstOrDefault(x => x.Email == Email);
        bool datosCorrectos = false;
        if (usuario != null)
        {
            if (CompararClave(HashHelper.HashPassword(Clave), usuario.Clave))
            {
                datosCorrectos = true;
                return usuario;
            } else
            {
                return null;
            }
            
        }
        return usuario;

    }

    private static bool CompararClave(string ClaveForm, string ClaveDb)
    {
        return ClaveForm == ClaveDb;
    }
}
