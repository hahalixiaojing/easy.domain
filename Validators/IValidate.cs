using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Domain.Base;

namespace Easy.Domain.Validators
{
    public interface IValidate<T>
    {
        Boolean IsSatisfy(T model);
    }
}
