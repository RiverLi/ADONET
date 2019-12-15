using DataAccessCommon;
using DataEntity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using static DataAccessCommon.MyStaticDBHelper;

namespace DataAccess
{
    public class DALPayRecord
    {
        public static void CreatePayRecord(Pay_Record pay_record)
        {
            string sql = @"insert into `pay_record` (
orderuuid,
pay_method,
response,
transaction_date
) 
values
(
@orderuuid,
@pay_method,
@response,
@transaction_date
)";
            ExecuteNonQueryByEntity(sql, pay_record);
        }
        public static void UpdatePayRecord(Pay_Record pay_record)
        {
            string sql = @"update `pay_record` set 
pay_method=@pay_method
response=@response,
transaction_date=@transaction_date
where orderuuid = @orderuuid";
            ExecuteNonQueryByEntity(sql, pay_record);
        }
        public static void DeletePayRecord(Pay_Record pay_record)
        {
            string sql = @"update from `pay_record` set Deleted=1 where orderuuid = @orderuuid";
            ExecuteNonQueryByEntity(sql, pay_record);
        }
        public static Pay_Record FindPayRecordByUuid(string orderuuid)
        {
            string sql = @"select * from `pay_record` where orderuuid = @orderuuid";
            return GetEntityByParameters<Pay_Record>(sql, orderuuid);
        }
        public static List<Pay_Record> GetAllPayRecordList(int pageIndex, int pageSize)
        {
            string sql = @"select * from `pay_record` where Deleted=0 limit @pageIndex, @pageSize";
            return GetListByParameters<Pay_Record>(sql, (pageIndex - 1) * pageSize, pageSize);
        }
    }
}
