namespace Finanzas.DTO
{
    public class VincularMovimientoDTO
    {
        public required string NombreMovimiento { get; set; }
        public required string TipoAccion { get; set; }
        public required string monedaId { get; set; }
        public required string CuentaId { get; set; }
        public required DateTime fecha { get; set; }
        public required string CategoriaId { get; set; }
        public required string Monto { get; set; }
    }
}
