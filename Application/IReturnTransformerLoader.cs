using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Application
{
    public interface IReturnTransformerLoader
    {
        IDictionary<string, IEnumerable<IReturnTransformer>> Find(IApplication application);
    }
}
