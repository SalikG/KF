using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KFWCFServiceLibrary.Models
{
    public class InsuranceCalc
    {
        private int _excess = 7245;

        [DataMember]
        public Car Car { get; set; }
        [DataMember]
        public List<Insurance> Insurances { get; set; }
        [DataMember]
        public Customer Customer { get; set; }
        [DataMember]
        public double FullPriceWithoutDiscount { get; set; }
        [DataMember]
        public double FullPriceWithDiscount { get; set; }
        [DataMember]
        public double CarNewPriceDiscount { get; set; }
        [DataMember]
        public double SeniorityDiscount { get; set; }
        [DataMember]
        public double YearsWithoutCrashDiscount { get; set; }
        [DataMember]
        public double ExcessDiscount { get; set; }
        [DataMember]
        public int Excess { get => _excess; set => _excess = value; }                   //Excess er selvrisiko
        [DataMember]
        public DateTime BeginningDate { get; set; }
        [DataMember]
        public bool CarChange { get; set; }
        [DataMember]
        public double TotalDiscount { get; set; }
    }
}