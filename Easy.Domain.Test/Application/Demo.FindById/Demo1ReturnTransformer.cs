using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Test.Application.Demo.FindById
{
    public class Demo1ReturnTransformer:Easy.Domain.Application.INewReturnTransformer<DemoModel1>
    {
        public DemoModel1 GetValue(Domain.Application.ReturnContext context, object data)
        {
            return new DemoModel1();
        }

        public int Order
        {
            get { return 0; }
        }

        public bool IsMapped(Domain.Application.ReturnContext context)
        {
            return context.SystemId == "demo1";
        }
        dynamic Domain.Application.IReturnTransformer.GetValue(Domain.Application.ReturnContext context, object data)
        {
            throw new NotImplementedException();
        }
    }
}
