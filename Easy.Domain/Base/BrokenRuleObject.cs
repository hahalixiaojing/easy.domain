using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Easy.Domain.Validators;

namespace Easy.Domain.Base
{
    [Serializable]
    public abstract class BrokenRuleObject
    {
        private List<BrokenRule> brokenRules;
        private BrokenRuleMessage brokenRuleMessage;

        private static EmptyBrokenRule emptyBrokenRule = new EmptyBrokenRule();

        public BrokenRuleObject()
        {
            this.brokenRules = new List<BrokenRule>();
            this.brokenRuleMessage = this.GetBrokenRuleMessages();
        }

        #region 验证相关
        protected abstract BrokenRuleMessage GetBrokenRuleMessages();
        public abstract Boolean Validate();
        public void AddBrokenRule(String messageKey)
        {
            this.brokenRules.Add(new BrokenRule(messageKey, this.brokenRuleMessage.GetRuleDescription(messageKey)));
        }
        public void AddBrokenRule(String messageKey, String propertyName)
        {
            this.brokenRules.Add(new BrokenRule(messageKey, this.brokenRuleMessage.GetRuleDescription(messageKey), propertyName));
        }
        public void AddBrokenRule(String messageKey, String propertyName, params String[] values)
        {
            BrokenRule brokenRule = new BrokenRule(messageKey, String.Format(this.brokenRuleMessage.GetRuleDescription(messageKey), values), propertyName);
        }
        public void AddBrokenRule<T,R>(String messageKey, Expression<Func<T, R>> expression, params String[] values)
        {
            String propertyName = this.GetPropertyName<T, R>(expression);
            BrokenRule brokenRule = new BrokenRule(messageKey, String.Format(this.brokenRuleMessage.GetRuleDescription(messageKey), values), propertyName);
            this.brokenRules.Add(brokenRule);
        }
        public void AddBrokenRule<T,R>(String messageKey, Expression<Func<T,R>> expression)
        {
            String propertyName = this.GetPropertyName<T, R>(expression);
            this.AddBrokenRule(messageKey, propertyName);
        }

       
        private String GetPropertyName<T, R>(Expression<Func<T, R>> expression)
        {
            String propertyPath = expression.Body.ToString();
            String propertyName = propertyPath.Substring(propertyPath.IndexOf(".") + 1);

            return propertyName;
        }
        public ReadOnlyCollection<BrokenRule> GetBrokenRules()
        {
            return this.brokenRules.AsReadOnly();
        }

        public BrokenRule FindBrokenRule<T, R>(Expression<Func<T, R>> expression)
        {
            String propertyName = this.GetPropertyName<T, R>(expression);
            return this.FindBrokenRule(propertyName);
        }

        public BrokenRule FindBrokenRule(String propertyName)
        {
            var rules = brokenRules.Where<BrokenRule>(rule => rule.PropertyName == propertyName);
            if (rules.Count() > 0)
            {
                return rules.First();
            }
            return emptyBrokenRule;
        }

        public void ClearBrokenRules()
        {
            this.brokenRules.Clear();
        }
        #endregion
    }
}
