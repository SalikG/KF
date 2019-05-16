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
        private Customer Customer{ get => new Customer(){
                Address = "Maegårdsvej 4",
                City = "Allinge",
                CprNr = 1234567890,
                CustomerId = 1,
                FirstName = "Saliko",
                LastName = "MisterGman",
                PhoneNumber = "12345678",
                IsPrivateCustomer = true,
                Mail = "ssss@honning.Syp.ru",
                Zipcode = 3770
            };
         }


        // GET: Calculator
        public ActionResult CarInsuranceCalc()
        {
            return View(new InsuranceCalc(){Customer = Customer, Insurances = _repository.GetInsurances()});
        }

        [HttpPost]
        public ActionResult CarInsuranceCalc(InsuranceCalc insuranceCalc, string action)
        {
       
                //insuranceCalc.Car = _repository.GetCar(insuranceCalc.Car.RegNr);
            
                System.Diagnostics.Debug.WriteLine(insuranceCalc.Insurances[0].IsSelected);
                System.Diagnostics.Debug.WriteLine(insuranceCalc.Insurances[1].IsSelected);
                System.Diagnostics.Debug.WriteLine(insuranceCalc.Insurances[2].IsSelected);
                System.Diagnostics.Debug.WriteLine(insuranceCalc.Insurances[3].IsSelected);
     
            return View("CarInsuranceCalc", insuranceCalc);
        }
    }
}