using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.Domain.Event;

namespace Easy.Domain.Test.Application.Demo.FindByIdDomainEvents
{
    public class DemoDomainEvent : IDomainEvent
    {
        public DemoDomainEvent(string a)
        {
            this.A = a;
        }
        public string A
        {
            get;private set;
        }

        public DateTime OccurredOn
        {
            get;private set;
        }
    }
}
