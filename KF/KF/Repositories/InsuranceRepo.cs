using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using KF.KFWCFServiceLibrary;

namespace KF.Repositories
{
    public class InsuranceRepo : IInsuranceRepository
    {
        KFWCFServiceLibrary.IService1 client = new KFWCFServiceLibrary.Service1Client();
        public InsuranceCalc CalculateInsurance(InsuranceCalc insuranceCalc)
        {
            return client.CalculateInsurance(insuranceCalc);
        }

        public Car GetCar(string regNum)
        {
            return client.GetCar(regNum);
        }

        public int GetExcess()
        {
            return client.GetExcess();
        }

        public List<InsuranceCalc> GetOffers(int customerId)
        {
            return client.GetOffers(customerId).ToList();
        }

        public bool SaveOffer(InsuranceCalc insuranceCalc)
        {
            return client.SaveOffer(insuranceCalc);
        }

        public Customer GetCustomer(long cprNum)
        {
            return client.GetCustomer(cprNum);
        }

        public Insurance[] GetInsurances()
        {
            return client.GetInsurances();
        }


    }
}