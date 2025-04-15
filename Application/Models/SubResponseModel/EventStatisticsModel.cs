using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.SubResponseModel
{
    public class EventStatisticsModel
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public int TotalRegistrations { get; set; }
        public int Confirmed { get; set; }
        public int Cancelled { get; set; }
        public int Waitlisted { get; set; }
    }
}
