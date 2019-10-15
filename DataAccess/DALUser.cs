using DataAccessCommon;
using DataEntity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using static DataAccessCommon.MyStaticDBHelper;

namespace DataAccess
{
    public class DALUser
    {
        private static ILogger _logger = null;
        public static void SetLogger(ILogger logger)
        {
            _logger = logger;
        }
        public static void CreateUser(User user)
        {
            string sql = @"insert into user (uuid,name,address,IDCARD,Scope,DeliveryFee, PhoneNumber, alipay, wechat, loginid, password, busytime) 
values (@uuid,@name,@address,@IDCARD,@Scope,@DeliveryFee,@PhoneNumber,@alipay,@wechat, @loginid, @password, @busytime)";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@uuid", DbType.String, user.uuid));
            parameterList.Add(new MyDBParameter("@name", DbType.String, user.name));
            parameterList.Add(new MyDBParameter("@address", DbType.String, user.address));
            parameterList.Add(new MyDBParameter("@IDCARD", DbType.String, user.IDCARD));
            parameterList.Add(new MyDBParameter("@Scope", DbType.String, user.Scope));
            parameterList.Add(new MyDBParameter("@DeliveryFee", DbType.Single, user.DeliveryFee));
            parameterList.Add(new MyDBParameter("@PhoneNumber", DbType.String, user.PhoneNumber));
            parameterList.Add(new MyDBParameter("@alipay", DbType.String, user.alipay));
            parameterList.Add(new MyDBParameter("@wechat", DbType.String, user.wechat));
            parameterList.Add(new MyDBParameter("@loginid", DbType.String, user.loginid));
            parameterList.Add(new MyDBParameter("@password", DbType.String, user.passsword));
            parameterList.Add(new MyDBParameter("@busytime", DbType.String, user.busytime));
            ExecuteNonQuery(parameterList, sql, _logger);
        }
        public static void UpdateUser(User user)
        {
            string sql = @"update user set name=@name,address=@address,IDCARD=@IDCARD,Scope=@Scope,DeliveryFee=@DeliveryFee
, PhoneNumber=@PhoneNumber, alipay=@alipay, wechat=@wechat, busytime=@busytime
where uuid = @uuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@name", DbType.String, user.name));
            parameterList.Add(new MyDBParameter("@address", DbType.String, user.address));
            parameterList.Add(new MyDBParameter("@IDCARD", DbType.String, user.IDCARD));
            parameterList.Add(new MyDBParameter("@Scope", DbType.String, user.Scope));
            parameterList.Add(new MyDBParameter("@DeliveryFee", DbType.Single, user.DeliveryFee));
            parameterList.Add(new MyDBParameter("@PhoneNumber", DbType.String, user.PhoneNumber));
            parameterList.Add(new MyDBParameter("@alipay", DbType.String, user.alipay));
            parameterList.Add(new MyDBParameter("@wechat", DbType.String, user.wechat));
            parameterList.Add(new MyDBParameter("@busytime", DbType.String, user.busytime));
            parameterList.Add(new MyDBParameter("@uuid", DbType.String, user.uuid));
            ExecuteNonQuery(parameterList, sql, _logger);
        }
        public static void UpdatePhoto1(User user)
        {
            string sql = @"update user set Photo1=@Photo1, Photo1ContentType=@Photo1ContentType
where uuid = @uuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@Photo1", DbType.Binary, user.Photo1));
            parameterList.Add(new MyDBParameter("@Photo1ContentType", DbType.String, user.Photo1ContentType));
            parameterList.Add(new MyDBParameter("@uuid", DbType.String, user.uuid));
            ExecuteNonQuery(parameterList, sql, _logger);
        }
        public static void UpdatePhoto2(User user)
        {
            string sql = @"update user set Photo2=@Photo2, Photo2ContentType=@Photo2ContentType
where uuid = @uuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@Photo2", DbType.Binary, user.Photo2));
            parameterList.Add(new MyDBParameter("@Photo2ContentType", DbType.String, user.Photo2ContentType));
            parameterList.Add(new MyDBParameter("@uuid", DbType.String, user.uuid));
            ExecuteNonQuery(parameterList, sql, _logger);
        }
        public static void DeleteUser(User user)
        {
            string sql = @"update user set Deleted=1 where uuid = @uuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@uuid", DbType.String, user.uuid));
            ExecuteNonQuery(parameterList, sql, _logger);
        }
        public static User FindUserByUuid(string uuid)
        {
            string sql = @"select * from user where uuid = @uuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@uuid", DbType.String, uuid));
            return ToEntityByEmit.GetEntity<User>(ExecuteDataset(parameterList, sql, _logger).Tables[0]);
        }
        public static User FindUserByLoginid(string loginid)
        {
            string sql = @"select * from user where loginid = @loginid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@loginid", DbType.String, loginid));
            return ToEntityByEmit.GetEntity<User>(ExecuteDataset(parameterList, sql, _logger).Tables[0]);
        }
        public static List<User> GetAllUserList(int pageIndex, int pageSize)
        {
            string sql = @"select * from user where Deleted=0 limit @pageIndex, @pageSize";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@pageIndex", DbType.Int16, (pageIndex - 1) * pageSize));
            parameterList.Add(new MyDBParameter("@pageSize", DbType.Int16, pageSize));
            return ToEntityByEmit.GetList<User>(ExecuteDataset(parameterList, sql, _logger));
        }
        public static bool LoginUser(string userName, String password, out User user)
        {
            string sql = "select * from user where Deleted=0 and loginid=@loginid and password=@password";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@loginid", DbType.String, userName));
            parameterList.Add(new MyDBParameter("@password", DbType.String, password));
            user = ToEntityByEmit.GetEntity<User>(ExecuteDataset(parameterList, sql, _logger).Tables[0]);
            if (user == null)
                return false;
            else
                return true;
        }
        public static User GetPhoto1ByUseruuid(string useruuid)
        {
            string sql = @"select uuid, Photo1, Photo1ContentType from user
where uuid = @useruuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@useruuid", DbType.String, useruuid));
            return ToEntityByEmit.GetEntity<User>(ExecuteDataset(parameterList, sql, _logger).Tables[0]);
        }
        public static User GetPhoto2ByUseruuid(string useruuid)
        {
            string sql = @"select uuid, Photo2, Photo2ContentType from user
where uuid = @useruuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@useruuid", DbType.String, useruuid));
            return ToEntityByEmit.GetEntity<User>(ExecuteDataset(parameterList, sql, _logger).Tables[0]);
        }
        public static void UpdateAlipayBarcode(User user)
        {
            string sql = @"update user set alipayBarcode=@alipayBarcode, alipayBarcodeContentType=@alipayBarcodeContentType
where uuid = @uuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@alipayBarcode", DbType.Binary, user.alipayBarcode));
            parameterList.Add(new MyDBParameter("@alipayBarcodeContentType", DbType.String, user.alipayBarcodeContentType));
            parameterList.Add(new MyDBParameter("@uuid", DbType.String, user.uuid));
            ExecuteNonQuery(parameterList, sql, _logger);
        }
        public static void UpdateWechatBarcode(User user)
        {
            string sql = @"update user set wechatBarcode=@wechatBarcode, wechatBarcodeContentType=@wechatBarcodeContentType
where uuid = @uuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@wechatBarcode", DbType.Binary, user.wechatBarcode));
            parameterList.Add(new MyDBParameter("@wechatBarcodeContentType", DbType.String, user.wechatBarcodeContentType));
            parameterList.Add(new MyDBParameter("@uuid", DbType.String, user.uuid));
            ExecuteNonQuery(parameterList, sql, _logger);
        }
        public static User GetAlipayByUseruuid(string useruuid)
        {
            string sql = @"select uuid, alipayBarcode, alipayBarcodeContentType from user
where uuid = @useruuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@useruuid", DbType.String, useruuid));
            return ToEntityByEmit.GetEntity<User>(ExecuteDataset(parameterList, sql, _logger).Tables[0]);
        }
        public static User GetWechatByUseruuid(string useruuid)
        {
            string sql = @"select uuid, wechatBarcode, wechatBarcodeContentType from user
where uuid = @useruuid";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@useruuid", DbType.String, useruuid));
            return ToEntityByEmit.GetEntity<User>(ExecuteDataset(parameterList, sql, _logger).Tables[0]);
        }
        public static List<User> GetUserListByPhoneNumber(string phoneNumber)
        {
            string sql = @"select * from user where PhoneNumber=@PhoneNumber and Deleted=0";
            var parameterList = new List<MyDBParameter>();
            parameterList.Add(new MyDBParameter("@PhoneNumber", DbType.String, phoneNumber));
            return ToEntityByEmit.GetList<User>(ExecuteDataset(parameterList, sql, _logger));
        }
        public static long GetCount()
        {
            string sql = @"select count(1) as cnt from user where Deleted=0";
            var count = ToEntityByEmit.GetEntity<Count>(ExecuteDataset(null, sql, _logger).Tables[0]);
            return count.cnt;
        }
    }
}
