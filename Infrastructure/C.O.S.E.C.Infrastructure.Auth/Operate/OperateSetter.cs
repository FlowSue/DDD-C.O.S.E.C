//系统包
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using C.O.S.E.C.Domain.Models;
using System;
//本地项目包

namespace C.O.S.E.C.Infrastructure.Auth.Operate
{
    public class OperateSetter : IEntityBaseAutoSetter
    {
        private readonly TokenModel _tokenModel;

        public OperateSetter(IOperateInfo operateInfo)
        {
            _tokenModel = operateInfo.TokenModel;
            TokenStr = operateInfo.TokenStr;
        }

        /// <summary>创建人姓名</summary>
        /// <value>The name of the create.</value>
        public string CreateName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_tokenModel?.Rname))
                    return StringEx.str_empty;
                return _tokenModel?.Rname;

            }
        }

        /// <summary>创建人Id</summary>
        /// <value>The create identifier.</value>
        public string CreateId => _tokenModel?.Uid ?? StringEx.str_empty;

        /// <summary>创建时间</summary>
        /// <value>The create time.</value>
        public DateTime CreateTime => DateTime.Now;

        /// <summary>更新人姓名</summary>
        /// <value>The name of the update.</value>
        public string UpdateName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_tokenModel?.Rname))
                    return StringEx.str_empty;
                return _tokenModel?.Rname;
            }
        }

        /// <summary>更新人Id</summary>
        /// <value>The update identifier.</value>
        public string UpdateId => this._tokenModel?.Uid ?? StringEx.str_empty;

        /// <summary>更新时间</summary>
        /// <value>The update time.</value>
        public DateTime UpdateTime => DateTime.Now;

        public string TokenStr { get; }

        public string SystemId => this._tokenModel?.SystemId;
    }
}
