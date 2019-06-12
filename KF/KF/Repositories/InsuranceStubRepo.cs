using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using KF.KFWCFServiceLibrary;

namespace KF.Repositories
{
    public class InsuranceStubRepo : IInsuranceRepository
    {
        public InsuranceCalc CalculateInsurance(InsuranceCalc insuranceCalc)
        {
            return new InsuranceCalc();
        }

        public Car GetCar(string regNum)
        {
            return new Car(){Brand = "Lambo", Model = "Diablo", ExtraEquipment = true, Id = 1, NewPrice = 1000000, RegNr = "AB 12345", HasYellowPlates = false, Type = "PersonBil", Year = 1999};
        }

        public Insurance[] GetInsurances()
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
            }.ToArray();
        }

        public int GetExcess()
        {
            return 4611;
        }

        public Customer GetCustomer(int customerId)
        {
            return new Customer();
        }

        public List<InsuranceCalc> GetOffers(int customerId)
        {
            return new List<InsuranceCalc>();
        }

        public bool SaveOffer(InsuranceCalc insuranceCalc)
        {
            return true;
        }
    }
}