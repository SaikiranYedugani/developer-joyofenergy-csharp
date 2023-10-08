using System;
using System.Collections.Generic;

namespace JOIEnergy.Domain
{
    public class ElectricityReading
    {
        public DateTime Time { get; set; }
        public Decimal Reading { get; set; }

        public List<ElectricityReading> Electricity { get; set; }




    }
}
