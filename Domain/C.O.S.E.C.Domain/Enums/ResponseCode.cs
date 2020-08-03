namespace C.O.S.E.C.Domain.Enums
{
    /// <summary>
    /// HTTP状态响应码
    /// </summary>
    public enum ResponseCode
    {
        /// <summary>
        /// 请求成功
        /// </summary>
        OK = 200,
        /// <summary>
        /// 该请求已成功，并因此创建了一个新的资源。
        /// </summary>
        Created = 201,
        /// <summary>
        /// 还未响应，没有结果。
        /// </summary>
        Accepted = 202,
        /// <summary>
        /// 不需要返回任何实体内容
        /// </summary>
        NoContent = 204,
        /// <summary>
        /// 参数错误
        /// </summary>
        BadRequest = 400,
        /// <summary>
        /// 拒绝或者禁止访问（无权限访问）
        /// </summary>
        Forbidden = 403,
        /// <summary>
        /// 未找到资源
        /// </summary>
        NotFound = 404,
        /// <summary>
        /// 请求方式错误
        /// </summary>
        MethodNotAllowed = 405,
        /// <summary>
        /// 服务器异常
        /// </summary>
        ServerError = 500,
        /// <summary>
        /// 请求超时
        /// </summary>
        BadGateway = 502,
        /// <summary>
        /// 服务器维护或停机
        /// </summary>
        ServiceUnavailable

    }
}
