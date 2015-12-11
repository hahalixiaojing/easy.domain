using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Domain.Validators
{
    class ValidateItem<T>
    {
        public ValidateItem(IValidate<T> validate, string messageKey)
        {
            this.Validate = validate;
            this.MessageKey = messageKey;
        }
        public IValidate<T> Validate
        {
            get;
            private set;
        }
        public String MessageKey
        {
            get;
            private set;
        }
    }
}
