using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Easy.Domain.Validators
{
    public class StringNullOrEmptyValidate<T> : BaseValidate<T, String>
    {
        public StringNullOrEmptyValidate(String propertyName)
            : base(propertyName)
        {

        }
        public StringNullOrEmptyValidate(Expression<Func<T, String>> expression)
            : base(expression)
        {

        }
        
        public override Boolean IsSatisfy(T model)
        {
            String value = this.GetObjectAttrValue(model);
            if (String.IsNullOrEmpty(value))
            {
                return false;
            }
            return true;
        }
    }
}
