using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RESTAPI_CORE.Modelos;
using System.Data;
using System.Data.SqlClient;

namespace RESTAPI_CORE.Controllers
{          
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : Controller
    {
        private readonly string cadenaSQL;

        public InventarioController (IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("ListadoNotas")]
        public IActionResult ListadoNotas(string CAALMA, string CATD)
        {

            List<ListadoNotas> ListadoNota = new List<ListadoNotas>();

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_Listado_Notas", conexion);
                    cmd.Parameters.AddWithValue("CAALMA", CAALMA);
                    cmd.Parameters.AddWithValue("CATD", CATD);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {

                        while (rd.Read())
                        {

                            ListadoNota.Add(new ListadoNotas
                            {

                                CAALMA = rd["CAALMA"].ToString(),
                                CATD = rd["CATD"].ToString(),
                                CANUMDOC = rd["CANUMDOC"].ToString(),
                                CACODPRO = rd["CACODPRO"].ToString(),
                                CAGLOSA = rd["CAGLOSA"].ToString(),
                                PRVCNOMBRE = rd["PRVCNOMBRE"].ToString(),
                                Cantidad = Convert.ToInt32(rd["Cantidad"]),
                                CAFECDOC = Convert.ToDateTime(rd["CAFECDOC"])

                            });
                        }

                    }
                }
                var response = new Response<List<ListadoNotas>>(ResponseType.Success, ListadoNota);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<List<ListadoNotas>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


        [HttpGet]
        [Route("lista")]
        public IActionResult lista()
        {

            List<inventario> lista = new List<inventario>();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("usp_lista_inventario", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {

                        while (rd.Read())
                        {

                            lista.Add(new inventario
                            {
                                documento = rd["documento"].ToString(),
                                numero = rd["numero"].ToString(),
                                fecha = rd["fecha"].ToString(),
                                razonsocial = rd["razonsocial"].ToString(),
             
                            });
                        }

                    }
                }

                var response = new Response<List<inventario>>(ResponseType.Success, lista);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<List<Producto>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


        [HttpGet]
        [Route("listaArticulo")]
        public IActionResult listaArticulo(string descripcion, string alm)
        {

            List<Articulo> listaArticulo = new List<Articulo>();

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("listaArticulos", conexion);
                    cmd.Parameters.AddWithValue("descripcion", descripcion);
                    cmd.Parameters.AddWithValue("alm", alm);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {

                        while (rd.Read())
                        {

                            listaArticulo.Add(new Articulo
                            {

                                codigo = rd["codigo"].ToString(),
                                descripcion = rd["descripcion"].ToString(),
                                cantidad= Convert.ToInt32(rd["cantidad"].ToString())

                            });
                        }

                    }
                }
                var response = new Response<List<Articulo>>(ResponseType.Success, listaArticulo);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<List<Articulo>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [Route("buscarArticulo")]
        public IActionResult buscarArticulo(string codbarras, string alm)
        {

            List<Articulo> listaArticulo = new List<Articulo>();

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("buscacodbarra", conexion);
                    cmd.Parameters.AddWithValue("codbarras", codbarras); 
                    cmd.Parameters.AddWithValue("alm", alm);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {

                        while (rd.Read())
                        {

                            listaArticulo.Add(new Articulo
                            {

                                codigo = rd["codigo"].ToString(),
                                descripcion = rd["descripcion"].ToString(),
                                cantidad = Convert.ToInt32(rd["cantidad"].ToString())

                            });
                        }

                    }
                }
                var response = new Response<List<Articulo>>(ResponseType.Success, listaArticulo);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<List<Articulo>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


        [HttpGet]
        [Route("listatipodoc")]
        public IActionResult listatipodoc()
        {

            List<tipodocumento> lista = new List<tipodocumento>();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("usp_listar_tipodocumento", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                           lista.Add(new tipodocumento
                           {
                                codigo = rd["codigo"].ToString(),
                                tipodoc = rd["tipodoc"].ToString(),


                            });
                        }

                    }
                }

                var response = new Response<List<tipodocumento>>(ResponseType.Success, lista);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<List<Producto>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


        [HttpGet]
        [Route("listaalmacenes")]
        public IActionResult listaalmacenes()
        {

            List<almacen> lista = new List<almacen>();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("usp_lista_almacenes", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new almacen
                            {
                                codigo = rd["codigo"].ToString(),
                                descripcion = rd["descripcion"].ToString(),
                            });
                        }

                    }
                }

                var response = new Response<List<almacen>>(ResponseType.Success, lista);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<List<Producto>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


        [HttpGet]
        [Route("listaProveedores")]
        public IActionResult listaProveedores(string descripcion)
        {
            List<Proveedor> lista = new List<Proveedor>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("usp_lista_proveedores", conexion);
                    cmd.Parameters.AddWithValue("descripcion", descripcion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Proveedor
                            {
                                codigo = rd["codigo"].ToString(),
                                descripcion = rd["descripcion"].ToString()

                            });
                        }
                    }
                }
                var response = new Response<List<Proveedor>>(ResponseType.Success, lista);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<List<Proveedor>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


        [HttpGet]
        [Route("listaClientes")]
        public IActionResult listaClientes(string descripcion)
        {
            List<Proveedor> lista = new List<Proveedor>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("usp_lista_proveedores", conexion);
                    cmd.Parameters.AddWithValue("descripcion", descripcion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Proveedor
                            {
                                codigo = rd["codigo"].ToString(),
                                descripcion = rd["descripcion"].ToString()

                            });
                        }
                    }
                }
                var response = new Response<List<Proveedor>>(ResponseType.Success, lista);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<List<Proveedor>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


        [HttpGet]
        [Route("listatransacingreso")]
        public IActionResult listatransacingreso()
        {

            List<transaccion> lista = new List<transaccion>();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("usp_lista_transaccioningreso", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new transaccion
                            {
                                codigo = rd["codigo"].ToString(),
                                descripcion = rd["descripcion"].ToString(),


                            });
                        }

                    }
                }

                var response = new Response<List<transaccion>>(ResponseType.Success, lista);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<List<transaccion>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [Route("listatransacsalida")]
        public IActionResult listatransacsalida()
        {

            List<transaccion> lista = new List<transaccion>();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("usp_lista_transaccionsalida", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new transaccion
                            {
                                codigo = rd["codigo"].ToString(),
                                descripcion = rd["descripcion"].ToString(),


                            });
                        }

                    }
                }

                var response = new Response<List<transaccion>>(ResponseType.Success, lista);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<List<transaccion>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}
