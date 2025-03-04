using Finanzas.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Finanzas.Models;

public partial class Movimiento
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public TipoAccion EnumAccion { get; set; }

    public DateTime Fecha { get; set; }

    public int IdCategoria { get; set; }
    public int IdCuentaXUsuario { get; set; }

    public string Monto { get; set; }
    public List<Movimiento> VerMovimientos()
    {
        FinanzasAppContext context = new FinanzasAppContext();
        var Movimientos = context.Movimientos.ToList();
        return Movimientos;
    }

    public static bool AgregarMovimiento(FinanzasAppContext context, string NombreMovimiento, int TipoAccion, int CXUId, DateTime Fecha, string Monto, int CategoriaId)
    {
        try
        {
            context.Database.BeginTransaction();
            context.Database.ExecuteSqlRaw($"Set DateFormat dmy;");
            context.Database.ExecuteSqlRaw($"insert into Movimientos (Nombre, TipoAccion, Fecha, IdCategoria, IdCXU, Monto) VALUES ('{NombreMovimiento}','{TipoAccion}','{Fecha}','{CategoriaId}','{CXUId}','{Monto}');");
            context.Database.CommitTransaction();
            return true;
        }
        catch (Exception)
        {
            context.Database.RollbackTransaction();
            return false;

        }
    }

    public static bool EliminarMovimiento(FinanzasAppContext context, string NombreMovimiento, int TipoAccion, int CXUId, DateTime Fecha, string Monto, int CategoriaId)
    {
        try
        {
            context.Database.BeginTransaction();
            context.Database.ExecuteSqlRaw($"Set DateFormat dmy;");
            context.Database.ExecuteSqlRaw($"DELETE From Movimientos WHERE Nombre = '{NombreMovimiento}' AND TipoAccion = {TipoAccion} AND Fecha = '{Fecha}' AND IdCategoria = {CategoriaId} AND IdCXU = {CXUId} AND Monto = '{Monto}'; ");
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
