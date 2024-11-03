namespace RESTAPI_CORE.Modelos
{
    public class Response<T>
    {
        public ResponseType InternalStatus { get; set; }
        public T Data { get; set; }
        public string Mensaje { get; set; }

        public Response(ResponseType _InternalStatus, T _Data)
        {
            InternalStatus = _InternalStatus;
            Data = _Data;
        }

        public Response(ResponseType _InternalStatus, string _Mensaje)
        {
            InternalStatus = _InternalStatus;
            Mensaje = _Mensaje;
        }

    }

}
