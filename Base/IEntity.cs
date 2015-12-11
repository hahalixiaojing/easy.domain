using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Domain.Base
{
    public interface IEntity<T>
    {
        T Id { get; }
    }
}
