using Finanzas.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Web.Helpers;

namespace Finanzas.Models;

public partial class Cuenta
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public static int? BuscarCuentaId(FinanzasAppContext context, string NombreCuenta)
    {
        var Cuenta = context.Cuentas.FirstOrDefault(c => c.Nombre.ToUpper() == NombreCuenta.ToUpper());
        if(Cuenta == null)
        {
            return null;
        }
        return Cuenta.Id;
    }
    public static string BuscarCuentaXNombre(FinanzasAppContext context, int idCuenta)
    {
        return context.Cuentas.FirstOrDefault(c => c.Id == idCuenta).Nombre;
    }

    public static bool AgregarCuenta(FinanzasAppContext context, string nombre)
    {
        try
        {
            context.Database.BeginTransaction();
            context.Database.ExecuteSqlRaw($"Insert into Cuentas(Nombre) VALUES('{nombre}')");
            context.Database.CommitTransaction();
            return true;
        }
        catch (Exception)
        {
            context.Database.RollbackTransaction();
            return false;
            
        }
    }
}

