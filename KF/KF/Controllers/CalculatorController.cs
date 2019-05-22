using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KF.KFWCFServiceLibrary;
using KF.Repositories;

namespace KF.Controllers
{
    
    public class CalculatorController : Controller
    {
        private readonly IInsuranceRepository _repository = new InsuranceRepo();
        private Customer customer;


    


        // GET: Calculator
        public ActionResult CarInsuranceCalc()
        {
        customer = _repository.GetCustomer(1234567890);
            _repository.GetOffers(1);
        InsuranceCalc insuranceCalc = new InsuranceCalc()
                {Customer = customer, Insurances = _repository.GetInsurances(), Excess = _repository.GetExcess()};
 
            return View(insuranceCalc);
        }

        [HttpPost]
        public ActionResult CarInsuranceCalc(InsuranceCalc insuranceCalc, string action)
        {
            ModelState.Clear();
            insuranceCalc.Customer = customer;
            if (insuranceCalc.Car == null) return View("CarInsuranceCalc", insuranceCalc);

            if (action == "Søg")
            {
                if (insuranceCalc.Car.RegNr == null) return View("CarInsuranceCalc", insuranceCalc);
                insuranceCalc.Car = _repository.GetCar(insuranceCalc.Car.RegNr);
                return View("CarInsuranceCalc", insuranceCalc);
            }

            var insuranceOffer = _repository.CalculateInsurance(insuranceCalc);
            return View("CarInsuranceCalc", insuranceOffer);
        }
    }
}