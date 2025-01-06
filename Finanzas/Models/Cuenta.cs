using System;
using System.Collections.Generic;

namespace Finanzas.Models;

public partial class Cuenta
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdCreador { get; set; }

    public virtual ICollection<CuentasPorUsuario> CuentasPorUsuarios { get; set; } = new List<CuentasPorUsuario>();

    public virtual Usuario IdCreadorNavigation { get; set; } = null!;
}
