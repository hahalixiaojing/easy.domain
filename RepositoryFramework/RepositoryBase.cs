using System;
using System.Collections.Generic;
using System.Text;
using Easy.Domain.Base;



namespace Easy.Domain.RepositoryFramework
{
    public abstract class RepositoryBase<T, TKey> : IRepository<T, TKey>, IUnitOfWorkRepository where T : IAggregateRoot
    {
        public RepositoryBase() { }

        #region IRepository<T> 成员
        public abstract T FindBy(TKey key);
        public abstract IList<T> FindAll();
        public abstract void RemoveAll();
        public void Add(T item)
        {
            this.PersistNewItem((T)item);
        }

        public void Update(T item)
        {
            this.PersistUpdatedItem((T)item);
        }

        public void Remove(T item)
        {
            this.PersistDeletedItem((T)item);
        }
        #endregion

        #region IUnitOfWorkRepository 成员
        public virtual void PersistNewItem(IAggregateRoot item)
        {
            this.PersistNewItem((T)item);
        }
        public virtual void PersistUpdatedItem(IAggregateRoot item)
        {
            this.PersistUpdatedItem((T)item);
        }
        public virtual void PersistDeletedItem(IAggregateRoot item)
        {
            this.PersistDeletedItem((T)item);
        }
        #endregion

        protected abstract void PersistNewItem(T item);
        protected abstract void PersistUpdatedItem(T item);
        protected abstract void PersistDeletedItem(T item);
    }
}
