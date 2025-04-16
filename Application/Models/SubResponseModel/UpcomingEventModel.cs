namespace Application.Models.SubResponseModel
{
    public class UpcomingEvenModel
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public int CurrentRegistrations { get; set; }
    }

}
