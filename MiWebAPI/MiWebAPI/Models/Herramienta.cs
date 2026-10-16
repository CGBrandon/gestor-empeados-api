namespace MiWebAPI.Models
{
    public class Herramienta
    {
        public long IdHerramienta { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Marca { get; set; }
        public string? Descripcion { get; set; }
        public decimal Costo { get; set; }
        public bool Activo { get; set; }

    }
}
