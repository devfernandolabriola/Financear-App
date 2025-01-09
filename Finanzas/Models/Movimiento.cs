using System;
using System.Collections.Generic;

namespace Finanzas.Models;

public partial class Movimiento
{
    public int Id { get; set; }

    public int TipoMovimientoId { get; set; }

    public DateTime Fecha { get; set; }

    public int IdCategoria { get; set; }
}
