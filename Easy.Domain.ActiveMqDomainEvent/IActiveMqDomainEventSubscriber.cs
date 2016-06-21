using Easy.Domain.Event;

namespace Easy.Domain.ActiveMqDomainEvent
{
    public interface IActiveMqDomainEventSubscriber : ISubscriber
    {
        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="aDomainEvent"></param>
        void HandleEvent(string aDomainEvent);
    }
}
