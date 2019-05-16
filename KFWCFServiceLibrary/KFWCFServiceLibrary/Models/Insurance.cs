using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KFWCFServiceLibrary.Models
{
    public class Insurance
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string TypeOfInsurance { get; set; }
        [DataMember]
        public int Price { get; set; }
        [DataMember]
        public DateTime BeginningDate { get; set; }
        [DataMember]
        public string Status { get; set; }
    }
}
