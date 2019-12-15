using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity
{
    public class Pay_Record
    {
        public string orderuuid { get; set; }
        public int pay_method { get; set; }
        public string response { get; set; }
        public DateTime transaction_date { get; set; }
    }
}
