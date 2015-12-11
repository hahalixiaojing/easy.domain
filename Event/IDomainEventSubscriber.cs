using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Event
{

    public interface ISubscriber
    {

    }

    /// <summary>
    /// 领域事件订阅者
    /// </summary>
    public interface IDomainEventSubscriber<T> :ISubscriber where T : IDomainEvent
    {
        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="aDomainEvent"></param>
        void HandleEvent(T aDomainEvent);
        /// <summary>
        /// 事件类型
        /// </summary>
        Type SuscribedToEventType
        {
            get;
        }
    }
}
