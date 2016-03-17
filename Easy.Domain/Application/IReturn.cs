using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Application
{
    public interface IReturn
    {
        dynamic Result(ReturnContext context);
        INewReturnTransformer<RET> GetReturnTransformer<RET>(ReturnContext context);
        RESULT Result<RESULT>(INewReturnTransformer<RESULT> transformer, IReturn @return);

    }
}
