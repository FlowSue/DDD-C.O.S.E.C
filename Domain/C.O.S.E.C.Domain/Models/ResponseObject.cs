using C.O.S.E.C.Domain.Enums;

namespace C.O.S.E.C.Domain.Models
{
    /// <summary>
    /// 响应数据
    /// </summary>
    public class ResponseObject
    {
        /// <summary>
        /// 接口响应码
        /// </summary>
        public ResponseCode Code { get; set; }
        /// <summary>
        /// 接口响应消息
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 接口响应数据
        /// </summary>
        public object Data { get; set; }
    }
    /// <summary>
    /// 响应数据
    /// </summary>
    public class ResponseObject<T>
    {
        /// <summary>
        /// 接口响应码
        /// </summary>
        public ResponseCode Code { get; set; }
        /// <summary>
        /// 接口响应消息
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 接口响应数据
        /// </summary>
        public T Data { get; set; }
    }
}
