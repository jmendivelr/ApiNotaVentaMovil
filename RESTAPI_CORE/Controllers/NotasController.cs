using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using RESTAPI_CORE.Modelos;

namespace RESTAPI_CORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotasController : Controller
    {

        private readonly string cadenaSQL;

        public NotasController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpPost("procesarNota")]
        public IActionResult ProcesarNota([FromBody] VentaData ventaData)
        {
            if (ventaData == null)
            {
                return BadRequest("El objeto ventaData es nulo.");
            }

            try
            {
                // Crear DataTable para FACCAB
                DataTable dtFaccab = new DataTable();
                dtFaccab.Columns.Add("CodTransacciones", typeof(string));
                dtFaccab.Columns.Add("ordenCompra", typeof(string));
                dtFaccab.Columns.Add("CodProveedor", typeof(string));
                dtFaccab.Columns.Add("CodTipoDocumento", typeof(string));
                dtFaccab.Columns.Add("correlativo", typeof(string));
                dtFaccab.Columns.Add("detalles", typeof(string));
                dtFaccab.Columns.Add("CFUSER", typeof(string));
                dtFaccab.Columns.Add("TipoNota", typeof(string));
                dtFaccab.Columns.Add("Almacen", typeof(string));

                // Añadir los datos a la tabla FACCAB
                dtFaccab.Rows.Add(ventaData.Faccab.CodTransacciones, ventaData.Faccab.ordenCompra, ventaData.Faccab.CodProveedor,
                                  ventaData.Faccab.CodTipoDocumento, ventaData.Faccab.correlativo, ventaData.Faccab.detalles,
                                  ventaData.Faccab.CFUSER, ventaData.Faccab.TipoNota, ventaData.Faccab.Almacen);

                // Crear DataTable para FACDET
                DataTable dtFacdet = new DataTable();
                dtFacdet.Columns.Add("DFSECUEN", typeof(int));
                dtFacdet.Columns.Add("DFCODIGO", typeof(string));
                dtFacdet.Columns.Add("DFCANTID", typeof(decimal));
                dtFacdet.Columns.Add("DFPREC", typeof(decimal));
                dtFacdet.Columns.Add("Almacen", typeof(string));

                // Añadir los datos a la tabla FACDET
                foreach (var detalle in ventaData.FacdetList)
                {
                    dtFacdet.Rows.Add(detalle.DFSECUEN, detalle.DFCODIGO, detalle.DFCANTID, detalle.DFPREC, detalle.Almacen);
                }

                using (SqlConnection conn = new SqlConnection(cadenaSQL))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ProcesarNota", conn))
                    {
                        List<ResultadoTransaccion> lista = new List<ResultadoTransaccion>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FACCABData", dtFaccab);
                        cmd.Parameters.AddWithValue("@FACDETData", dtFacdet);
                        using (var rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                lista.Add(new ResultadoTransaccion
                                {

                                    Resultado = Convert.ToInt32(rd["Resultado"].ToString()),
                                    Mensaje = rd["Mensaje"].ToString()
                                  

                                });
                            }
                        }
                    }
                }

                return Ok("Transacción procesada exitosamente.");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
public class ResultadoTransaccion
{
    public int Resultado { get; set; }
    public string Mensaje { get; set; }
}

public class VentaData
{
    public FACCAB Faccab { get; set; }
    public List<FACDET> FacdetList { get; set; }
}

public class FACCAB
{
    public string CodTransacciones { get; set; }
    public string ordenCompra { get; set; }
    public string CodProveedor { get; set; }
    public string CodTipoDocumento { get; set; }
    public string correlativo { get; set; }
    public string detalles { get; set; }
    public string CFUSER { get; set; }
    public string TipoNota { get; set; }
    public string Almacen { get; set; }
}

public class FACDET
{
    public int DFSECUEN { get; set; }
    public string DFCODIGO { get; set; }
    public decimal DFCANTID { get; set; }
    public decimal DFPREC { get; set; }
    public string Almacen { get; set; }
}