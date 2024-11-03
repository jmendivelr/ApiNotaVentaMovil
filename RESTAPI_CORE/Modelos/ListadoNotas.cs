namespace RESTAPI_CORE.Modelos
{
    public class ListadoNotas
    { // Almacén
        public string CAALMA { get; set; }

        // Tipo de Documento
        public string CATD { get; set; }

        // Número de Documento
        public string CANUMDOC { get; set; }

        // Código del Proveedor
        public string CACODPRO { get; set; }

        // Glosa (Descripción)
        public string CAGLOSA { get; set; }

        // Nombre del Proveedor
        public string PRVCNOMBRE { get; set; }

        // Cantidad de Items
        public int Cantidad { get; set; }

        // Fecha del Documento
        public DateTime CAFECDOC { get; set; }
    }
}
