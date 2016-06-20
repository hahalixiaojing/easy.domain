using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.Domain.Event;

namespace Easy.Domain.Application
{
    public interface IDomainEventManager
    {
        void RegisterSubscriber(string name, ISubscriber item);
        void PublishEvent<T>(string name, T obj) where T : IDomainEvent;
    }
}
