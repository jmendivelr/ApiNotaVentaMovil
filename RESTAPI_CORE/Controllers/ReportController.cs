using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RESTAPI_CORE.Modelos;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace RESTAPI_CORE.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly string cadenaSQL;
        public ReportController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }


        [HttpGet]
        [Route("GeneratePDF")]
        public IActionResult GeneratePDF(int ID)
        {

            List<Impresion> listaImpresion = new List<Impresion>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("LISTARCOTIIMPRESION", conexion);
                    cmd.Parameters.AddWithValue("CotiSofkit", ID);
                    cmd.CommandType = CommandType.StoredProcedure;
                
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            listaImpresion.Add(new Impresion
                            {

                                idcotizacion = Convert.ToInt32(rd["idcotizacion"].ToString()),
                                codcliente = rd["codcliente"].ToString(),
                                nombrecliente = rd["nombrecliente"].ToString(),
                                dircliente = rd["dircliente"].ToString(),
                                fecha = rd["fecha"].ToString(),
                                item = rd["item"].ToString(),
                                Cod = rd["Cod"].ToString(),
                                MarcaPrecio = rd["MarcaPrecio"].ToString(),
                                NomArticulo = rd["NomArticulo"].ToString(),
                                UND = rd["cant"].ToString(),
                                cant = Convert.ToInt32(rd["cant"].ToString()),
                                Precio = Convert.ToDecimal(rd["Precio"].ToString()),
                                TOTAL = Convert.ToDecimal(rd["TOTAL"].ToString()),
                                Moneda = rd["Moneda"].ToString(),
                                TipoCambio = Convert.ToDecimal(rd["TipoCambio"].ToString())

                            });
                        }
                    }
                }
                var response = new Response<List<Impresion>>(ResponseType.Success, listaImpresion);
                return StatusCode(StatusCodes.Status200OK, response);
            } 
            catch (Exception ex)
            {
                var response = new Response<List<Impresion>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

        }
    }
}
