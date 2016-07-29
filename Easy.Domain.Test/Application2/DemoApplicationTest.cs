using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Easy.Domain.ActiveMqDomainEvent;
using Easy.Domain.Application;
using NUnit.Framework;

namespace Easy.Domain.Test.Application2
{
    public class DemoApplicationTest
    {
        [SetUp]
        public void SetUp()
        {
            ActiveMqManager mq = new ActiveMqManager(@"activemq:tcp://127.0.0.1:61616?wireFormat.maxInactivityDuration=0", "test");

            ApplicationFactory.Instance().Register(new DemoApplication(new ActiveMqDomainEventManager(mq)));
        }

        [Test]
        public void AddTest()
        {
            int i = 0;
            while (i<50)
            {
                ApplicationFactory.Instance().Get<DemoApplication>().Add();
                Thread.Sleep(1000);
                i++;
            }
        }
    }
}
