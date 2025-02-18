using Finanzas.Models.Context;
using System;
using System.Collections.Generic;

namespace Finanzas.Models;

public partial class Categoria
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;


    public static List<Categoria> VerCategorias()
    {
        FinanzasAppContext context = new FinanzasAppContext();
        var Categorias = context.Categorias.ToList();
        return Categorias;
    }
}
