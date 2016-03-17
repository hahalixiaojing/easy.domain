using System;

namespace Easy.Domain.Base
{
    /// <summary>
    /// 业务规则错误
    /// </summary>
    public class BrokenRuleException : Exception
    {
        public BrokenRuleException(string errorCode,string message) : base(message)
        {
            this.ErrorCode = errorCode;
        }
        public string ErrorCode { get; private set; }
    }
}
