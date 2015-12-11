using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Easy.Domain.Validators
{
    public class LessThanValidate<T, K> : BaseValidate<T, K> where K : IComparable<K>
    {
        K value;
        public LessThanValidate(string propertyName, K value)
            : base(propertyName)
        {
            this.value = value;
        }

        public LessThanValidate(Expression<Func<T, K>> expression, K value)
            : base(expression)
        {
            this.value = value;
        }

        public override Boolean IsSatisfy(T model)
        {
            K pValue = this.GetObjectAttrValue(model);

            return pValue.CompareTo(this.value) < 0;
        }
    }
}
