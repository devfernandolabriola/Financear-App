using System;
using System.Collections.Generic;

namespace Finanzas.Models;

public partial class Movimiento
{
    public int Id { get; set; }

    public int TipoMovimientoId { get; set; }

    public DateTime Fecha { get; set; }

    public int IdCategoria { get; set; }

    public virtual ICollection<CuentasPorUsuario> CuentasPorUsuarios { get; set; } = new List<CuentasPorUsuario>();

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual TipoMovimiento TipoMovimiento { get; set; } = null!;
}
