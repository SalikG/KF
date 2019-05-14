using System.Runtime.Serialization;

namespace KFWCFServiceLibrary.Models
{
    public class Customer
    {
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public int CprNr { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public int Zipcode { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public int PhoneNumber { get; set; }
        [DataMember]
        public string Mail { get; set; }
        [DataMember]
        public string CustomerType { get; set; }
        [DataMember]
        public int Seniority { get; set; }              //Anciennitet
        [DataMember]
        public int YearsWithoutCrash { get; set; }
        [DataMember]
        public int MyProperty { get; set; }
    }
}