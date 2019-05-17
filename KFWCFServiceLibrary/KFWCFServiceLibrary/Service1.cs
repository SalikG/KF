using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KFWCFServiceLibrary.Models;

namespace KFWCFServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {      
        public InsuranceCalc CalculateInsurance(InsuranceCalc insuranceCalc)
        {
            //Lav udregning med data fra insuranceCalc
            double totalYearlyPrice = 0;         
            foreach (Insurance insurance in insuranceCalc.Insurances)
            {
                totalYearlyPrice += insurance.Price;
            }

            totalYearlyPrice += insuranceCalc.Car.HasYellowPlates ? 1000 : 0;

            insuranceCalc.CarNewPriceDiscount = insuranceCalc.Car.NewPrice < 200000 ? 20 :
                insuranceCalc.Car.NewPrice < 400000 ? 10 :
                insuranceCalc.Car.NewPrice < 800000 ? 5 : 0;
            insuranceCalc.ExcessDiscount = insuranceCalc.Excess == 2305 ? 0.5 :
                insuranceCalc.Excess == 4611 ? 1 :
                insuranceCalc.Excess == 7245 ? 1.5 :
                insuranceCalc.Excess == 9881 ? 2 :
                insuranceCalc.Excess == 12514 ? 2.5 : 0;
            insuranceCalc.SeniorityDiscount += insuranceCalc.Customer.Seniority < 2 ? 0 : insuranceCalc.Customer.Seniority < 5 ? 5 : 10;
            insuranceCalc.YearsWithoutCrashDiscount += insuranceCalc.Customer.YearsWithoutCrash < 5 ? 0 : 2;
            insuranceCalc.FullPriceWithDiscount = totalYearlyPrice *
                                                  (1 - ((insuranceCalc.CarNewPriceDiscount +
                                                    insuranceCalc.SeniorityDiscount +
                                                    insuranceCalc.YearsWithoutCrashDiscount +
                                                    insuranceCalc.ExcessDiscount) / 100));
            insuranceCalc.TotalDiscount = (insuranceCalc.CarNewPriceDiscount +
                                                    insuranceCalc.SeniorityDiscount +
                                                    insuranceCalc.YearsWithoutCrashDiscount +
                                                    insuranceCalc.ExcessDiscount);
            insuranceCalc.FullPriceWithoutDiscount = totalYearlyPrice;
            return insuranceCalc;
        }



        public Car GetCar(string regNum)
        {
            if (regNum.StartsWith("A"))
            {
                return new Car()
                {
                    RegNr = regNum,
                    Brand = "Mercedes",
                    Model = "A180",
                    NewPrice = 280000,
                    Type = "PersonBil",
                    Year = 2018
                };
            }
            else
            {
                return new Car()
                {
                    RegNr = regNum,
                    Brand = "Volvo",
                    Model = "V70",
                    NewPrice = 450000,
                    Type = "PersonBil",
                    Year = 2016
                };
            }
           
        }

        public List<Insurance> GetInsurances()
        {
            return new List<Insurance>()
            {
                new Insurance()
                {
                    Name = "Kasko og Ansvar",
                    Price = 6500,
                    IsSelected = true
                },
                new Insurance()
                {
                    Name = "Vejhjælp",
                    Price = 600,
                    IsSelected = false
                },
                new Insurance()
                {
                    Name = "Førepladsdækning",
                    Price = 400,
                    IsSelected = false
                },
                new Insurance()
                {
                    Name = "Udvidet bildækning",
                    Price = 1000,
                    IsSelected = true
                },
                new Insurance()
                {
                    Name = "Parkeringsskade",
                    Price = 800,
                    IsSelected = false
                }
            };
        }

        public int GetExcess()
        {
            return 4611;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
