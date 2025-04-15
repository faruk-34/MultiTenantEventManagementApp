using Domain.Enums;

namespace Application.Models.SubRequestModel
{
    public class RequestRegistration
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public RegistrationStatusEnum Status { get; set; }
    }



}
