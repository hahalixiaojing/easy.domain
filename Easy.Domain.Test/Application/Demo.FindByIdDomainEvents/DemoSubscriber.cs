using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Test.Application.Demo.FindByIdDomainEvents
{
    public class DemoSubscriber : Easy.Domain.Event.IDomainEventSubscriber<DemoDomainEvent>
    {
        public Type SuscribedToEventType
        {
            get
            {
                return typeof(DemoDomainEvent);
            }
        }

        public void HandleEvent(DemoDomainEvent aDomainEvent)
        {
            throw new NotImplementedException();
        }
    }
}
