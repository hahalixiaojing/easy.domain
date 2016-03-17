using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Application
{
    public interface INewReturnTransformer<T> : IReturnTransformer
    {
        new T GetValue(ReturnContext context, object data);

    }
}
