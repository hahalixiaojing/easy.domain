using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Event
{
    /// <summary>
    /// 领域事件发布
    /// </summary>
    public class DomainEventPublisher
    {
        private readonly List<ISubscriber> subscriberList = new List<ISubscriber>();
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="event"></param>
        public void Subscribe<T>(IDomainEventSubscriber<T> subscriber) where T : IDomainEvent
        {
            subscriberList.Add(subscriber);
        }
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="aDomainEvent"></param>
        public void Publish<T>(T aDomainEvent) where T : IDomainEvent
        {
            foreach (var subscriber in subscriberList)
            {
                var subscribedTo = subscriber as IDomainEventSubscriber<T>;

                if (subscribedTo != null && subscribedTo.SuscribedToEventType == aDomainEvent.GetType())
                {

                    Task.Factory.StartNew(() =>
                    {
                        subscribedTo.HandleEvent(aDomainEvent);
                    });

                }
            }
        }
    }
}
