using CongestionTaxCalculator.Core;
using CongestionTaxCalculator.Core.Models;
using RulesEngine.Models;
using System;
using System.Collections.Generic;

namespace CongestionTaskCalculator.Test
{
    public class TestDataHelper
    {
        public static IEnumerable<TaxRule> GenerateRuleList()
        {
            List<TaxRule> ruleList = new List<TaxRule>();


            //##############################################
            //Rule1
            //##############################################
            List<LocalParam> paramlist = new List<LocalParam>();
            paramlist.Add(TaxHelper.GenerateLocalParam("hour", Convert.ToString(6)));
            paramlist.Add(TaxHelper.GenerateLocalParam("startMinute", Convert.ToString(00)));
            paramlist.Add(TaxHelper.GenerateLocalParam("endMinute", Convert.ToString(29)));
            paramlist.Add(TaxHelper.GenerateLocalParam("output", Convert.ToString(8)));
            ruleList.Add(TaxHelper.GenerateRule("1", "Rule1", paramlist, "input1.Hour == hour && input1.Minute >= startMinute && input1.Minute <= endMinute", TaxHelper.GenerateRuleActions(paramlist)));

            //##############################################
            //Rule2
            //##############################################
            paramlist = new List<LocalParam>();
            paramlist.Add(TaxHelper.GenerateLocalParam("hour", Convert.ToString(6)));
            paramlist.Add(TaxHelper.GenerateLocalParam("startMinute", Convert.ToString(30)));
            paramlist.Add(TaxHelper.GenerateLocalParam("endMinute", Convert.ToString(59)));
            paramlist.Add(TaxHelper.GenerateLocalParam("output", Convert.ToString(13)));
            ruleList.Add(TaxHelper.GenerateRule("2", "Rule2", paramlist, "input1.Hour == hour && input1.Minute >= startMinute && input1.Minute <= endMinute", TaxHelper.GenerateRuleActions(paramlist)));

            //##############################################
            //Rule3
            //##############################################
            paramlist = new List<LocalParam>();
            paramlist.Add(TaxHelper.GenerateLocalParam("hour", Convert.ToString(7)));
            paramlist.Add(TaxHelper.GenerateLocalParam("startMinute", Convert.ToString(00)));
            paramlist.Add(TaxHelper.GenerateLocalParam("endMinute", Convert.ToString(59)));
            paramlist.Add(TaxHelper.GenerateLocalParam("output", Convert.ToString(18)));
            ruleList.Add(TaxHelper.GenerateRule("3", "Rule3", paramlist, "input1.Hour == hour && input1.Minute >= startMinute && input1.Minute <= endMinute", TaxHelper.GenerateRuleActions(paramlist)));

            //##############################################
            //Rule4
            //##############################################
            paramlist = new List<LocalParam>();
            paramlist.Add(TaxHelper.GenerateLocalParam("hour", Convert.ToString(8)));
            paramlist.Add(TaxHelper.GenerateLocalParam("startMinute", Convert.ToString(00)));
            paramlist.Add(TaxHelper.GenerateLocalParam("endMinute", Convert.ToString(29)));
            paramlist.Add(TaxHelper.GenerateLocalParam("output", Convert.ToString(13)));
            ruleList.Add(TaxHelper.GenerateRule("4", "Rule4", paramlist, "input1.Hour == hour && input1.Minute >= startMinute && input1.Minute <= endMinute", TaxHelper.GenerateRuleActions(paramlist)));

            //##############################################
            //Rule5
            //##############################################
            paramlist = new List<LocalParam>();
            paramlist.Add(TaxHelper.GenerateLocalParam("hour1", Convert.ToString(8)));
            paramlist.Add(TaxHelper.GenerateLocalParam("hour2", Convert.ToString(14)));
            paramlist.Add(TaxHelper.GenerateLocalParam("hour3", Convert.ToString(9)));
            paramlist.Add(TaxHelper.GenerateLocalParam("startMinute", Convert.ToString(30)));
            paramlist.Add(TaxHelper.GenerateLocalParam("endMinute", Convert.ToString(59)));
            paramlist.Add(TaxHelper.GenerateLocalParam("output", Convert.ToString(8)));
            ruleList.Add(TaxHelper.GenerateRule("5", "Rule5", paramlist, "(input1.Hour >= hour1 && input1.Hour <= hour2 && input1.Minute >= startMinute && input1.Minute <= endMinute) || (input1.Hour >= hour3 && input1.Hour <= hour2)", TaxHelper.GenerateRuleActions(paramlist)));

            //##############################################
            //Rule6
            //##############################################
            paramlist = new List<LocalParam>();
            paramlist.Add(TaxHelper.GenerateLocalParam("hour", Convert.ToString(15)));
            paramlist.Add(TaxHelper.GenerateLocalParam("startMinute", Convert.ToString(00)));
            paramlist.Add(TaxHelper.GenerateLocalParam("endMinute", Convert.ToString(29)));
            paramlist.Add(TaxHelper.GenerateLocalParam("output", Convert.ToString(13)));
            ruleList.Add(TaxHelper.GenerateRule("6", "Rule6", paramlist, "input1.Hour == hour && input1.Minute >= startMinute && input1.Minute <= endMinute", TaxHelper.GenerateRuleActions(paramlist)));

            //##############################################
            //Rule7
            //##############################################
            paramlist = new List<LocalParam>();
            paramlist.Add(TaxHelper.GenerateLocalParam("hour1", Convert.ToString(15)));
            paramlist.Add(TaxHelper.GenerateLocalParam("hour2", Convert.ToString(16)));
            paramlist.Add(TaxHelper.GenerateLocalParam("startMinute", Convert.ToString(00)));
            paramlist.Add(TaxHelper.GenerateLocalParam("endMinute", Convert.ToString(59)));
            paramlist.Add(TaxHelper.GenerateLocalParam("output", Convert.ToString(18)));
            ruleList.Add(TaxHelper.GenerateRule("7", "Rule7", paramlist, "(input1.Hour == hour1 && input1.Minute >= startMinute) || (input1.Hour == hour2 && input1.Minute <= endMinute)", TaxHelper.GenerateRuleActions(paramlist)));

            //##############################################
            //Rule8
            //##############################################
            paramlist = new List<LocalParam>();
            paramlist.Add(TaxHelper.GenerateLocalParam("hour1", Convert.ToString(17)));
            paramlist.Add(TaxHelper.GenerateLocalParam("startMinute", Convert.ToString(00)));
            paramlist.Add(TaxHelper.GenerateLocalParam("endMinute", Convert.ToString(59)));
            paramlist.Add(TaxHelper.GenerateLocalParam("output", Convert.ToString(13)));
            ruleList.Add(TaxHelper.GenerateRule("8", "Rule8", paramlist, "input1.Hour == hour && input1.Minute >= startMinute && input1.Minute <= endMinute", TaxHelper.GenerateRuleActions(paramlist)));

            //##############################################
            //Rule9
            //##############################################
            paramlist = new List<LocalParam>();
            paramlist.Add(TaxHelper.GenerateLocalParam("hour", Convert.ToString(18)));
            paramlist.Add(TaxHelper.GenerateLocalParam("startMinute", Convert.ToString(00)));
            paramlist.Add(TaxHelper.GenerateLocalParam("endMinute", Convert.ToString(29)));
            paramlist.Add(TaxHelper.GenerateLocalParam("output", Convert.ToString(8)));
            ruleList.Add(TaxHelper.GenerateRule("9", "Rule9", paramlist, "input1.Hour == hour && input1.Minute >= startMinute && input1.Minute <= endMinute", TaxHelper.GenerateRuleActions(paramlist)));

            return ruleList;
        }

        public static IEnumerable<FreeDate> GenerateFreeDateList()
        {
            List<FreeDate> dateList = new List<FreeDate>();

            dateList.Add(TaxHelper.GenerateFreeDate("1", 2013, 1, new int[] { 1 }));
            dateList.Add(TaxHelper.GenerateFreeDate("2", 2013, 3, new int[] { 28, 29 }));
            dateList.Add(TaxHelper.GenerateFreeDate("3", 2013, 4, new int[] { 1, 30 }));
            dateList.Add(TaxHelper.GenerateFreeDate("4", 2013, 5, new int[] { 1, 8, 9 }));
            dateList.Add(TaxHelper.GenerateFreeDate("5", 2013, 6, new int[] { 5, 6, 21 }));
            dateList.Add(TaxHelper.GenerateFreeDate("6", 2013, 7, new int[] { 1 }));
            dateList.Add(TaxHelper.GenerateFreeDate("7", 2013, 11, new int[] { 1 }));
            dateList.Add(TaxHelper.GenerateFreeDate("8", 2013, 12, new int[] { 24, 25, 26, 31 }));

            return dateList;

        }


    }
}
