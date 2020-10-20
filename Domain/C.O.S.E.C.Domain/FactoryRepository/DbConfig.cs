//using C.O.S.E.C.Infrastructure.Config;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C.O.S.E.C.Domain.FactoryRepository
{
    public sealed class DbConfig
    {
        private static string ConnectionString { get; set; } = "Data Source=.;Initial Catalog=CosecCRM;Persist Security Info=True;User ID=sa;Password=123456";
        public DbConfig(string ConnectionString)
        {
            DbConfig.ConnectionString = ConnectionString;
        }
        public static SqlSugarClient GetDbInstance(string Conne = StringEx.str_empty, bool IsAutoCloseConnection = true)
        {
            try
            {
                string ConnectionString = string.Empty;
                if (string.IsNullOrEmpty(Conne))
                {
                    ConnectionString = DbConfig.ConnectionString;
                }
                else
                {
                    ConnectionString = Conne;
                }

                SqlSugarClient db = new SqlSugarClient(new List<ConnectionConfig>() {
                    new ConnectionConfig(){ ConfigId=1, DbType=DbType.SqlServer, ConnectionString=ConnectionString, InitKeyType=InitKeyType.Attribute,IsAutoCloseConnection=IsAutoCloseConnection },
                    //new ConnectionConfig(){ ConfigId=2, DbType=DbType.MySql, ConnectionString=connection, InitKeyType=InitKeyType.Attribute,IsAutoCloseConnection=IsAutoCloseConnection }
                });

                #region 全局过滤
                //var ComponentDbSystemId = ConfigSugar.GetAppString("ComponentDbSystemId");
                //db.QueryFilter.Add(new SqlFilterItem()//单表全局过滤器
                //{
                //    FilterValue = filterDb =>
                //    {
                //        return new SqlFilterResult() { Sql = " SystemId=@SystemId ", Parameters = new { SystemId = ComponentDbSystemId } };
                //    },
                //    IsJoinQuery = false
                //}).Add(new SqlFilterItem()//多表全局过滤器
                //{
                //    FilterValue = filterDb =>
                //    {
                //        return new SqlFilterResult() { Sql = " SystemId=@SystemId ", Parameters = new { SystemId = ComponentDbSystemId } };
                //    },
                //    IsJoinQuery = true
                //});
                #endregion
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine(sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                    Console.WriteLine();
                };
                return db;
            }
            catch (Exception ex)
            {
                throw new Exception($"连接数据库出错，请检查您的连接字符串，和网络。 ex:{ex.Message}");

            }
        }
    }
}
