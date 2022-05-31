using System;

namespace LogService.Tools
{
    /// <summary>
    /// API返回规范（code=0 表示成功，否则表示失败）
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    [Serializable]
    public class ApiResultModel<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        private int _code = (int)ApiResultCode.Fail;
        /// <summary>
        /// Gets or sets 状态码
        /// </summary>
        public int code { get => _code; set => _code = value; }

        /// <summary>
        /// 数据
        /// </summary>
        private T _data;
        /// <summary>
        /// Gets or sets 数据
        /// </summary>
        public T data { get => _data; set => _data = value; }

        /// <summary>
        /// 消息
        /// </summary>
        private string _message;
        /// <summary>
        /// Gets or sets 消息
        /// </summary>
        public string message { get => _message; set => _message = value; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResultModel{T}"/> class.
        /// 默认返回失败，可传状态码
        /// </summary>
        /// <param name="apiResultCode">状态码</param>
        public ApiResultModel(ApiResultCode apiResultCode = ApiResultCode.Fail, string message = "")
        {
            _code = (int)apiResultCode;
            _data = default(T);
            _message = apiResultCode == ApiResultCode.Success ? ApiResultMessage.Success : message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResultModel{T}"/> class.
        /// 默认返回，传自定义状态码
        /// </summary>
        /// <param name="customerCode">自定义状态码</param>
        public ApiResultModel(int customerCode)
        {
            _code = customerCode;
            _data = default(T);
            _message = string.Empty;
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message"></param>
        public void IsSuccessed(T data = default(T))
        {
            _code = (int)ApiResultCode.Success;
            _message = ApiResultMessage.Success;
            _data = data;
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="message"></param>
        public void IsFailed(string message = ApiResultMessage.Fail)
        {
            _code = (int)ApiResultCode.Fail;
            _message = message;
        }
    }
}
