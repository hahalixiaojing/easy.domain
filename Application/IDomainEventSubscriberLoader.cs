using Easy.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Application
{
    /// <summary>
    /// 领域事件加载接口
    /// </summary>
    public interface IDomainEventSubscriberLoader
    {
        IDictionary<string, IEnumerable<ISubscriber>> Find(IApplication application);
    }
}
