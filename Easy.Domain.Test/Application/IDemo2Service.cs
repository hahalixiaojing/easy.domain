using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.Domain.Application;

namespace Easy.Domain.Test.Application
{
    public interface IDemo2Service
    {
        IReturn Select();
        IReturn TestDemo();
        IReturn TestDefaultValue();
    }
}
