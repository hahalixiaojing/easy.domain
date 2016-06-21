using Easy.Domain.Application;
using Easy.Domain.Test.Application2.Demo.AddDomainEvents;

namespace Easy.Domain.Test.Application2
{
    public class DemoApplication : BaseApplication
    {
        public DemoApplication(IDomainEventManager manager) : base(manager)
        {

        }

        public void Add()
        {
            this.PublishEvent(nameof(DemoApplication.Add), new AddDomainEvent("text email"));
        }
    }
}
