using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finanzas.DTO
{
    public class VincularCuentaXUsuarioDTO
    {
        public required string NombreCuenta { get; set; }
        public required string Moneda { get; set; }
        public required string MontoTotal { get; set; }
    }
}

