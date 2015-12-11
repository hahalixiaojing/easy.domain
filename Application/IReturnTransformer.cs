using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Application
{
    public interface IReturnTransformer
    {
        bool IsMapped(ReturnContext context);
        dynamic GetValue(ReturnContext context, object data);
    }
}
