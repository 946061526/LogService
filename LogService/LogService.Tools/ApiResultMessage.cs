namespace LogService.Tools
{
    /// <summary>
    /// API自定义消息
    /// </summary>
    public static class ApiResultMessage
    {
        /// <summary>
        /// 参数不能为空
        /// </summary>
        public const string ParamIsNullOrEmpty = "参数不能为空";

        /// <summary>
        /// 无数据
        /// </summary>
        public const string NoData = "未找到相关数据";

        /// <summary>
        /// 成功
        /// </summary>
        public const string Success = "success";

        /// <summary>
        /// 失败
        /// </summary>
        public const string Fail = "失败";

        /// <summary>
        /// 无Token
        /// </summary>
        public const string NoToken = "token为空";

        /// <summary>
        /// 登陆超时
        /// </summary>
        public const string LoginTimeout = "用户信息已过期，请重新登录";
    }
}
