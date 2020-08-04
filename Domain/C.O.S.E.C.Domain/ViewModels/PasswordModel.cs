using System.ComponentModel;

namespace C.O.S.E.C.Domain.ViewModels
{
    /// <summary>
    /// 密码保存实体类
    /// </summary>
    public class PasswordModel
    {
        /// <summary>
        /// 获得/设置 原密码
        /// </summary>
        [DisplayName("原密码")]
        public string Password { get; set; } = "";

        /// <summary>
        /// 获得/设置 新密码
        /// </summary>
        [DisplayName("新密码")]
        public string NewPassword { get; set; } = "";

        /// <summary>
        /// 获得/设置 确认密码
        /// </summary>
        [DisplayName("确认密码")]
        public string ConfirmPassword { get; set; } = "";
    }
}
