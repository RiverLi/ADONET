using System;

namespace DataEntity
{
    public class User
    {
        public string uuid { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string IDCARD { get; set; }
        public byte[]  Photo1 { get; set; }
        public string Photo1ContentType { get; set; }
        public byte[] Photo2 { get; set; }
        public string Photo2ContentType { get; set; }
        public string Scope { get; set; }
        public float DeliveryFee { get; set; }
        public string PhoneNumber { get; set; }
        public string alipay { get; set; }
        public string wechat { get; set; }
        public string loginid { get; set; }
        public string passsword { get; set; }
        public byte[] alipayBarcode { get; set; }
        public byte[] wechatBarcode { get; set; }
        public string alipayBarcodeContentType { get; set; }
        public string wechatBarcodeContentType { get; set; }
        public string busytime { get; set; }
    }
}
