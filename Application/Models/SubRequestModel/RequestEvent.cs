using Domain.Entities;

namespace Application.Models.SubRequestModel
{
    public class RequestEvent
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDateTime { get; set; }
        public string Location { get; set; } = null!;
        public int Capacity { get; set; }
        public int Status { get; set; }  
        public int TenantId { get; set; }
      //  public Tenant Tenant { get; set; } = null!;
      //  public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
