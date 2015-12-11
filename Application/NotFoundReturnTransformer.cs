using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Application
{
    class NotFoundReturnTransformer : IReturnTransformer
    {
        public bool IsMapped(ReturnContext context)
        {
            return true;
        }

        public dynamic GetValue(ReturnContext context, object data)
        {
            return "not found error" + context.SystemId + ":" + context.Version;
        }
    }
}
