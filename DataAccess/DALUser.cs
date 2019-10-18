using DataAccessCommon;
using DataEntity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using static DataAccessCommon.MyStaticDBHelper;

namespace DataAccess
{
    public class DALUser
    {
        public static void SetLogger(ILogger logger)
        {
            MyStaticDBHelper.logger = logger;
        }
        public static void CreateUser(User user)
        {
            string sql = @"insert into user (uuid,name,address,IDCARD,Scope,DeliveryFee, PhoneNumber, alipay, wechat, loginid, password, busytime) 
values (@uuid,@name,@address,@IDCARD,@Scope,@DeliveryFee,@PhoneNumber,@alipay,@wechat, @loginid, @password, @busytime)";
            ExecuteNonQueryByEntity(sql, user);
        }
        public static void UpdateUser(User user)
        {
            string sql = @"update user set name=@name,address=@address,IDCARD=@IDCARD,Scope=@Scope,DeliveryFee=@DeliveryFee
, PhoneNumber=@PhoneNumber, alipay=@alipay, wechat=@wechat, busytime=@busytime
where uuid = @uuid";
            ExecuteNonQueryByEntity(sql, user);
        }
        public static void UpdatePhoto1(User user)
        {
            string sql = @"update user set Photo1=@Photo1, Photo1ContentType=@Photo1ContentType
where uuid = @uuid";
            ExecuteNonQueryByEntity(sql, user);
        }
        public static void UpdatePhoto2(User user)
        {
            string sql = @"update user set Photo2=@Photo2, Photo2ContentType=@Photo2ContentType
where uuid = @uuid";
            ExecuteNonQueryByEntity(sql, user);
        }
        public static void DeleteUser(User user)
        {
            string sql = @"update user set Deleted=1 where uuid = @uuid";
            ExecuteNonQueryByEntity(sql, user);
        }
        public static User FindUserByUuid(string uuid)
        {
            string sql = @"select * from user where uuid = @uuid";
            return GetEntityByParameters<User>(sql, uuid);
        }
        public static User FindUserByLoginid(string loginid)
        {
            string sql = @"select * from user where loginid = @loginid";
            return GetEntityByParameters<User>(sql, loginid);
        }
        public static List<User> GetAllUserList(int pageIndex, int pageSize)
        {
            string sql = @"select * from user where Deleted=0 limit @pageIndex, @pageSize";
            return GetListByParameters<User>(sql, pageIndex, pageSize);
        }
        public static User LoginUser(string userName, String password)
        {
            string sql = "select * from user where Deleted=0 and loginid=@loginid and password=@password";
            return GetEntityByParameters<User>(sql, userName, password);
        }
        public static User GetPhoto1ByUseruuid(string useruuid)
        {
            string sql = @"select uuid, Photo1, Photo1ContentType from user
where uuid = @useruuid";
            return GetEntityByParameters<User>(sql, useruuid);
        }
        public static User GetPhoto2ByUseruuid(string useruuid)
        {
            string sql = @"select uuid, Photo2, Photo2ContentType from user
where uuid = @useruuid";
            return GetEntityByParameters<User>(sql, useruuid);
        }
        public static void UpdateAlipayBarcode(User user)
        {
            string sql = @"update user set alipayBarcode=@alipayBarcode, alipayBarcodeContentType=@alipayBarcodeContentType
where uuid = @uuid";
            ExecuteNonQueryByEntity(sql, user);
        }
        public static void UpdateWechatBarcode(User user)
        {
            string sql = @"update user set wechatBarcode=@wechatBarcode, wechatBarcodeContentType=@wechatBarcodeContentType
where uuid = @uuid";
            ExecuteNonQueryByEntity(sql, user);
        }
        public static User GetAlipayByUseruuid(string useruuid)
        {
            string sql = @"select uuid, alipayBarcode, alipayBarcodeContentType from user
where uuid = @useruuid";
            return GetEntityByParameters<User>(sql, useruuid);
        }
        public static User GetWechatByUseruuid(string useruuid)
        {
            string sql = @"select uuid, wechatBarcode, wechatBarcodeContentType from user
where uuid = @useruuid";
            return GetEntityByParameters<User>(sql, useruuid);
        }
        public static List<User> GetUserListByPhoneNumber(string phoneNumber)
        {
            string sql = @"select * from user where PhoneNumber=@PhoneNumber and Deleted=0";
            return GetListByParameters<User>(sql, phoneNumber);
        }
        public static Count GetCount()
        {
            string sql = @"select count(1) as cnt from user where Deleted=0";
            return GetEntityByParameters<Count>(sql);
        }
    }
}
