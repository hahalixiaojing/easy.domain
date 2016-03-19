using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.Domain.Application;

namespace Easy.Domain.Test.Application.Demo2.Select
{
    public class Demo2SelectReturnTransformer : Domain.Application.INewReturnTransformer<string>
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public string GetValue(ReturnContext context, object data)
        {
            return data.ToString();
        }

        public bool IsMapped(ReturnContext context)
        {
            return true;
        }

        dynamic IReturnTransformer.GetValue(ReturnContext context, object data)
        {
            throw new NotImplementedException();
        }
    }
}
