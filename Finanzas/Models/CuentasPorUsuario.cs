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

    public static List<CuentasPorUsuario> VerCuentasPorUsuario(int userId, int monedaid)
    {
        FinanzasAppContext context = new FinanzasAppContext();

        //TODO: Retornar unicamente los nombres de las cunetas por usuario
        return context.CuentasPorUsuarios.Where(x => (x.IdUsuario == userId) && (x.IdMoneda == monedaid)).ToList();
    }
}
