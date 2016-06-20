using Easy.Domain.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Test.Application
{
    public class DemoApplication : BaseApplication
    {
        public IReturn FindById()
        {
            return this.WriteAndPublishDomainEvent("FindById", "data", new Demo.FindByIdDomainEvents.DemoDomainEvent("aa"));
        }
    }
}
