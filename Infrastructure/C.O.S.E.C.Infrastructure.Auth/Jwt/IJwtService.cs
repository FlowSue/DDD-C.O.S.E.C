//本地项目包

using C.O.S.E.C.Domain.Enums.Auth;
using C.O.S.E.C.Domain.Models;

namespace C.O.S.E.C.Infrastructure.Auth.Jwt
{
    /// <summary>
    /// Jwt服务[Interface]
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        string IssueJwt(TokenModel tokenModel);
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="username"></param>
        /// <param name="role"></param>
        /// <param name="project"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        string IssueJwt(string uid, string username, string rename, string role, string systemId, string project = "C.O.S.E.C", TokenTypeEnum tokenType = TokenTypeEnum.Web);

        /// <summary>
        /// 解析jwt字符串
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        TokenModel SerializeJWT(string jwtStr);
    }
}
