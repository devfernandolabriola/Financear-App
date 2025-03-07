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

    public string Locale { get; set; } = null!;
    public Moneda ()
        {

        }

    public Moneda(int id, string nombre, string locale)
    {
        Id = id;
        Nombre = nombre;
        Locale = locale;
    }

    public static List<Moneda> VerMonedas()
    {
        FinanzasAppContext context = new FinanzasAppContext();
        var moneda = context.Monedas.ToList();
        return moneda;
    }

    public static Moneda GetMonedaXId(int moneda)
    {
        FinanzasAppContext context = new FinanzasAppContext();
        return context.Monedas.FirstOrDefault(m => m.Id == moneda);
    }
    public static Moneda GetMonedaXNombre(string moneda)
    {
        FinanzasAppContext context = new FinanzasAppContext();
        return context.Monedas.FirstOrDefault(m => EF.Functions.Like(m.Nombre, moneda));
    }
}
