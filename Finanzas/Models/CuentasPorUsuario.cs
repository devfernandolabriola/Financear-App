using System;
using System.Collections.Generic;

namespace Finanzas.Models;

public partial class CuentasPorUsuario
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public int IdCuenta { get; set; }

    public int IdMoneda { get; set; }

    public int IdMovimiento { get; set; }

    public virtual Cuenta IdCuentaNavigation { get; set; } = null!;

    public virtual Moneda IdMonedaNavigation { get; set; } = null!;

    public virtual Movimiento IdMovimientoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
