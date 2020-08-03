//本地项目包
using C.O.S.E.C.Infrastructure.Auth.Models;

namespace C.O.S.E.C.Infrastructure.Auth.Operate
{
    /// <summary>
    /// 操作人信息[interface]
    /// </summary>
    public interface IOperateInfo
    {
        /// <summary>登录人信息</summary>
        /// <value>The authentication base.</value>
        TokenModel TokenModel { get; }

        /// <summary>登录token</summary>
        /// <value>The token.</value>
        string TokenStr { get; }
    }
}
