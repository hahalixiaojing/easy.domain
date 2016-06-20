using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.Domain.Event;

namespace Easy.Domain.Application
{
    public class TaskDomainEventManager : IDomainEventManager
    {
        private readonly IDictionary<String, IList<ISubscriber>> DOMAIN_EVENTS = new Dictionary<String, IList<ISubscriber>>();

        private IList<ISubscriber> GetDomainEvents(string name)
        {
            return this.DOMAIN_EVENTS[name];
        }

        public void PublishEvent<T>(string name, T obj) where T : IDomainEvent
        {
            var domainEvents = this.GetDomainEvents(name);
            var domainEventPublisher = new DomainEventPublisher();
            foreach (var @event in domainEvents)
            {
                domainEventPublisher.Subscribe(@event as IDomainEventSubscriber<T>);
            }
            domainEventPublisher.Publish(obj);
        }

        public void RegisterSubscriber(string name, ISubscriber item)
        {
            if (this.DOMAIN_EVENTS.ContainsKey(name))
            {
                this.DOMAIN_EVENTS[name].Add(item);
            }
            else
            {
                var list = new List<ISubscriber>();
                list.Add(item);
                this.DOMAIN_EVENTS.Add(name, list);
            }
        }
    }
}
