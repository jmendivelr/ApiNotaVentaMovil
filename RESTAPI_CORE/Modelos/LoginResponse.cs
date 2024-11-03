namespace RESTAPI_CORE.Modelos
{
    public class LoginResponse
    {
        public string Status { get; set; }
        public UserResult Result { get; set; }
    }

    public class UserResult
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string TPD { get; set; }
        public string serie_doc { get; set; }
    }

}
