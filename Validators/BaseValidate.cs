using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Easy.Domain.Base;

namespace Easy.Domain.Validators
{
    public abstract class BaseValidate<T, Value> : IValidate<T>
    {
        private String propertyName;
        private string[] propertyNames;
        public BaseValidate(String propertyName)
        {
            this.propertyName = propertyName;
            this.ParsePropertyName();
        }
        public BaseValidate(Expression<Func<T, Value>> expression)
        {
            this.propertyName = this.GetPropertyName(expression);
            this.ParsePropertyName();
        }
        private void ParsePropertyName()
        {
            propertyNames = this.propertyName.Split('.');
        }
        private String GetPropertyName(Expression<Func<T, Value>> expression)
        {
            String propertyPath = expression.Body.ToString();
            String propertyName = propertyPath.Substring(propertyPath.IndexOf(".") + 1);

            return propertyName;
        }
        protected String PropertyName
        {
            get
            {
                return this.propertyName;
            }
        }
        protected Value GetObjectAttrValue(T model)
        {
            Object value = this.GetObjectValue(model);

            if (value == null)
            {
                return default(Value);
            }
            return (Value)value;
        }
        private Object GetObjectValue(T model)
        {
            Object objectValue = model;
            foreach (String propertyName in this.propertyNames)
            {
                if (objectValue == null)
                {
                   return null;
                }
                objectValue = objectValue.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(objectValue, null);
            }
            return objectValue;
        }

        #region IValidate<T,TValue> 成员
        public abstract bool IsSatisfy(T model);
        #endregion
    }
}
