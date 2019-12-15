using DataAccessCommon;
using DataEntity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using static DataAccessCommon.MyStaticDBHelper;

namespace DataAccess
{
    public class DALOrder
    {
        public static void CreateOrder(Order order)
        {
            string sql = @"insert into `order` (
orderuuid,
OrderDate,
useruuid,
DeliveryFee,
TotalPrice,
Discount,
CustomerName,
CustomerPhone,
DeliveryAddress,
Comment,
PayMethod
) 
values
(
@orderuuid,
@OrderDate,
@useruuid,
@DeliveryFee,
@TotalPrice,
@Discount,
@CustomerName,
@CustomerPhone,
@DeliveryAddress,
@Comment,
@PayMethod
)";
            ExecuteNonQueryByEntity(sql, order);
        }
        public static void UpdateOrder(Order order)
        {
            string sql = @"update `order` set OrderDate=@OrderDate,
useruuid=@useruuid,
DeliveryFee=@DeliveryFee,
TotalPrice=@TotalPrice,
Discount=@Discount,
CustomerName=@CustomerName,
CustomerPhone=@CustomerPhone,
DeliveryAddress=@DeliveryAddress,
Comment=@Comment,
PayMethod=@PayMethod
where orderuuid = @orderuuid";
            ExecuteNonQueryByEntity(sql, order);
        }
        public static void DeleteOrder(Order order)
        {
            string sql = @"update from `order` set Deleted=1 where orderuuid = @orderuuid";
            ExecuteNonQueryByEntity(sql, order);
        }
        public static Order FindOrderByUuid(string orderuuid)
        {
            string sql = @"select * from `order` where orderuuid = @orderuuid";
            return GetEntityByParameters<Order>(sql, orderuuid);
        }
        public static List<Order> GetAllOrderList(int pageIndex, int pageSize)
        {
            string sql = @"select * from `order` where Deleted=0 limit @pageIndex, @pageSize";
            return GetListByParameters<Order>(sql, (pageIndex - 1) * pageSize, pageSize);
        }
        public static List<Order> GetAllOrderListByUserId(string useruuid, int pageIndex, int pageSize)
        {
            string sql = @"select a.* from `order` a where a.Deleted=0 and a.useruuid=@useruuid order by a.OrderDate desc limit @pageIndex, @pageSize";
            return GetListByParameters<Order>(sql, useruuid, (pageIndex - 1) * pageSize, pageSize);
        }
        public static List<Order> GetRecentOrderListByUserId(string useruuid)
        {
            string sql = @"select a.*, b.name as ProductName from `order` a left join order_product c on a.orderuuid = c.orderuuid left join product b on c.productuuid = b.productuuid where a.Deleted=0 and a.useruuid=@useruuid and OrderDate > date_add(Now(), interval -2 minute)";
            return GetListByParameters<Order>(sql, useruuid);
        }
        public static Count GetCountByUserId(string useruuid)
        {
            string sql = @"select count(1) as cnt from `order` a left join order_product c on a.orderuuid = c.orderuuid left join product b on c.productuuid = b.productuuid where a.Deleted=0 and a.useruuid=@useruuid";
            return GetEntityByParameters<Count>(sql, useruuid);
        }
        public static void UpdateConfirmed(Order order)
        {
            string sql = @"update `order` set Confirmed=@Confirmed
where orderuuid = @orderuuid";
            ExecuteNonQueryByEntity(sql, order);
        }
    }
}
