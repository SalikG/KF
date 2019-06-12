using System;
using System.Collections.Generic;
using System.Web.Mvc;
using KF.Controllers;
using KF.KFWCFServiceLibrary;
using KF.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KFTest
{
    [TestClass]
    public class CalculatorControllerUnitTest
    {

        [TestMethod]
        public void GetCalculatorViewTest()
        {
            var controller = new CalculatorController(new Customer()
            {
                CustomerId = 1,
                CprNr = 1234567890,
                IsPrivateCustomer = true,
                FirstName = "Bo",
                LastName = "Mamder",
                Address = "Vej 10",
                Zipcode = 2400,
                City = "København NV",
                Mail = "mail@mail.dk",
                Seniority = 5,
                YearsWithoutCrash = 10
            }, new InsuranceStubRepo());
            var result = controller.CarInsuranceCalc() as ViewResult;
            Assert.AreEqual("CarInsuranceCalc", result.ViewName);
            Assert.IsInstanceOfType((InsuranceCalc)result.ViewData.Model, typeof(InsuranceCalc));
        }
    }
}
