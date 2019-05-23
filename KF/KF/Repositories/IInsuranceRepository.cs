using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KF.KFWCFServiceLibrary;

namespace KF.Repositories
{
    interface IInsuranceRepository
    {
        InsuranceCalc CalculateInsurance(InsuranceCalc insuranceCalc);

        Car GetCar(string regNum);

        Insurance[] GetInsurances();

        int GetExcess();

        Customer GetCustomer(int customerId);

        List<InsuranceCalc> GetOffers(int customerId);

        bool SaveOffer(InsuranceCalc insuranceCalc);

    }
}
