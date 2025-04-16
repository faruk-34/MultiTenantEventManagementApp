namespace Domain.Entities
{
    public class UserRole : BaseEntity, ISoftDeletable
    { 
        public int UserId { get; set; }
        public Users User { get; set; }  
        public int RoleId { get; set; }
        public Role Role { get; set; }   
    }
}
    