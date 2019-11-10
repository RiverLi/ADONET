using DataAccessCommon;
using DataEntity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using static DataAccessCommon.MyStaticDBHelper;

namespace DataAccess
{
    public class DALProduct
    {
        public static void SetLogger(ILogger logger)
        {
            MyStaticDBHelper.logger = logger;
        }
        public static void CreateProduct(Product product)
        {
            string sql = @"insert into product (productuuid,useruuid,name,description,price, MeasureType) 
values (@productuuid,@useruuid,@name,@description,@price, @MeasureType)";
            ExecuteNonQueryByEntity(sql, product);
        }
        public static void UpdateProduct(Product product)
        {
            string sql = @"update product set useruuid=@useruuid,name=@name,description=@description,price=@price, MeasureType=@MeasureType
where productuuid = @productuuid";
            ExecuteNonQueryByEntity(sql, product);
        }
        public static void UpdatePhoto1(Product product)
        {
            string sql = @"update product set Photo1=@Photo1, Photo1ContentType=@Photo1ContentType
where productuuid = @productuuid";
            ExecuteNonQueryByEntity(sql, product);
        }
        public static void UpdatePhoto2(Product product)
        {
            string sql = @"update product set Photo2=@Photo2, Photo2ContentType=@Photo2ContentType
where productuuid = @productuuid";
            ExecuteNonQueryByEntity(sql, product);
        }
        public static void UpdatePhoto3(Product product)
        {
            string sql = @"update product set Photo3=@Photo3, Photo3ContentType = @Photo3ContentType
where productuuid = @productuuid";
            ExecuteNonQueryByEntity(sql, product);
        }
        public static void DeleteProduct(string productuuid)
        {
            string sql = @"update product set Deleted=1 where productuuid = @productuuid";
            ExecuteNonQueryByParameters(sql, productuuid);
        }
        public static Product FindProductByUuid(string uuid)
        {
            string sql = @"select * from product where productuuid = @productuuid and Deleted<>1";
            return GetEntityByParameters<Product>(sql, uuid);
        }
        public static List<Product> GetAllProductList(int pageIndex , int pageSize)
        {
            string sql = @"select * from product where Deleted<>1 limit @pageIndex, @pageSize";
            return GetListByParameters<Product>(sql, (pageIndex - 1) * pageSize, pageSize);
        }
        public static List<Product> GetAllProductListByUserId(string useruuid)
        {
            string sql = @"select productuuid,useruuid,`name`,description,price, MeasureType from product where Deleted=0 and useruuid=@useruuid";
            return GetListByParameters<Product>(sql, useruuid);
        }
        public static Product GetPhoto1ByProductUuid(string productuuid)
        {
            string sql = @"select productuuid, Photo1, Photo1ContentType from product
where Deleted=0 and productuuid = @productuuid";
            return GetEntityByParameters<Product>(sql, productuuid);
        }
        public static Product GetPhoto2ByProductUuid(string productuuid)
        {
            string sql = @"select productuuid, Photo2, Photo2ContentType from product
where Deleted=0 and productuuid = @productuuid";
            return GetEntityByParameters<Product>(sql, productuuid);
        }
        public static Product GetPhoto3ByProductUuid(string productuuid)
        {
            string sql = @"select productuuid, Photo3, Photo3ContentType from product
where Deleted=0 and productuuid = @productuuid";
            return GetEntityByParameters<Product>(sql, productuuid);
        }
        public static Product GetProductByProductUuid(string productuuid)
        {
            string sql = @"select * from product
where Deleted=0 and productuuid = @productuuid";
            return GetEntityByParameters<Product>(sql, productuuid);
        }
    }
}