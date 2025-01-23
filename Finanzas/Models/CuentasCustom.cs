using Finanzas.Models.Context;

namespace Finanzas.Models
{
    public class CuentasCustom
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public void VerCuentasCustom(FinanzasAppContext context)
        {
            context.CuentasCustom.ToList();
        }
    }
}
