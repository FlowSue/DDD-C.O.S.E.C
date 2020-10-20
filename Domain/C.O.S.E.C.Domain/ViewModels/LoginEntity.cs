using System.ComponentModel;

namespace C.O.S.E.C.Domain.ViewModels
{
    /// <summary>
    /// 登录用实体
    /// </summary>
    public class LoginEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [DisplayName("用户名")]
        public string Username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        public string Password { get; set; }
        /// <summary>
        /// 系统
        /// </summary>
        [DisplayName("系统平台")]
        public int OS { get; set; }
    }
}
