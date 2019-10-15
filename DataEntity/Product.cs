using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity
{
    public class Product
    {
        public string productuuid { get; set; }
        public string useruuid { get; set; }
        public string name { get; set; }
        public byte[] Photo1 { get; set; }
        public byte[] Photo2 { get; set; }
        public byte[] Photo3 { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public string Photo1ContentType { get; set; }
        public string Photo2ContentType { get; set; }
        public string Photo3ContentType { get; set; }
        public int MeasureType { get; set; }
    }
}
