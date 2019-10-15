using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity
{
    public class Order
    {
        public string orderuuid { get; set; }
        public DateTime OrderDate { get; set; }
        public string productuuid { get; set; }
        public string useruuid { get; set; }
        public int count { get; set; }
        public float weight { get; set; }
        public float volume { get; set; }
        public float DeliveryFee { get; set; }
        public float TotalPrice { get; set; }
        public float UnitPrice { get; set; }
        public float Discount { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public int PayMethod { get; set; }
        public sbyte Confirmed { get; set; }
        public string Comment { get; set; }
    }
}
