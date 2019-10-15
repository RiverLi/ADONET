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
    public class DALProduct
    {
        private static ILogger _logger = null;
        public static void SetLogger(ILogger logger)
        {
            _logger = logger;
        }
        public static void CreateProduct(Product product)
        {
            string sql = @"insert into product (productuuid,useruuid,name,description,price, MeasureType) 
values (@productuuid,@useruuid,@name,@description,@price, @MeasureType)";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@productuuid", DbType.String, product.productuuid));
            parameterList.Add(new MyDBParameter("@useruuid", DbType.String, product.useruuid));
            parameterList.Add(new MyDBParameter("@name", DbType.String, product.name));
            parameterList.Add(new MyDBParameter("@description", DbType.String, product.description));
            parameterList.Add(new MyDBParameter("@price", DbType.Single, product.price));
            parameterList.Add(new MyDBParameter("@MeasureType", DbType.Single, product.MeasureType));
            ExecuteNonQuery(parameterList, sql, _logger);
        }

        public static void UpdateProduct(Product product)
        {
            string sql = @"update product set useruuid=@useruuid,name=@name,description=@description,price=@price, MeasureType=@MeasureType
where productuuid = @productuuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@useruuid", DbType.String, product.useruuid));
            parameterList.Add(new MyDBParameter("@name", DbType.String, product.name));
            parameterList.Add(new MyDBParameter("@description", DbType.String, product.description));
            parameterList.Add(new MyDBParameter("@price", DbType.Single, product.price));
            parameterList.Add(new MyDBParameter("@MeasureType", DbType.Single, product.MeasureType));
            parameterList.Add(new MyDBParameter("@productuuid", DbType.String, product.productuuid));
            ExecuteNonQuery(parameterList, sql, _logger);
        }

        public static void UpdatePhoto1(Product product)
        {
            string sql = @"update product set Photo1=@Photo1, Photo1ContentType=@Photo1ContentType
where productuuid = @productuuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@Photo1ContentType", DbType.Binary, product.Photo1ContentType));
            parameterList.Add(new MyDBParameter("@Photo1", DbType.Binary, product.Photo1));
            parameterList.Add(new MyDBParameter("@productuuid", DbType.String, product.productuuid));
            ExecuteNonQuery(parameterList, sql, _logger);
        }

        public static void UpdatePhoto2(Product product)
        {
            string sql = @"update product set Photo2=@Photo2, Photo2ContentType=@Photo2ContentType
where productuuid = @productuuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@Photo2ContentType", DbType.Binary, product.Photo2ContentType));
            parameterList.Add(new MyDBParameter("@Photo2", DbType.Binary, product.Photo2));
            parameterList.Add(new MyDBParameter("@productuuid", DbType.String, product.productuuid));
            ExecuteNonQuery(parameterList, sql, _logger);
        }

        public static void UpdatePhoto3(Product product)
        {
            string sql = @"update product set Photo3=@Photo3, Photo3ContentType = @Photo3ContentType
where productuuid = @productuuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@Photo3ContentType", DbType.Binary, product.Photo3ContentType));
            parameterList.Add(new MyDBParameter("@Photo3", DbType.Binary, product.Photo3));
            parameterList.Add(new MyDBParameter("@productuuid", DbType.String, product.productuuid));
            ExecuteNonQuery(parameterList, sql, _logger);
        }

        public static void DeleteProduct(string productuuid)
        {
            string sql = @"update product set Deleted=1 where productuuid = @productuuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@productuuid", DbType.String, productuuid));
            ExecuteNonQuery(parameterList, sql, _logger);
        }

        public static Product FindProductByUuid(string uuid)
        {
            string sql = @"select * from product where productuuid = @productuuid and Deleted<>1";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@productuuid", DbType.String, uuid));
            return ToEntityByEmit.GetEntity<Product>(ExecuteDataset(parameterList, sql, _logger).Tables[0]);
        }

        public static List<Product> GetAllProductList(int pageIndex , int pageSize)
        {
            string sql = @"select * from product where Deleted<>1 limit @pageIndex, @pageSize";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@pageIndex", DbType.Int16, (pageIndex - 1) * pageSize));
            parameterList.Add(new MyDBParameter("@pageSize", DbType.Int16, pageSize));
            return ToEntityByEmit.GetList<Product>(ExecuteDataset(parameterList, sql, _logger));
        }
        public static List<Product> GetAllProductListByUserId(string useruuid)
        {
            string sql = @"select productuuid,useruuid,`name`,description,price, MeasureType from product where Deleted=0 and useruuid=@useruuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@useruuid", DbType.String, useruuid));
            return ToEntityByEmit.GetList<Product>(ExecuteDataset(parameterList, sql, _logger));
        }
        public static Product GetPhoto1ByProductUuid(string productuuid)
        {
            string sql = @"select productuuid, Photo1, Photo1ContentType from product
where Deleted=0 and productuuid = @productuuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@productuuid", DbType.String, productuuid));
            return ToEntityByEmit.GetEntity<Product>(ExecuteDataset(parameterList, sql, _logger).Tables[0]);
        }
        public static Product GetPhoto2ByProductUuid(string productuuid)
        {
            string sql = @"select productuuid, Photo2, Photo2ContentType from product
where Deleted=0 and productuuid = @productuuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@productuuid", DbType.String, productuuid));
            return ToEntityByEmit.GetEntity<Product>(ExecuteDataset(parameterList, sql, _logger).Tables[0]);
        }
        public static Product GetPhoto3ByProductUuid(string productuuid)
        {
            string sql = @"select productuuid, Photo3, Photo3ContentType from product
where Deleted=0 and productuuid = @productuuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@productuuid", DbType.String, productuuid));
            return ToEntityByEmit.GetEntity<Product>(ExecuteDataset(parameterList, sql, _logger).Tables[0]);
        }
        public static Product GetProductByProductUuid(string productuuid)
        {
            string sql = @"select * from product
where Deleted=0 and productuuid = @productuuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@productuuid", DbType.String, productuuid));
            return ToEntityByEmit.GetEntity<Product>(ExecuteDataset(parameterList, sql, _logger).Tables[0]);
        }
    }
}