namespace AseIsthmusAPI.Models
{
    public class Login
    {
        public string EmailAddress { get; set; }
        public string Pw { get; set; }

        public bool IsActive { get; set; }
    }
}
