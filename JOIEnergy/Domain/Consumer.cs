using JOIEnergy.Enums;
using System.Collections.Generic;

namespace JOIEnergy.Domain
{
    public class Consumer
    {
        public int UserId { get; set; }

        public string UserName { get; set; }


        public List<ElectricityReading> ElectricityReadings { get; set; }


        public Supplier Supplier { get; set; }

    }
}
