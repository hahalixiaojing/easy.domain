using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Domain.Base
{
    [Serializable]
    class EmptyBrokenRule : BrokenRule
    {
        public EmptyBrokenRule()
            : base(String.Empty, String.Empty, String.Empty)
        {

        }
    }
}
