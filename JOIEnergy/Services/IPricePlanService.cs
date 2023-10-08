using JOIEnergy.Domain;
using System.Collections.Generic;

namespace JOIEnergy.Services
{
    public interface IPricePlanService
    {
        Dictionary<string, decimal> GetConsumptionCostOfElectricityReadingsForEachPricePlan(string smartMeterId);

        decimal calculateCost(List<ElectricityReading> electricityReadings, PricePlan pricePlan);
    }
}