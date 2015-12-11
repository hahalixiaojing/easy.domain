using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Easy.Domain.Validators
{
    public class EmailValidate<T> : BaseValidate<T, String>
    {
        public EmailValidate(String propertyName)
            : base(propertyName)
        {

        }
        public EmailValidate(Expression<Func<T, String>> expression)
            : base(expression)
        {

        }
        public override bool IsSatisfy(T model)
        {
            String value = this.GetObjectAttrValue(model);

            if (String.IsNullOrEmpty(value))
            {
                return true;
            }
            return Validators.EmailValidate(value);
        }
    }
}
