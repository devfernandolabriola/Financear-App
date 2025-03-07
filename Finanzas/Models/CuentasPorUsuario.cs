using Finanzas.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Finanzas.Controllers;


namespace Finanzas.Models;

public partial class CuentasPorUsuario
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public int IdCuenta { get; set; }

    public int IdMoneda { get; set; }

    public string MontoTotal { get; set; }


    public static bool AgregarCuentaAUsuario(FinanzasAppContext context, int userid, int cuentaid, int monedaid, string montoTotal)
    {
        try
        {
            context.Database.BeginTransaction();
            context.Database.ExecuteSqlRaw($"Insert into CuentasPorUsuario(IdUsuario,IdCuenta,IdMoneda,MontoTotal) VALUES('{userid}','{cuentaid}','{monedaid}','{montoTotal}')");
            context.Database.CommitTransaction();
            return true;
        }
        catch (Exception)
        {
            context.Database.RollbackTransaction();
            return false;
            
        }
    }

    public static List<CuentasPorUsuario> VerCuentasPorUsuario(int userId, int monedaId)
    {
        FinanzasAppContext context = new FinanzasAppContext();

        //TODO: Retornar unicamente los nombres de las cunetas por usuario
        return context.CuentasPorUsuarios.Where(x => (x.IdUsuario == userId) && (x.IdMoneda == monedaId)).ToList();
    }

    public static List<CuentasPorUsuario> VerCuentasPorUsuario(int userId)
    {
        FinanzasAppContext context = new FinanzasAppContext();

        //TODO: Retornar unicamente los nombres de las cunetas por usuario
        return context.CuentasPorUsuarios.Where(x => (x.IdUsuario == userId)).ToList();
    }

    public static CuentasPorUsuario GetCuentasPorUsuario(int CXU)
    {
        FinanzasAppContext context = new FinanzasAppContext();
        return context.CuentasPorUsuarios.FirstOrDefault(c => (c.Id == CXU));
    }

    public static int? BuscarCXUId(FinanzasAppContext context, int userId, int cuentaId, int monedaId)
    {
        var CXUId = context.CuentasPorUsuarios.FirstOrDefault(c => (c.IdMoneda == monedaId) && (c.IdUsuario == userId) && (c.IdCuenta == cuentaId));
        return CXUId.Id; 
    }

    public static bool HacerMovimiento(FinanzasAppContext context, int CXUId, int Accion, string Monto)
    {
        var DineroCuenta = context.CuentasPorUsuarios.FirstOrDefault(c => c.Id == CXUId).MontoTotal;

        if(Accion == 1)
        {
            var DineroActual = Convert.ToDouble(DineroCuenta) + Convert.ToDouble(Monto);
            try
            {
                context.Database.BeginTransaction();
                context.Database.ExecuteSqlRaw($"Update CuentasPorUsuario SET MontoTotal = '{DineroActual.ToString()}' WHERE Id = {CXUId};");
                context.Database.CommitTransaction();
                return true;
            }
            catch (Exception)
            {
                context.Database.RollbackTransaction();
                return false;

            }
        } else
        {
            var DineroActual = Convert.ToDouble(DineroCuenta) - Convert.ToDouble(Monto);
            try
            {
                context.Database.BeginTransaction();
                context.Database.ExecuteSqlRaw($"Update CuentasPorUsuario SET MontoTotal = '{DineroActual.ToString()}' WHERE Id = {CXUId};");
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
}
