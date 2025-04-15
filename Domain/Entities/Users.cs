namespace Domain.Entities
{
    public class Users : BaseEntity ,  ISoftDeletable
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

    }
}
