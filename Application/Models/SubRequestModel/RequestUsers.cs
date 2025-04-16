namespace Application.Models.SubRequestModel
{
    public class RequestUsers
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
      //  public int TenantId { get; set; }
    }
}
