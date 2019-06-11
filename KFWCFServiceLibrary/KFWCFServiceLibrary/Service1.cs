using System;
using System.Collections.Generic;
using System.Linq;
using KFWCFServiceLibrary.DatabaseContextModels;
using KFWCFServiceLibrary.Models;
using Microsoft.SqlServer.Server;
using Car = KFWCFServiceLibrary.Models.Car;
using Customer = KFWCFServiceLibrary.Models.Customer;
using Insurance = KFWCFServiceLibrary.Models.Insurance;

namespace KFWCFServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {   
        private KFInsuranceDataContext kfInsuranceData = new KFInsuranceDataContext();

        public InsuranceCalc CalculateInsurance(InsuranceCalc insuranceCalc)
        {
            //Laver udregning med data fra insuranceCalc
            double totalYearlyPrice = 0;         
            foreach (Insurance insurance in insuranceCalc.Insurances.Where(i => i.IsSelected))
            {
                totalYearlyPrice += insurance.Price;
            }

            totalYearlyPrice += insuranceCalc.Car.HasYellowPlates.GetValueOrDefault() ? 1000 : 0;

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
            if (regNum.ToLower().StartsWith("a") && regNum != null)
            {
                return new Car
                {
                    Id = 1,
                    RegNr = regNum,
                    Brand = "Mercedes",
                    Model = "A180",
                    NewPrice = 280000,
                    Type = "PersonBil",
                    Year = 2018
                };
            }

            return new Car
            {
                Id = 2,
                RegNr = regNum,
                Brand = "Volvo",
                Model = "V70",
                NewPrice = 450000,
                Type = "PersonBil",
                Year = 2016
            };

        }

        public List<Insurance> GetInsurances()
        {
            return new List<Insurance>
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
            };
        }

        public int GetExcess()
        {
            return 4611;
        }

        public Customer GetCustomer(int customerId)
        {
            DatabaseContextModels.Customer customer =
                (from c in kfInsuranceData.Customers where c.Id == customerId select c).FirstOrDefault();
            return new Customer()
            {
                CustomerId = customer.Id,
                CprNr = customer.CprNr,
                IsPrivateCustomer = customer.IsPrivate,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                Zipcode = (from z in kfInsuranceData.Zipcodes where z.Id == customer.Fk_ZipcodeId select z.ZipcodeNum)
                    .FirstOrDefault(),
                City = (from c in kfInsuranceData.Cities
                    select c.Zipcodes.FirstOrDefault(z => z.Id == customer.Fk_ZipcodeId)).FirstOrDefault()?.City.Name,
                PhoneNumber = customer.PhoneNumber,
                Mail = customer.Mail,
                Seniority = customer.Seniority,
                YearsWithoutCrash = customer.YearsWithoutCrash
            };

        }


        public List<InsuranceCalc> GetOffers(int customerId)
        {
            List<InsuranceCalc> result = new List<InsuranceCalc>();
            var offers = (from o in kfInsuranceData.Offers where o.Fk_CustomerId == customerId && (DateTime.Now - o.BeginningDate).TotalDays <= 30 orderby o.BeginningDate ascending select o).ToList();
            foreach (var o in offers)
            {
                // Laver en liste af insurances som det specifikke offer indeholder
                var insuranceList = (from i in kfInsuranceData.Insurances
                    join insuranceOffer in kfInsuranceData.InsuranceOffers on i.Id 
                        equals insuranceOffer.Fk_InsuranceId where insuranceOffer.Fk_OfferId == o.Id
                            select i).ToList();

                var insList = new List<Insurance>();
                foreach (var ins in insuranceList)
                {
                    insList.Add(new Insurance()
                    {
                        Name = ins.Name,
                        Price = ins.Price,
                        IsSelected = true
                    });
                }

                result.Add(CalculateInsurance(new InsuranceCalc()
                {
                    Car = new Car()
                    {
                        Model = o.Car.Model.Name,
                        ExtraEquipment = o.Car.ExtraEquipment,
                        Brand = o.Car.Model.Brand.Name,
                        HasYellowPlates = o.Car.HasYellowPlates,
                        NewPrice = o.Car.NewPrice,
                        RegNr = o.Car.RegNum,
                        Type = o.Car.Model.Type.Name,
                        Year = o.Car.Year
                    },
                    Customer = GetCustomer(customerId),
                    Insurances = insList,                 
                    CarNewPriceDiscount = o.CarNewPriceDiscount,
                    SeniorityDiscount = o.SeniorityDiscount,
                    YearsWithoutCrashDiscount = o.YearsWithoutCrashDiscount,
                    ExcessDiscount = o.ExcessDiscount,
                    Excess = o.Excess,
                    BeginningDate = o.BeginningDate,
                    CarChange = o.CarChange,
                }));
            }

            return result;
        }

        public bool SaveOffer(InsuranceCalc insuranceCalc)
        {
            try
            {                         
                var offer = new Offer()
                {
                    Fk_CustomerId = insuranceCalc.Customer.CustomerId,
                    Fk_CarId = insuranceCalc.Car.Id,
                    CarNewPriceDiscount = insuranceCalc.CarNewPriceDiscount,
                    SeniorityDiscount = insuranceCalc.SeniorityDiscount,
                    YearsWithoutCrashDiscount = insuranceCalc.YearsWithoutCrashDiscount,
                    ExcessDiscount = insuranceCalc.ExcessDiscount,
                    Excess = insuranceCalc.Excess,
                    BeginningDate = DateTime.Now,
                    CarChange = insuranceCalc.CarChange       
                };

                kfInsuranceData.Offers.InsertOnSubmit(offer);
                kfInsuranceData.Offers.Context.SubmitChanges();
                var insOffers = new List<InsuranceOffer>();
                foreach (var ins in insuranceCalc.Insurances.Where(i => i.IsSelected))
                {
                    insOffers.Add(new InsuranceOffer()
                    {
                        Fk_InsuranceId = ins.Id,
                        Fk_OfferId = (from o in kfInsuranceData.Offers where o.BeginningDate == offer.BeginningDate select o.Id).FirstOrDefault()
                    });
                }
                kfInsuranceData.InsuranceOffers.InsertAllOnSubmit(insOffers);
                kfInsuranceData.InsuranceOffers.Context.SubmitChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
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
