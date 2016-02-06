using Easy.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Validators
{
    /// <summary>
    /// 实体对象验证
    /// </summary>
    /// <typeparam name="Model"></typeparam>
    public class EntityValidation<Model> : IValidate<Model> where Model : BrokenRuleObject
    {
        private IDictionary<String, IList<ValidateItem<Model>>> propertyValidates;
        private IList<ValidateItem<Model>> classValidates;
        public EntityValidation()
        {
            propertyValidates = new Dictionary<String, IList<ValidateItem<Model>>>();
            classValidates = new List<ValidateItem<Model>>();
        }

        protected void Equal<T>(Expression<Func<Model, T>> expression, T value, String messageKey) where T : IEquatable<T>
        {
            var validate = new EqualValidate<Model, T>(expression, value);
            this.AddRule(expression, validate, messageKey);
        }
        protected void GreaterThan<T>(Expression<Func<Model, T>> expression, T value, String messageKey) where T : IComparable<T>
        {
            var validate = new GreaterThanValidate<Model, T>(expression, value);
            this.AddRule<T>(expression, validate, messageKey);
        }

        protected void LessThan<T>(Expression<Func<Model, T>> expression, T value, String messageKey) where T : IComparable<T>
        {
            var validate = new LessThanValidate<Model, T>(expression, value);
            this.AddRule(expression, validate, messageKey);
        }
        protected void Email(Expression<Func<Model, String>> expression, String messageKey)
        {
            var emailValidate = new EmailValidate<Model>(expression);
            this.AddRule<String>(expression, emailValidate, messageKey);
        }

        protected void IsNullOrWhiteSpace(Expression<Func<Model, String>> expression, String messageKey)
        {
            var nullOrderWhiteSpace = new NullOrWhiteSpaceValidate<Model>(expression);
            this.AddRule<String>(expression, nullOrderWhiteSpace, messageKey);
        }

        protected void IsNullOrEmpty(Expression<Func<Model, String>> expression, String messageKey)
        {
            var stringNulOrderEmpty = new StringNullOrEmptyValidate<Model>(expression);
            this.AddRule<String>(expression, stringNulOrderEmpty, messageKey);
        }
        protected void MaxStringLength(Expression<Func<Model, String>> expression, Int32 maxLength, String messageKey)
        {
            var maxStringLength = new MaxStringLength<Model>(expression, maxLength);
            this.AddRule<String>(expression, maxStringLength, messageKey);
        }
        protected void IncludeChars(Expression<Func<Model, String>> expression, Char[] chars, String messageKey)
        {
            var includeCharsValidate = new IncludeCharsValidate<Model>(expression, chars);
            this.AddRule<String>(expression, includeCharsValidate, messageKey);
        }
        protected void ExcludeChars(Expression<Func<Model, String>> expression, Char[] chars, String messageKey)
        {
            var exludeChars = new ExcludeCharsValidate<Model>(expression, chars);
            this.AddRule<String>(expression, exludeChars, messageKey);
        }
        protected void AddRule<TProperty>(Expression<Func<Model, TProperty>> expression, IValidate<Model> validate, String messageKey)
        {
            String propertyName = this.GetPropertyName<TProperty>(expression);

            if (propertyValidates.ContainsKey(propertyName))
            {
                propertyValidates[propertyName].Add(new ValidateItem<Model>(validate, messageKey));
            }
            else
            {
                var validates = new List<ValidateItem<Model>>();
                validates.Add(new ValidateItem<Model>(validate, messageKey));

                propertyValidates.Add(propertyName, validates);
            }
        }

        protected void AddRule(IValidate<Model> validate, String messageKey)
        {
            this.classValidates.Add(new ValidateItem<Model>(validate, messageKey));
        }
        protected void AddRule(IsSatisfy<Model> del, String messageKey)
        {
            var validate = new DelegateValidate<Model>(del);
            this.AddRule(validate, messageKey);
        }

        protected void AddRule<TProperty>(Expression<Func<Model, TProperty>> expression, IsSatisfy<Model> del, String messageKey)
        {
            var validate = new DelegateValidate<Model>(del);
            this.AddRule(expression, validate, messageKey);
        }

        private String GetPropertyName<TProperty>(Expression<Func<Model, TProperty>> expression)
        {
            String propertyPath = expression.Body.ToString();
            String propertyName = propertyPath.Substring(propertyPath.IndexOf(".") + 1);

            return propertyName;
        }

        public virtual Boolean IsSatisfy(Model model)
        {
            Boolean propertyIsValid = true;
            foreach (var validateGroup in this.propertyValidates)
            {
                foreach (var validateItem in validateGroup.Value)
                {
                    if (!validateItem.Validate.IsSatisfy(model))
                    {
                        propertyIsValid = false;
                        model.AddBrokenRule(validateItem.MessageKey, validateGroup.Key);
                        break;
                    }
                }
            }
            Boolean classIsValid = true;
            foreach (var validate in this.classValidates)
            {
                if (!validate.Validate.IsSatisfy(model))
                {
                    classIsValid = false;
                    model.AddBrokenRule(validate.MessageKey);
                    break;
                }
            }

            return propertyIsValid && classIsValid;
        }

        public virtual async Task<bool> IsSatisfyAsync(Model model)
        {
            var validateList = this.propertyValidates.Select(t =>
            {
                return new Task<ErrorKeys>(() =>
                {
                    foreach (var validateItem in t.Value)
                    {
                        if (!validateItem.Validate.IsSatisfy(model))
                        {
                            return new ErrorKeys() { MessageKey = validateItem.MessageKey, GroupKey = t.Key };
                        }
                    }
                    return null;
                });
            }).Concat(this.classValidates.Select(ct =>
            {
                return new Task<ErrorKeys>(() =>
                {
                    if (!ct.Validate.IsSatisfy(model))
                    {
                        return new ErrorKeys() { MessageKey = ct.MessageKey };
                    }
                    return null;
                });
            }));

            var taskList = validateList.Select(m => { m.Start(); return m; });
            var resultList = await Task.WhenAll(taskList);

            return resultList.Count(m =>
            {
                if (m != null)
                {
                    model.AddBrokenRule(m.MessageKey, m.GroupKey);
                }
                ; return m == null;
            }) == resultList.Count();
        }

        class ErrorKeys
        {
            public string MessageKey;
            public string GroupKey;
        }
    }
}
