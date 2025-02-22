using Finanzas.Models.Context;
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
}
