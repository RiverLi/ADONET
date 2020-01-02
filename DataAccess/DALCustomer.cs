using DataAccessCommon;
using DataEntity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using static DataAccessCommon.MyStaticDBHelper;

namespace DataAccess
{
    public class DALCustomer
    {
        public static void CreateCustomer(Customer customer)
        {
            string sql = @"insert into customer (customeruuid,CustomerName, CustomerPhone, DeliveryAddress) 
values (@customeruuid,@CustomerName, @CustomerPhone, @DeliveryAddress)";
            ExecuteNonQueryByEntity(sql, customer);
        }
        public static void UpdateCustomer(Customer customer)
        {
            string sql = @"update customer set customeruuid=@customeruuid,CustomerName=@CustomerName, CustomerPhone=@CustomerPhone, DeliveryAddress=@DeliveryAddress
where customeruuid = @customeruuid";
            ExecuteNonQueryByEntity(sql, customer);
        }
        public static void DeleteCustomer(string customeruuid)
        {
            string sql = @"update customer set Deleted=1 where customeruuid = @customeruuid";
            ExecuteNonQueryByParameters(sql, customeruuid);
        }
        public static Customer FindCustomerByUuid(string customeruuid)
        {
            string sql = @"select * from customer where customeruuid = @customeruuid and Deleted<>1";
            return GetEntityByParameters<Customer>(sql, customeruuid);
        }
        public static List<Customer> GetAllCustomerList(int pageIndex , int pageSize)
        {
            string sql = @"select * from customer where Deleted<>1 limit @pageIndex, @pageSize";
            return GetListByParameters<Customer>(sql, (pageIndex - 1) * pageSize, pageSize);
        }
    }
}