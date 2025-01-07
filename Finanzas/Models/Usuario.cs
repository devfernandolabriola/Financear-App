using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Finanzas.Models.Context;

namespace Finanzas.Models;

public partial class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Configura el campo como autoincremental
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(12, ErrorMessage = "El nombre no puede exceder los 12 caracteres.")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El mail es obligatorio.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "La clave es obligatoria.")]
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
            context.Usuarios.Add(new Usuario { Email = email, Nombre = nombre, Clave = clave });
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        } 
    }
}
