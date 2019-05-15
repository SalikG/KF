using System;
using System.Runtime.Serialization;

namespace KFWCFServiceLibrary.Models
{
    [DataContract]
    public class Car
    {
        [DataMember]
        public string RegNr { get; set; }
        [DataMember]
        public string Brand { get; set; }
        [DataMember]
        public string Model { get; set; }
        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public double NewPrice { get; set; }
        
    }
}