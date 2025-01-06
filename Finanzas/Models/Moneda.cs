using System;
using System.Collections.Generic;

namespace Finanzas.Models;

public partial class Moneda
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<CuentasPorUsuario> CuentasPorUsuarios { get; set; } = new List<CuentasPorUsuario>();
}
