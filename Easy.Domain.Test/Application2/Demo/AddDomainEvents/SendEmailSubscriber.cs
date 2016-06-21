using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.Domain.ActiveMqDomainEvent;
using Newtonsoft.Json;

namespace Easy.Domain.Test.Application2.Demo.AddDomainEvents
{
    public class SendEmailSubscriber : IActiveMqDomainEventSubscriber
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
            var evt = JsonConvert.DeserializeObject<AddDomainEvent>(aDomainEvent);

            System.Console.WriteLine("send email");
        }
    }
}
