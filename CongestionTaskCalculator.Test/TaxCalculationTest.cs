using CongestionTaxCalculator.Core.Models;
using CongestionTaxCalculator.Core.Repositories;
using CongestionTaxCalculator.Core.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CongestionTaskCalculator.Test
{
	public class TaxCalculationTest
	{
		private readonly TaxCalculationService _calcService;

		public TaxCalculationTest()
		{
			IEnumerable<TaxRule> ruleList = TestDataHelper.GenerateRuleList();
			IEnumerable<FreeDate> dateList = TestDataHelper.GenerateFreeDateList();

			var ruleRespository = Mock.Of<ITaxRuleRespository>(c => c.GetCongestionTaxTime().Result == ruleList && c.GetCongestionFreeDate().Result == dateList);

			_calcService = new TaxCalculationService(ruleRespository);
		}

		[Fact]
		public void GetTax_SingleCharge_ReturnsExpectedCharge()
		{

			var dateArray = new string[] {
				"2013-02-08 06:00:00",
				"2013-02-08 06:20:00",
				"2013-02-08 06:40:00",
				"2013-02-08 07:10:27",
				"2013-02-08 07:40:27",
				"2013-02-08 07:50:27"};

			var dateList = dateArray.Select(s => DateTime.Parse(s));
			var vehicle = new Vehicle("Car");

			var result = _calcService.GetTax(vehicle, dateList);

			result.Key.Should().Be(31);
		}

		[Fact]
		public void GetTax_SingleCharge_ReturnsMaxCharge()
		{

			var dateArray = new string[] {
				"2013-02-08 06:27:00",
				"2013-02-08 06:20:27",
				"2013-02-08 14:35:00",
				"2013-02-08 15:29:00",
				"2013-02-08 15:47:00",
				"2013-02-08 16:01:00",
				"2013-02-08 16:48:00",
				"2013-02-08 17:49:00",
				"2013-02-08 18:29:00"};

			var dateList = dateArray.Select(s => DateTime.Parse(s));
			var vehicle = new Vehicle("Car");

			var result = _calcService.GetTax(vehicle, dateList);

			result.Key.Should().Be(60);
		}

		[Fact]
		public void GetTax_DiffrentDays_ReturnsExpectedCharge()
		{

			var dateArray = new string[] {
				"2013-02-08 06:27:00",
				"2013-02-12 06:20:27",
				"2013-02-13 16:48:00"
			};

			var dateList = dateArray.Select(s => DateTime.Parse(s));
			var vehicle = new Vehicle("Car");

			var result = _calcService.GetTax(vehicle, dateList);

			result.Key.Should().Be(34);
		}

		[Fact]
		public void GetTax_DiffrentDaysWithWeekend_ReturnsExpectedCharge()
		{

			var dateArray = new string[] {
				"2013-02-08 06:27:00",
				"2013-02-09 06:20:27",
				"2013-02-10 16:48:00"
			};

			var dateList = dateArray.Select(s => DateTime.Parse(s));
			var vehicle = new Vehicle("Car");

			var result = _calcService.GetTax(vehicle, dateList);

			result.Key.Should().Be(8);
		}

		[Fact]
		public void GetTax_Holidays_ReturnsNoCharge()
		{
			var dateArray = new string[] {
				"2013-05-08 06:00:00",
				"2013-05-08 06:20:00",
				"2013-05-08 06:40:00",
				"2013-05-01 07:10:27",
				"2013-05-01 07:40:27",
				"2013-05-01 07:50:27"};

			var dateList = dateArray.Select(s => DateTime.Parse(s));
			var vehicle = new Vehicle("Car");

			var result = _calcService.GetTax(vehicle, dateList);

			result.Key.Should().Be(0);
		}
	}
}
