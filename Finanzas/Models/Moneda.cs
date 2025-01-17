using Finanzas.Controllers;
using System;
using System.Collections.Generic;

namespace Finanzas.Models;

public partial class Moneda
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public Moneda ()
        {

        }

    public Moneda(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }

}
