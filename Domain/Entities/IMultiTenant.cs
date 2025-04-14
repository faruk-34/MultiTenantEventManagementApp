namespace Domain.Entities
{
    public interface IMultiTenant
    {
        public int TenantId { get; set; }
    }
}
