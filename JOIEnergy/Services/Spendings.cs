using JOIEnergy.Domain;
using JOIEnergy.Enums;
using JOIEnergy.Functions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JOIEnergy.Services
{
    public class Spendings : ISpendings
    {
        private readonly IMeterReadingService _meterReadingService;
        private readonly Dictionary<string, Supplier> _smartSuppliers;
        private readonly List<PricePlan> _pricePlans;

        private readonly IPricePlanService _pricePlanService;
        public Spendings(IMeterReadingService meterReadingService, IPricePlanService pricePlanService, List<PricePlan> pricePlans, Dictionary<string, Supplier> smartSuppliers)
        {
            _meterReadingService = meterReadingService;
            _pricePlanService = pricePlanService;
            _pricePlans = pricePlans;
            _smartSuppliers = smartSuppliers;
        }
        public Decimal? WeeklySpendings(string smartmeterId)
        {
            List<ElectricityReading> consumerSpend = _meterReadingService.GetReadings(smartmeterId).Where(t => t.Time < DateTime.Now && t.Time> DateTime.Now.AddDays(-7)).ToList();
            var smartSuppplier = _smartSuppliers.ContainsKey(smartmeterId) ? _smartSuppliers[smartmeterId]: Supplier.NullSupplier;

            if(smartSuppplier == Supplier.NullSupplier)
            {
                return null;
            }
            var priceplan = _pricePlans.Where(p=>p.EnergySupplier.ToString().Equals(smartSuppplier.ToString())).FirstOrDefault();
            if(priceplan==null)
            {
                return null;
            }
            var average = ConsumptionCalculations.calculateAverageReading(consumerSpend);
            var timeElapsed = ConsumptionCalculations.calculateTimeElapsed(consumerSpend);
            var energyConsumerd = average * timeElapsed;
            return _pricePlanService.calculateCost(consumerSpend, priceplan);
            //energyConsumerd* priceplan.UnitRate;

        }
    }
}
