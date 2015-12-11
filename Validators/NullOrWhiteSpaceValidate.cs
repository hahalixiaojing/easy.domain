using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Easy.Domain.Validators
{
    public class NullOrWhiteSpaceValidate<T> : BaseValidate<T, String>
    {
        public NullOrWhiteSpaceValidate(String propertyName)
            : base(propertyName)
        {

        }
        public NullOrWhiteSpaceValidate(Expression<Func<T, String>> expression)
            : base(expression)
        {

        }
        public override bool IsSatisfy(T model)
        {
            String value = this.GetObjectAttrValue(model);
            if (String.IsNullOrWhiteSpace(value))
            {
                return false;
            }
            return true;
        }
    }
}
