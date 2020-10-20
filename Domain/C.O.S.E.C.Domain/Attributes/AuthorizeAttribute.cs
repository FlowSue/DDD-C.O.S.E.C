using C.O.S.E.C.Domain.Enums.Auth;

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
