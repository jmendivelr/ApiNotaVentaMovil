namespace RESTAPI_CORE.Modelos
{
    public class Cotizacion
    {
            public int ID { get; set; }
            public int idcotizacion { get; set; }
            public string codcliente { get; set; }
            public string Cliente { get; set; }
            public string dircliente { get; set; }
            public string Moneda { get; set; }
            public string item { get; set; }
            public int cant { get; set; }
            public string Cod { get; set; }
            public string NomArticulo { get; set; }

            public string fecha { get; set; }
   
            public double Precio { get; set; }
            public string MarcaPrecio { get; set; }

            public string RUCD { get; set; }
            public string NomDealer { get; set; }
            public string fecD { get; set; }
            public double PrecioD { get; set; }
            public string MarcaD { get; set; }
            public string ObsD { get; set; }

            public string RUCP1 { get; set; }
            public string NomP1 { get; set; }
            public string FecP1 { get; set; }
            public double PrecioP1 { get; set; }
            public string MarcaP1 { get; set; }
            public string ObsP1 { get; set; }

            public string RUCP2 { get; set; }
            public string NomP2 { get; set; }
            public string FecP2 { get; set; }
            public double PrecioP2 { get; set; }
            public string MarcaP2 { get; set; }
            public string ObsP2 { get; set; }

            public string RUCP3 { get; set; }
            public string NomP3 { get; set; }
            public string FecP3 { get; set; }
            public double PrecioP3 { get; set; }
            public string MarcaP3 { get; set; }
            public string ObsP3 { get; set; }

            public string RUCP4 { get; set; }
            public string NomP4 { get; set; }
            public string FecP4 { get; set; }
            public double PrecioP4 { get; set; }
            public string MarcaP4 { get; set; }
            public string ObsP4 { get; set; }

            public string usuario { get; set; }

            public double KVVenta { get; set; }
            public string KCVenta { get; set; }
            public string KFVenta { get; set; }

            public double KVCotizacion { get; set; }
            public string KCCotizacion { get; set; }
            public string KFCotizacion { get; set; }

            public double KVNotaIngreso { get; set; }
            public string KCNotaIngreso { get; set; }
            public string KFNotaIngreso { get; set; }

    }
}
