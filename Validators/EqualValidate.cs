using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Easy.Domain.Validators
{
    public class EqualValidate<T, K> : BaseValidate<T, K> where K : IEquatable<K>
    {
        K value;
        public EqualValidate(string propertyName, K value)
            : base(propertyName)
        {
            this.value = value;
        }

        public EqualValidate(Expression<Func<T, K>> expression, K value)
            : base(expression)
        {
            this.value = value;
        }

        public override bool IsSatisfy(T model)
        {
            K pValue = this.GetObjectAttrValue(model);
            return pValue.Equals(this.value);
        }
    }
}
