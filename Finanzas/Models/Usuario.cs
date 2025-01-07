using System;
using System.Collections.Generic;

namespace Finanzas.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();

    public virtual ICollection<CuentasPorUsuario> CuentasPorUsuarios { get; set; } = new List<CuentasPorUsuario>();

    public Usuario() { 
   
    }
    public Usuario(string email, string nombre, string clave)
    {
        Email = email;
        Nombre = nombre;
        Clave = clave;
    }

    public void RegisterUser(string email, string nombre, string clave)
    {
        
    }
}
