namespace ClientPracticaEDEGA2024.Models
{
    public class AutoModel
    {
        public int ?au_id { get; set; }
        public string? au_vin { get; set; }
        public string? au_color { get; set; }
        public decimal? au_precio { get; set; }
        public string? au_estado { get; set; }
        public int ?au_kilometraje { get; set; }
        public int ?au_anio_fabricacion { get; set; }
        public string ?au_fecha_ingreso { get; set; }
        public int ?au_estatus { get; set; }
        public int ?ma_id { get; set; }
        public string ?ma_nombre { get; set; }
        public int? mo_id { get; set; }
        public string ?mo_nombre { get; set; }
        public int ?su_id { get; set; }
        public string ?su_nombre { get; set; }
        public List<object>? Autos { get; set; }
    }
}
