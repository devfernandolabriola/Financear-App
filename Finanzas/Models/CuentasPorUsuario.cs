using System;
using System.Collections.Generic;

namespace Finanzas.Models;

public partial class CuentasPorUsuario
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public int IdCuenta { get; set; }

    public int IdMoneda { get; set; }

    public string MontoTotal { get; set; }

    public bool IsCuentaCustom { get; set; }
}
