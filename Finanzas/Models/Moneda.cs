using Finanzas.Controllers;
using Finanzas.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;

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

    public List<Moneda> VerMonedas()
    {
        FinanzasAppContext context = new FinanzasAppContext();
        var moneda = context.Monedas.ToList();
        return moneda;
    }

    public static int? BuscarMoneda(FinanzasAppContext context, string moneda)
    {
        var resultado = context.Monedas
                        .FirstOrDefault(m => EF.Functions.Like(m.Nombre, moneda));
        return resultado?.Id;
    }
}
