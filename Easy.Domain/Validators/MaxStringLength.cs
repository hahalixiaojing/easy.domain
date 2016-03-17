using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Easy.Domain.Validators
{
    public class MaxStringLength<T> : BaseValidate<T, String>
    {
        private Int32 maxLength;

        public MaxStringLength(String propertyName, Int32 maxLength)
            : base(propertyName)
        {
            this.maxLength = maxLength;
        }
        public MaxStringLength(Expression<Func<T, String>> expression, Int32 maxLength)
            : base(expression)
        {
            this.maxLength = maxLength;
        }
        public override Boolean IsSatisfy(T model)
        {
            String value = this.GetObjectAttrValue(model);

            if (String.IsNullOrEmpty(value))
            {
                return true;
            }

            if (value.Length > this.maxLength)
            {
                return false;
            }
            return true;
        }
    }
}
