using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Application
{
    class DefaultReturnTransformer : IReturnTransformer
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public dynamic GetValue(ReturnContext context, object data)
        {
            return data;
        }

        public bool IsMapped(ReturnContext context)
        {
            return true;
        }
    }
}
