using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.Domain.Event;

namespace Easy.Domain.Test.Application2.Demo.AddDomainEvents
{
    public class AddDomainEvent : IDomainEvent
    {

        public AddDomainEvent(string text)
        {
            this.OccurredOn = DateTime.Now;
            this.Text = text;
        }
        public AddDomainEvent()
        {

        }
        public string Text { get; protected set; }

        public DateTime OccurredOn
        {
            get; protected set;
        }
    }
}
