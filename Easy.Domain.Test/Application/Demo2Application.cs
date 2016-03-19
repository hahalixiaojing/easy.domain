using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.Domain.Application;

namespace Easy.Domain.Test.Application
{
    public class Demo2Application : BaseApplication, IDemo2Service
    {
        public IReturn Select()
        {
            return this.Write("Select", "abc");
        }
    }
}
