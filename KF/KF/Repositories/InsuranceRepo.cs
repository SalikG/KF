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

        public Insurance[] GetInsurances()
        {
            return client.GetInsurances();
        }
    }
}