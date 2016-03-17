using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Test.Application.Demo.FindById
{
    public class Demo2ReturnTransformer: Easy.Domain.Application.INewReturnTransformer<DemoModel2>
    {

        public DemoModel2 GetValue(Domain.Application.ReturnContext context, object data)
        {
            return new DemoModel2();
        }

        public int Order
        {
            get { return 0; }
        }

        public bool IsMapped(Domain.Application.ReturnContext context)
        {
            return context.SystemId == "demo2";
        }

        dynamic Domain.Application.IReturnTransformer.GetValue(Domain.Application.ReturnContext context, object data)
        {
            throw new NotImplementedException();
        }
    }
}
