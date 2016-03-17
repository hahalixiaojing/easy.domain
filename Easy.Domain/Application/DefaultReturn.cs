using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Application
{
    public class DefaultReturn<T> : IReturn
    {
        private readonly T obj;
        private readonly IList<IReturnTransformer> transformer;
        private static readonly NotFoundReturnTransformer notFound = new NotFoundReturnTransformer();

        public DefaultReturn(T obj, IList<IReturnTransformer> transformers)
        {
            this.obj = obj;
            this.transformer = transformers;
        }
         
        public dynamic Result(ReturnContext context)
        {
            IReturnTransformer value = transformer.FirstOrDefault(m => m.IsMapped(context));
            if (value == null)
            {
                return notFound.GetValue(context, null);
            }
            return value.GetValue(context, this.obj);
        }

        public INewReturnTransformer<RESULT> GetReturnTransformer<RESULT>(ReturnContext context)
        {
            return transformer.FirstOrDefault(m => m.IsMapped(context)) as INewReturnTransformer<RESULT>;
        }

        public RESULT Result<RESULT>(INewReturnTransformer<RESULT> transformer, IReturn @return)
        {
            return transformer.GetValue(null, (@return as DefaultReturn<T>).obj);
        }
    }
}
