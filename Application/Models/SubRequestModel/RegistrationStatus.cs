using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.SubRequestModel
{
    public enum RegistrationStatus
    {
        Pending = 1,   // Beklemede
        Confirmed = 2, // Onaylı
        Canceled = 3   // İptal Edildi
    }
}
