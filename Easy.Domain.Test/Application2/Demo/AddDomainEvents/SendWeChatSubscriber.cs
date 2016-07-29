using System;
using System.Threading;
using Easy.Domain.ActiveMqDomainEvent;

namespace Easy.Domain.Test.Application2.Demo.AddDomainEvents
{
    public class SendWeChatSubscriber : IActiveMqDomainEventSubscriber
    {
        public Type SuscribedToEventType
        {
            get
            {
                return typeof(AddDomainEvent);
            }
        }

        public void HandleEvent(string aDomainEvent)
        {
            System.Diagnostics.Debug.WriteLine("发送微信消息");
            System.Diagnostics.Debug.WriteLine("SendWeChatSubscriber线程ID=" + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
