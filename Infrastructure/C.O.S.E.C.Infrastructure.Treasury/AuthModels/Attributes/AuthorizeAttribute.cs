//微软包
using Microsoft.AspNetCore.Authorization;
using C.O.S.E.C.Infrastructure.Auth.Enums;
//本地项目包

namespace C.O.S.E.C.Infrastructure.Auth.Attributes
{
    /// <summary>
    /// 自定义授权特性
    /// </summary>
    public sealed class AuthorizeAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        public AuthorizeAttribute(AuthPolicyEnum authPolicyEnum)
        {
            this.Policy = authPolicyEnum.ToString();
        }
    }
}
