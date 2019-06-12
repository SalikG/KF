using System;
using System.Collections.Generic;
using System.Text;
using KFWCFServiceLibrary;
using KFWCFServiceLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KFServiceTest
{
    [TestClass]
    public class KFServiceUnitTest
    {
        [TestMethod]
        public void ServiceCalculatorTest()
        {
            var service = new Service1();
            var result = service.CalculateInsurance(new InsuranceCalc()
            {
                Car = new Car()
                {
                    Brand = "Lambo",
                    Model = "Diablo",
                    ExtraEquipment = true,
                    Id = 1,
                    NewPrice = 1000000,
                    RegNr = "AB 12345",
                    HasYellowPlates = false,
                    Type = "PersonBil",
                    Year = 1999
                },

                Customer = new Customer()
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
                },
                Insurances = new List<Insurance>
                {
                    new Insurance
                    {
                        Id = 2,
                        Name = "Ansvar og Kasko",
                        Price = 6500,
                        IsSelected = true
                    },
                    new Insurance
                    {
                        Id = 3,
                        Name = "Vejhjælp",
                        Price = 600,
                        IsSelected = false
                    },
                    new Insurance
                    {
                        Id = 4,
                        Name = "Førepladsdækning",
                        Price = 400,
                        IsSelected = false
                    },
                    new Insurance
                    {
                        Id = 5,
                        Name = "Udvidet bildækning",
                        Price = 1000,
                        IsSelected = true
                    },
                    new Insurance
                    {
                        Id = 6,
                        Name = "Parkeringsskade",
                        Price = 800,
                        IsSelected = false
                    }
                },
                Excess = 9881,
                BeginningDate = DateTime.Now,
                CarChange = true,
            });
            Assert.AreEqual(6450, result.FullPriceWithDiscount);
            Assert.AreEqual(7500, result.FullPriceWithoutDiscount);
            Assert.AreEqual(14, result.TotalDiscount);
            result = service.CalculateInsurance(new InsuranceCalc()
            {
                Car = new Car()
                {
                    Brand = "Volvo",
                    Model = "V60",
                    ExtraEquipment = false,
                    Id = 1,
                    NewPrice = 300000,
                    RegNr = "AB 12345",
                    HasYellowPlates = true,
                    Type = "PersonBil",
                    Year = 1999
                },

                Customer = new Customer()
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
                    Seniority = 2,
                    YearsWithoutCrash = 2
                },
                Insurances = new List<Insurance>
                {
                    new Insurance
                    {
                        Id = 2,
                        Name = "Ansvar og Kasko",
                        Price = 6500,
                        IsSelected = true
                    },
                    new Insurance
                    {
                        Id = 3,
                        Name = "Vejhjælp",
                        Price = 600,
                        IsSelected = true
                    },
                    new Insurance
                    {
                        Id = 4,
                        Name = "Førepladsdækning",
                        Price = 400,
                        IsSelected = true
                    },
                    new Insurance
                    {
                        Id = 5,
                        Name = "Udvidet bildækning",
                        Price = 1000,
                        IsSelected = false
                    },
                    new Insurance
                    {
                        Id = 6,
                        Name = "Parkeringsskade",
                        Price = 800,
                        IsSelected = true
                    }
                },
                Excess = 0,
                BeginningDate = DateTime.Now,
                CarChange = false,
            });
            Assert.AreEqual(7905, result.FullPriceWithDiscount);
            Assert.AreEqual(9300, result.FullPriceWithoutDiscount);
            Assert.AreEqual(15, result.TotalDiscount);
        }
    }
}
