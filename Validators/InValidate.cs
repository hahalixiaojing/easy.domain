using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Easy.Domain.Validators
{
    public class InValidate<Model, T> : BaseValidate<Model, T> where T : IEquatable<T>
    {
        T[] values;

        public InValidate(String propertyName, T[] values)
            : base(propertyName)
        {
            this.values = values;
        }
        public InValidate(Expression<Func<Model, T>> expression, T[] values)
            : base(expression)
        {
            this.values = values;
        }

        public override bool IsSatisfy(Model model)
        {
            if (this.values == null || this.values.Length == 0)
            {
                return false;
            }

            T value = this.GetObjectAttrValue(model);

            if (this.values.Count(m => m.Equals(value)) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
