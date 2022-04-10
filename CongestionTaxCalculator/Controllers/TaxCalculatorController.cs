using CongestionTaxCalculator.Core.API;
using CongestionTaxCalculator.Core.Models;
using CongestionTaxCalculator.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CongestionTaxCalculator.Controllers
{
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly ITaxCalculationService _calculationService;

        private readonly ILogger<TaxCalculatorController> _logger;

        public TaxCalculatorController(ILogger<TaxCalculatorController> logger, ITaxCalculationService calculationService)
        {
            _logger = logger;
            _calculationService = calculationService;
        }


        [HttpPost]
        [Route("/GetTax")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TaxCalculationResponse> GetTax([FromBody] TaxCalculationRequest request)
        {

            IVehicle vehicle;
            switch (request.VehicleType)
            {
                case "Car": vehicle = new Car(request.VehicleType); break;
                case "Motorbike": vehicle = new Motorbike(request.VehicleType); break;
                default: vehicle = new Vehicle(request.VehicleType); break;
            }
            var tax = _calculationService.GetTax(vehicle, request.Dates.Select(c => DateTime.Parse(c)));

            return new TaxCalculationResponse(tax.Key, tax.Value);
        }
    }
}
