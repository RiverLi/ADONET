using DataAccessCommon;
using DataEntity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using static DataAccessCommon.MyStaticDBHelper;

namespace DataAccess
{
    public class DALOrderProduct
    {
        public static void SetLogger(ILogger logger)
        {
            MyStaticDBHelper.logger = logger;
        }
        public static void CreateOrderProduct(OrderProduct orderProduct)
        {
            string sql = @"insert into `order_product` (
orderuuid,
productuuid,
UnitPrice,
count
) 
values
(
@orderuuid,
@productuuid,
@UnitPrice,
@count
)";
            ExecuteNonQueryByEntity(sql, orderProduct);
        }
        public static void UpdateOrderProduct(OrderProduct orderProduct)
        {
            string sql = @"update `order_product` set
useruuid=@useruuid,
productuuid=@productuuid,
UnitPrice=@UnitPrice,
count=@count
where orderuuid = @orderuuid and productuuid = @productuuid";
            ExecuteNonQueryByEntity(sql, orderProduct);
        }
        public static void DeleteOrderProduct(OrderProduct orderProduct)
        {
            string sql = @"update from `order_product` set Deleted=1 where orderuuid = @orderuuid and productuuid = @productuuid";
            ExecuteNonQueryByEntity(sql, orderProduct);
        }
        public static OrderProduct FindOrderProductByPk(string orderuuid, string productuuid)
        {
            string sql = @"select * from `order_product` where orderuuid = @orderuuid and productuuid = @productuuid";
            return GetEntityByParameters<OrderProduct>(sql, orderuuid, productuuid);
        }
        public static List<OrderProduct> GetAllOrderProductListByOrderuuid(string orderuuid)
        {
            string sql = @"select a.*, b.name as productname from `order_product` a inner join `product` b on a.productuuid = b.productuuid where a.Deleted=0 and a.orderuuid=@orderuuid";
            return GetListByParameters<OrderProduct>(sql, orderuuid);
        }
    }
}
