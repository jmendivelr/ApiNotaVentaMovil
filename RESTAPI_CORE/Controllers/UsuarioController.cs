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

    public class UsuarioController : ControllerBase
    {
        private readonly string cadenaSQL;
        public UsuarioController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        //REFERENCIAS
        //MODELO
        //SQL

        [HttpPost]
        [Route("LoginUsuario")]
        public IActionResult LoginUsuario([FromBody] UsuarioLogin objeto)
        {
            UserResult usuario = null;  // Cambiar a objeto singular
            LoginResponse response = new LoginResponse();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("usp_LoginUsuario", conexion);
                    cmd.Parameters.AddWithValue("correo", objeto.correo);
                    cmd.Parameters.AddWithValue("clave", objeto.clave);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())  // Solo esperamos un usuario
                        {
                            usuario = new UserResult
                            {
                                Id = Convert.ToInt32(rd["UsuarioId"]),
                                FullName = rd["FullName"].ToString(),
                                Email = rd["Correo"].ToString(),
                                Password = rd["Password"].ToString(),
                                Token = rd["token"].ToString(),
                                TPD = rd["TPD"].ToString(),
                                serie_doc = rd["serie_doc"].ToString(),
                            };
                        }
                    }
                }

                // Si se encontró el usuario, establece el status en 'Success'
                if (usuario != null)
                {
                    response.Status = "ok";
                    response.Result = usuario;
                }
                else
                {
                    // Si no se encontró el usuario, establece el status en 'NoData'
                    response.Status = "NoData";
                    response.Result = null;
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var responsex = new Response<LoginResponse>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, responsex);
            }
        }
    }
}
