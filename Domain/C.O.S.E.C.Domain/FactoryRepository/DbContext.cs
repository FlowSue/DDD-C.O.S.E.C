using C.O.S.E.C.Domain.InterfaceDrivers;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using C.O.S.E.C.Infrastructure.Config;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace C.O.S.E.C.Domain.Factory
{
    public class DbContext<T> where T : class, IEntity<T>, new()
    {
        private readonly AllConfigModel allConfigModel;
        protected readonly IEntityBaseAutoSetter setter;
        public DbContext(AllConfigModel _allConfigModel, IEntityBaseAutoSetter _setter)
        {
            allConfigModel = _allConfigModel;
            setter = _setter;
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = _allConfigModel.ConnectionStringsModel.SqlServerDatabase,
                DbType = DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了

            });
            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(_allConfigModel.ConnectionStringsModel.SqlServerDatabase);
                Console.WriteLine(sql + "\r\n" +
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
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
        }

        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
        //public SimpleClient<SystemModule> StudentDb { get { return new SimpleClient<SystemModule>(Db); } }//用来处理Student表的常用操作
        //public SimpleClient<UserInfo> UserDb { get { return new SimpleClient<UserInfo>(Db); } }//用来处理School表的常用操作
        public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }//用来处理T表的常用操作


        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList()
        {
            return CurrentDb.GetList();
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(dynamic id)
        {
            return CurrentDb.Delete(id);
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(T obj)
        {
            return CurrentDb.Update(obj);
        }

    }
}
