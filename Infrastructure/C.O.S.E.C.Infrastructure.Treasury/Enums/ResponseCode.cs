namespace C.O.S.E.C.Infrastructure.Treasury.Enums
{
    public enum ResponseCode
    {
        /// <summary>
        /// 请求成功
        /// </summary>
        success = 200,
        /// <summary>
        /// 参数错误
        /// </summary>
        error = 400,
        /// <summary>
        ///拒绝或者禁止访问（无权限访问）
        /// </summary>
        noAccess = 403,
        /// <summary>
        /// 地址不存在
        /// </summary>
        notfound = 404,
        /// <summary>
        /// 请求方式错误
        /// </summary>
        methoderror = 405,
        /// <summary>
        /// 请求超时
        /// </summary>
        timeout = 502,
        /// <summary>
        /// 服务器异常
        /// </summary>
        exception = 500,
    }
}
