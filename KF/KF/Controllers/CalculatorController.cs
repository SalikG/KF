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
        private static readonly IInsuranceRepository Repository = new InsuranceRepo();
        private Customer _customer = Repository.GetCustomer(1234567890);


    


        // GET: Calculator
        public ActionResult CarInsuranceCalc()
        {
            InsuranceCalc insuranceCalc = new InsuranceCalc()
                {Customer = _customer, Insurances = Repository.GetInsurances(), Excess = Repository.GetExcess()};
 
            return View(insuranceCalc);
        }

        [HttpPost]
        public ActionResult CarInsuranceCalc(InsuranceCalc insuranceCalc, string action)
        {
            ModelState.Clear();
            insuranceCalc.Customer = _customer;
            if (insuranceCalc.Car == null) return View("CarInsuranceCalc", insuranceCalc);

            if (action == "Søg")
            {
                if (insuranceCalc.Car.RegNr == null) return View("CarInsuranceCalc", insuranceCalc);
                insuranceCalc.Car = Repository.GetCar(insuranceCalc.Car.RegNr);
                return View("CarInsuranceCalc", insuranceCalc);
            }
            else if (action == "Gem tilbud")
            {
                if (insuranceCalc.Car.RegNr == null) return View("CarInsuranceCalc", insuranceCalc);
                var saveSuccess = Repository.SaveOffer(insuranceCalc);
                if (Request.UrlReferrer != null)
                    Response.Redirect(Request.UrlReferrer.ToString() + "?saveSuccess=" + saveSuccess);

                return View("CarInsuranceCalc", insuranceCalc);
            }

            var insuranceOffer = Repository.CalculateInsurance(insuranceCalc);
            return View("CarInsuranceCalc", insuranceOffer);
        }
    }
}