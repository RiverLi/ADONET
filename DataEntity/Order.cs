using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity
{
    public class Order
    {
        public string orderuuid { get; set; }
        public DateTime OrderDate { get; set; }
        public string useruuid { get; set; }
        public float DeliveryFee { get; set; }
        public float TotalPrice { get; set; }
        public float Discount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public int PayMethod { get; set; }
        public sbyte Confirmed { get; set; }
        public string Comment { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public string ProductNames
        {
            get{
                if (this.OrderProducts != null)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (OrderProduct orderProduct in this.OrderProducts)
                        sb.Append(orderProduct.productname + " " + orderProduct.count.ToString());
                    return sb.ToString();
                }
                return string.Empty;
            }
        }
    }
}
