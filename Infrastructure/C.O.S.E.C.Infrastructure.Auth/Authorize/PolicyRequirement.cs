//微软包
using Microsoft.AspNetCore.Authorization;

namespace C.O.S.E.C.Infrastructure.Auth.Authorize
{
    /// <summary>
    /// 授权策略
    /// </summary>
    public class PolicyRequirement : IAuthorizationRequirement
    {
        public PolicyRequirement(bool isNeedAuthorizeds = true)
        {
            this.IsNeedAuthorized = isNeedAuthorizeds;
        }
        public PolicyRequirement(string role, bool isNeedAuthorizeds = true)
        {
            this.SetRequireRoles(role.Split(','));
            this.IsNeedAuthorized = isNeedAuthorizeds;
        }

        public bool IsNeedAuthorized { get; set; }

        private string[] requireRoles;

        public string[] GetRequireRoles()
        {
            return requireRoles;
        }

        public void SetRequireRoles(string[] value)
        {
            requireRoles = value;
        }
    }
}
