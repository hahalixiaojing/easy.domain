using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.Domain.Application;
using Easy.Domain.Event;

namespace Easy.Domain.Test.Application
{
    public class Demo2Application : BaseApplication, IDemo2Service
    {
        public IReturn Select()
        {
            return this.Write("Select", "abc");
        }

        public IReturn TestDemo()
        {
            System.Diagnostics.Debug.WriteLine("测试里面的值");

            return this.Write(nameof(TestDemo), "aaaaa");
        }

        public IReturn TestDefaultValue()
        {
            return this.Write(nameof(TestDefaultValue), "defaultvalue");
        }

        public void PublishTest()
        {
            System.Diagnostics.Debug.WriteLine("测试里面的值");

            this.PublishEvent(nameof(PublishTest), new Evt());
        }
    }

    public class Evt : IDomainEvent
    {
        public DateTime OccurredOn
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
