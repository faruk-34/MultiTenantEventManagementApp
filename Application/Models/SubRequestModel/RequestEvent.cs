 using Domain.Enums;

namespace Application.Models.SubRequestModel
{
    public class RequestEvent
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public string Location { get; set; } = null!;
        public int Capacity { get; set; }
        public EventStatusEnum Status { get; set; }  
 
      }
}
