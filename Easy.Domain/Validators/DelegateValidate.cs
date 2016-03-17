using Easy.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Domain.Validators
{
    public delegate Boolean IsSatisfy<Model>(Model m) where Model : BrokenRuleObject;

    class DelegateValidate<Model> : IValidate<Model> where Model : BrokenRuleObject
    {
        private IsSatisfy<Model> validateDelegate;

        public DelegateValidate(IsSatisfy<Model> del)
        {
            this.validateDelegate = del;
        }

        public bool IsSatisfy(Model model)
        {
            return validateDelegate.Invoke(model);
        }
    }
}
