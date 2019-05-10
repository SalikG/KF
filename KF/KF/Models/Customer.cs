using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KF.Models
{
    public class Customer
    {
        public int customerId { get; set; }
        public int cprNr { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String adress { get; set; }
        public int zippCode { get; set; }
        public String city { get; set; }
        public int phoneNumber { get; set; }
        public String mail { get; set; }
        public String customerType { get; set; }
    }
}