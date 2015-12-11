using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Domain.Event
{
    /// <summary>
    /// 领域事件对象
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// 发生时间
        /// </summary>
        DateTime OccurredOn
        {
            get;
        }
    }
}
