namespace work_platform_backend.Dtos
{
    public class AuthenticationResponse
    {
        public AuthenticationResponse(string Token , string Username , string Email)
        {
            this.Username = Username ;
            this.Email = Email ;
            this.Token =Token ;

        }
        public  string Token { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        
    }
}