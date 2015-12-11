using System;
namespace Easy.Domain.Base
{
    [Serializable]
    public abstract class EntityBase<T> : BrokenRuleObject, IEntity<T>
    {
        private T _id;

        public EntityBase()
            : this(default(T))
        {
        }
        public EntityBase(T id)
        {
            _id = id;
        }
        public virtual T Id
        {
            get
            {
                return _id;
            }
            protected set
            {
                _id = value;
            }
        }
    }
}
