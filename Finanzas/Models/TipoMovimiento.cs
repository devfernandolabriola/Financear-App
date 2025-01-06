using System;
using System.Collections.Generic;

namespace Finanzas.Models;

public partial class TipoMovimiento
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
