using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RESTAPI_CORE.Modelos;
using System.Data;
using System.Data.SqlClient;

namespace RESTAPI_CORE.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReglaController : ControllerBase

    {
       
        private readonly string cadenaSQL;
        public ReglaController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        //REFERENCIAS
        //MODELO
        //SQL

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Regla> lista = new List<Regla>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("listaReglas", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Regla
                            {

                                idR = Convert.ToInt32(rd["idR"].ToString()),
                                Nombre = rd["Nombre"].ToString(),
                                signoA = rd["signoA"].ToString(),
                                ValorUnit = Convert.ToInt32(rd["ValorUnit"].ToString()),
                                signoB = rd["signoB"].ToString(),
                                cant = Convert.ToInt32(rd["cant"].ToString()),
                                factor = Convert.ToDecimal(rd["factor"].ToString()),

                            });
                        } 
                    }
                }
                var response = new Response<List<Regla>>(ResponseType.Success, lista);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<List<Regla>>(ResponseType.Error, ex.Message); 
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


        [HttpGet]
        [Route("ObtenerR")]
        public IActionResult ObtenerR(int idR)
        {
            List<Regla> lista = new List<Regla>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("listaReglasXIDR", conexion);
                    cmd.Parameters.AddWithValue("idR", idR);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Regla
                            {

                                idR = Convert.ToInt32(rd["idR"].ToString()),
                                Nombre = rd["Nombre"].ToString(),
                                signoA = rd["signoA"].ToString(),
                                ValorUnit = Convert.ToInt32(rd["ValorUnit"].ToString()),
                                signoB = rd["signoB"].ToString(),
                                cant = Convert.ToInt32(rd["cant"].ToString()),
                                factor = Convert.ToDecimal(rd["factor"].ToString()),

                            });
                        }
                    }
                }
                var response = new Response<List<Regla>>(ResponseType.Success, lista);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<List<Regla>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Regla objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("guardar_regla", conexion);
                    cmd.Parameters.AddWithValue("Nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("signoA", objeto.signoA);
                    cmd.Parameters.AddWithValue("ValorUnit", objeto.ValorUnit);
                    cmd.Parameters.AddWithValue("signoB", objeto.signoB);
                    cmd.Parameters.AddWithValue("cant", objeto.cant);
                    cmd.Parameters.AddWithValue("factor", objeto.factor);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
                var response = new Response<string>(ResponseType.Success, "agregado");
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<string>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Regla objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("editar_regla", conexion);
                    cmd.Parameters.AddWithValue("idR", objeto.idR);
                    cmd.Parameters.AddWithValue("Nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("signoA", objeto.signoA);
                    cmd.Parameters.AddWithValue("ValorUnit", objeto.ValorUnit);
                    cmd.Parameters.AddWithValue("signoB", objeto.signoB);
                    cmd.Parameters.AddWithValue("cant", objeto.cant);
                    cmd.Parameters.AddWithValue("factor", objeto.factor);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                var response = new Response<string>(ResponseType.Success, "editado");
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<string>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete]
        [Route("Eliminar/{idR:int}")]
        public IActionResult Eliminar(int idR)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("eliminar_regla", conexion);
                    cmd.Parameters.AddWithValue("idR", idR);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                var response = new Response<string>(ResponseType.Success, "eliminado");
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<string>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
