using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Easy.Domain.Validators
{
    public class ExcludeCharsValidate<T> : BaseValidate<T, String>
    {
        Char[] excludeChars;
        public ExcludeCharsValidate(String propertyName,Char[] excludeChars)
            : base(propertyName)
        {
            this.excludeChars = excludeChars;
        }
        public ExcludeCharsValidate(Expression<Func<T, String>> expression, Char[] excludeChars)
            : base(expression)
        {
            this.excludeChars = excludeChars;
        }

        public override Boolean IsSatisfy(T model)
        {
            String value = this.GetObjectAttrValue(model);

            if (String.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            return Validators.ExcludeCharsValidate(value, this.excludeChars);
        }
    }
}
