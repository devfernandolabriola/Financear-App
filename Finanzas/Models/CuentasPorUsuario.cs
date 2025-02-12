using Finanzas.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Finanzas.Models;

public partial class CuentasPorUsuario
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public int IdCuenta { get; set; }

    public int IdMoneda { get; set; }

    public string MontoTotal { get; set; }


    public static bool AgregarCuentaAUsuario(FinanzasAppContext context, int userid, int cuentaid, int monedaid)
    {
        try
        {
            context.Database.BeginTransaction();
            context.Database.ExecuteSqlRaw($"Insert into CuentasPorUsuario(IdUsuario,IdCuenta,IdMoneda,MontoTotal) VALUES('{userid}','{cuentaid}','{monedaid}','0')");
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
