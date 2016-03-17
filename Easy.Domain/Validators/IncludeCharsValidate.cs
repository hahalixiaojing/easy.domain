using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Easy.Domain.Validators
{
    public class IncludeCharsValidate<T> : BaseValidate<T, String>
    {
        Char[] includeChars;
        public IncludeCharsValidate(String propertyName,Char[] includeChars)
            : base(propertyName)
        {
            this.includeChars = includeChars;
        }
        public IncludeCharsValidate(Expression<Func<T, String>> expression, Char[] includeChars)
            : base(expression)
        {
            this.includeChars = includeChars;
        }

        public override Boolean IsSatisfy(T model)
        {
            String value = this.GetObjectAttrValue(model);

            if (String.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            return Validators.IncludeCharsValidate(value, this.includeChars);
        }
    }
}
