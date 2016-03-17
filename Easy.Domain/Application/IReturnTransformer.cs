using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Application
{
    public interface IReturnTransformer
    {
        /// <summary>
        /// 接口顺序，越大越靠前
        /// </summary>
        Int32 Order
        {
            get;
        }
        bool IsMapped(ReturnContext context);
        dynamic GetValue(ReturnContext context, object data);
    }
}
