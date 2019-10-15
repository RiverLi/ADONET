using DataAccessCommon;
using DataEntity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using static DataAccessCommon.MyStaticDBHelper;

namespace DataAccess
{
    public class DALOrder
    {
        private static ILogger _logger = null;
        public static void SetLogger(ILogger logger)
        {
            _logger = logger;
        }
        public static void CreateOrder(Order order)
        {
            string sql = @"insert into `order` (orderuuid,OrderDate,productuuid,useruuid,count,weight,volume,DeliveryFee,TotalPrice,UnitPrice,Discount, CustomerName, CustomerPhone, DeliveryAddress, Comment, PayMethod) 
values (@orderuuid,@OrderDate,@productuuid,@useruuid,@count,@weight,@volume,@DeliveryFee,@TotalPrice,@UnitPrice,@Discount, @CustomerName, @CustomerPhone, @DeliveryAddress, @Comment, @PayMethod)";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@orderuuid", DbType.String, order.orderuuid));
            parameterList.Add(new MyDBParameter("@OrderDate", DbType.DateTime, order.OrderDate));
            parameterList.Add(new MyDBParameter("@productuuid", DbType.String, order.productuuid));
            parameterList.Add(new MyDBParameter("@useruuid", DbType.String, order.useruuid));
            parameterList.Add(new MyDBParameter("@count", DbType.Int32, order.count));
            parameterList.Add(new MyDBParameter("@weight", DbType.Single, order.weight));
            parameterList.Add(new MyDBParameter("@volume", DbType.Single, order.volume));
            parameterList.Add(new MyDBParameter("@DeliveryFee", DbType.Single, order.DeliveryFee));
            parameterList.Add(new MyDBParameter("@TotalPrice", DbType.Single, order.TotalPrice));
            parameterList.Add(new MyDBParameter("@UnitPrice", DbType.Single, order.UnitPrice));
            parameterList.Add(new MyDBParameter("@Discount", DbType.Single, order.Discount));
            parameterList.Add(new MyDBParameter("@CustomerName", DbType.String, order.CustomerName));
            parameterList.Add(new MyDBParameter("@CustomerPhone", DbType.String, order.CustomerPhone));
            parameterList.Add(new MyDBParameter("@DeliveryAddress", DbType.String, order.DeliveryAddress));
            parameterList.Add(new MyDBParameter("@Comment", DbType.String, order.Comment));
            parameterList.Add(new MyDBParameter("@PayMethod", DbType.String, order.PayMethod));
            ExecuteNonQuery(parameterList, sql, _logger);
        }
        public static void UpdateOrder(Order order)
        {
            string sql = @"update `order` set OrderDate=@OrderDate,productuuid=@productuuid,useruuid=@useruuid,count=@count,weight=@weight,volume=@volume,DeliveryFee=@DeliveryFee,TotalPrice=@TotalPrice,UnitPrice=@UnitPrice,Discount=@Discount, CustomerName=@CustomerName, CustomerPhone=@CustomerPhone, DeliveryAddress=@DeliveryAddress, Comment=@Comment, PayMethod=@PayMethod
where orderuuid = @orderuuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@OrderDate", DbType.String, order.OrderDate));
            parameterList.Add(new MyDBParameter("@productuuid", DbType.String, order.productuuid));
            parameterList.Add(new MyDBParameter("@useruuid", DbType.String, order.useruuid));
            parameterList.Add(new MyDBParameter("@count", DbType.Int32, order.count));
            parameterList.Add(new MyDBParameter("@weight", DbType.Single, order.weight));
            parameterList.Add(new MyDBParameter("@volume", DbType.Single, order.volume));
            parameterList.Add(new MyDBParameter("@DeliveryFee", DbType.Single, order.DeliveryFee));
            parameterList.Add(new MyDBParameter("@TotalPrice", DbType.Single, order.TotalPrice));
            parameterList.Add(new MyDBParameter("@UnitPrice", DbType.Single, order.UnitPrice));
            parameterList.Add(new MyDBParameter("@Discount", DbType.Single, order.Discount));
            parameterList.Add(new MyDBParameter("@CustomerName", DbType.String, order.CustomerName));
            parameterList.Add(new MyDBParameter("@CustomerPhone", DbType.String, order.CustomerPhone));
            parameterList.Add(new MyDBParameter("@DeliveryAddress", DbType.String, order.DeliveryAddress));
            parameterList.Add(new MyDBParameter("@Comment", DbType.String, order.Comment));
            parameterList.Add(new MyDBParameter("@PayMethod", DbType.String, order.PayMethod));
            parameterList.Add(new MyDBParameter("@orderuuid", DbType.String, order.orderuuid));
            ExecuteNonQuery(parameterList, sql, _logger);
        }
        public static void DeleteOrder(Order order)
        {
            string sql = @"update from `order` set Deleted=1 where orderuuid = @orderuuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@orderuuid", DbType.String, order.orderuuid));
            ExecuteNonQuery(parameterList, sql, _logger);
        }
        public static Order FindOrderByUuid(string orderuuid)
        {
            string sql = @"select * from `order` where orderuuid = @orderuuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@orderuuid", DbType.String, orderuuid));
            return ToEntityByEmit.GetEntity<Order>(ExecuteDataset(parameterList, sql, _logger).Tables[0]);
        }
        public static List<Order> GetAllOrderList(int pageIndex, int pageSize)
        {
            string sql = @"select * from `order` where Deleted=0 limit @pageIndex, @pageSize";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@pageIndex", DbType.Int16, (pageIndex - 1) * pageSize));
            parameterList.Add(new MyDBParameter("@pageSize", DbType.Int16, pageSize));
            return ToEntityByEmit.GetList<Order>(ExecuteDataset(parameterList, sql, _logger));
        }
        public static List<Order> GetAllOrderListByUserId(string useruuid, int pageIndex, int pageSize)
        {
            string sql = @"select a.*, b.name as ProductName from `order` a left join product b on a.productuuid = b.productuuid where a.Deleted=0 and a.useruuid=@useruuid order by a.OrderDate desc limit @pageIndex, @pageSize";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@useruuid", DbType.String, useruuid));
            parameterList.Add(new MyDBParameter("@pageIndex", DbType.Int16, (pageIndex - 1) * pageSize));
            parameterList.Add(new MyDBParameter("@pageSize", DbType.Int16, pageSize));
            return ToEntityByEmit.GetList<Order>(ExecuteDataset(parameterList, sql, _logger));
        }
        public static List<Order> GetRecentOrderListByUserId(string useruuid)
        {
            string sql = @"select a.*, b.name as ProductName from `order` a left join product b on a.productuuid = b.productuuid where a.Deleted=0 and a.useruuid=@useruuid and OrderDate > date_add(Now(), interval -2 minute)";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@useruuid", DbType.String, useruuid));
            return ToEntityByEmit.GetList<Order>(ExecuteDataset(parameterList, sql, _logger));
        }
        public static long GetCountByUserId(string useruuid)
        {
            string sql = @"select count(1) as cnt from `order` a left join product b on a.productuuid = b.productuuid where a.Deleted=0 and a.useruuid=@useruuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@useruuid", DbType.String, useruuid));
            var count = ToEntityByEmit.GetEntity<Count>(ExecuteDataset(parameterList, sql, _logger).Tables[0]);
            return count.cnt;
        }
        public static void UpdateConfirmed(Order order)
        {
            string sql = @"update `order` set Confirmed=@Confirmed
where orderuuid = @orderuuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@Confirmed", DbType.SByte, order.Confirmed));
            parameterList.Add(new MyDBParameter("@orderuuid", DbType.String, order.orderuuid));
            ExecuteNonQuery(parameterList, sql, _logger);
        }
    }
}
