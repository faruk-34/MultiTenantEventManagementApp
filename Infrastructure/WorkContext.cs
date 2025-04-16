namespace Infrastructure
{
    public interface IWorkContext
    {
        public int UserId { get; set; }
        public int TenantId { get; set; }
    }

    public class WorkContext : IWorkContext
    {
        public int  UserId { get; set; }
        public int TenantId { get; set; }
    }
}
