using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Res247.Web.ViewModels
{
    public class ShipperViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool HealthStatus { get; set; }

        public int Vaccination { get; set; }

        public bool TravelToOtherPlace { get; set; }

        public bool HaveSymptoms { get; set; }

        public bool MeetCovidPatients { get; set; }
    }
}