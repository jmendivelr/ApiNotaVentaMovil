namespace RESTAPI_CORE.Modelos
{
    public class Impresion
    {
        public int idcotizacion { get; set; }
        public string codcliente { get; set; }
        public string nombrecliente { get; set; }
        public string dircliente { get; set; }
        public string fecha { get; set; }
        public string item { get; set; }
        public string Cod { get; set; }
        public string MarcaPrecio { get; set; }
        public string NomArticulo { get; set; }
        public string UND { get; set; }
        public int cant { get; set; }
        public decimal Precio { get; set; }
        public decimal TOTAL { get; set; }
        public string Moneda { get; set; }
        public decimal TipoCambio { get; set; }
    }
}
