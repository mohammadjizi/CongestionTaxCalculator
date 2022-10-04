using CongestionTaxCalculator.Core.Models;
using RulesEngine.Models;
using System.Collections.Generic;
using System.Linq;

namespace CongestionTaxCalculator.Core
{
    public static class TaxHelper
    {
        public static RulesEngine.RulesEngine GenerateRuleEngine(IEnumerable<TaxRule> taxRuleList, string wfName)
        {
            List<Rule> ruleExpressionList = new List<Rule>();

            Workflow wf = new Workflow();
            wf.WorkflowName = wfName;

            foreach (Rule[] i in (from c in taxRuleList
                                  select c.Rules))
            {
                foreach (Rule j in i)
                {
                    ruleExpressionList.Add(j);
                }
            };
            wf.Rules = ruleExpressionList;

            return new RulesEngine.RulesEngine(new string[] { Newtonsoft.Json.JsonConvert.SerializeObject(wf) }, null);
        }


		public static TaxRule GenerateRule(string id, string name, IEnumerable<LocalParam> paramList, string exp, RuleActions actions)
		{
			List<Rule> ruleList = new List<Rule>();

			Rule r = new Rule();
			r.RuleName = name;
			r.RuleExpressionType = RuleExpressionType.LambdaExpression;
			r.LocalParams = paramList;
			r.Expression = exp;
			r.Actions = actions;


            ruleList.Add(r);

			TaxRule rule = new TaxRule();
			rule.Id = id;
			rule.City = "Gothenburg";
			rule.Rules = ruleList.ToArray();

			return rule;
		}

		public static LocalParam GenerateLocalParam(string name, string exp)
		{
			LocalParam param = new LocalParam();
			param.Name = name;
			param.Expression = exp;

			return param;
		}

		public static RuleActions GenerateRuleActions(IEnumerable<LocalParam> paramList)
		{
			RuleActions action = new RuleActions();
			action.OnSuccess = GenerateActionInfo(paramList);
			return action;
		}

		private static ActionInfo GenerateActionInfo(IEnumerable<LocalParam> paramList)
		{
			ActionInfo info = new ActionInfo();
			info.Name = "OutputExpression";
			info.Context = new Dictionary<string, object>();
			info.Context.Add("Expression", paramList.First(c => c.Name == "output").Expression);

			return info;
		}

		public static FreeDate GenerateFreeDate(string id, int year, int month, int[] days)
		{
			FreeDate date = new FreeDate();
			date.Id = id;
			date.Year = year;
			date.Month = month;
			date.Days = days;

			return date;
		}
	}
}
