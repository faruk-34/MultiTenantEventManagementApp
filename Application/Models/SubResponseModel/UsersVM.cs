namespace Application.Models.SubResponseModel
{
    public class UsersVM
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int TenantId { get; set; }
       
    }
}
