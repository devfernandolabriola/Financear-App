using System;
using System.Collections.Generic;

namespace Finanzas.Models;

public partial class Cuenta
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdCreador { get; set; }
}
