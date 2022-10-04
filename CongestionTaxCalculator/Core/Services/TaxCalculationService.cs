using System;
using System.Collections.Generic;
using System.Linq;
using CongestionTaxCalculator.Core.Models;
using CongestionTaxCalculator.Core.Repositories;

namespace CongestionTaxCalculator.Core.Services
{
    public class TaxCalculationService : ITaxCalculationService
    {
        private readonly  IEnumerable<TaxRule> _taxRuleList;
        private readonly  IEnumerable<FreeDate> _freeDateList;
        private readonly  RulesEngine.RulesEngine _engine;

        public TaxCalculationService(ITaxRuleRespository taskRuleRespository)
        {
            _taxRuleList = taskRuleRespository.GetCongestionTaxTime().Result;
            _freeDateList = taskRuleRespository.GetCongestionFreeDate().Result;
            _engine = TaxHelper.GenerateRuleEngine(_taxRuleList,"Tax");
        }

        /**
          * Calculate the total toll fee for one day
          *
          * @param vehicle - the vehicle
          * @param dates   - date and time of all passes on one day
          * @return - the total congestion tax for that day
          */

        public KeyValuePair<int, IEnumerable<TaxInfo>> GetTax(IVehicle vehicle, IEnumerable<DateTime> dates)
        {
            var dateCollection = dates.OrderBy(c => c).GroupBy(d => d.Date.Day).ToList();
            List<TaxInfo> taxInfoList = new List<TaxInfo>();
            int total = 0;
            dateCollection.ForEach(dates =>
            {
                var tax = GetTaxForOneDay(vehicle, dates.ToArray());

                taxInfoList.Add(new TaxInfo
                {
                    Date = dates.First(),
                    Value = tax
                });

                total += tax;
            });

            return new KeyValuePair<int, IEnumerable<TaxInfo>>(total, taxInfoList);
        }

        private int GetTaxForOneDay(IVehicle vehicle, DateTime[] dates)
        {
            DateTime intervalStart = dates[0];
            int totalFee = 0;
            foreach (DateTime date in dates)
            {
                int nextFee = GetTollFee(date, vehicle);
                int tempFee = GetTollFee(intervalStart, vehicle);

                //First Bug
                //--------------------------------------------------------------------
                //long diffInMillies = date.Millisecond - intervalStart.Millisecond;
                //long minutes = diffInMillies / 1000 / 60;
                //--------------------------------------------------------------------

                //Fix
                double minutes = date.Subtract(intervalStart).TotalMinutes;

                if (minutes <= 60)
                {
                    if (totalFee > 0) totalFee -= tempFee;
                    if (nextFee >= tempFee) tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                {
                    //Fix for Third Bug
                    intervalStart = date;
                    totalFee += nextFee;
                }
            }
            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }

        private bool IsTollFreeVehicle(IVehicle vehicle)
        {
            return vehicle == null ? false : Enum.GetNames<TollFreeVehicles>().Contains(vehicle.GetVehicleType());
        }

        public int GetTollFee(DateTime date, IVehicle vehicle)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

            int hour = date.Hour;
            int minute = date.Minute;

            //Fix for second bug was done by changing rule number 5
            var resultList = _engine.ExecuteAllRulesAsync("Tax", date).Result;

            var result = (from c in resultList
                          where c.ActionResult is not null && c.ActionResult.Output is not null
                          select c);

            return result.Count() == 0 ? 0 : Convert.ToInt32(result.First().ActionResult.Output);
        }

        private Boolean IsTollFreeDate(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            return ((from c in _freeDateList
                    where c.Year == year && c.Month == month && (c.Days.Length == 0 || c.Days.Contains(day))
                    select c).Count() > 0 ? true : false);

        }


    }
}
