using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity
{
    public class OrderProduct
    {
        public string orderuuid { get; set; }
        public string productuuid { get; set; }
        public string productname { get; set; }
        public float UnitPrice { get; set; }
        public float count { get; set; }
    }
}
