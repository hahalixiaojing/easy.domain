using System;
using System.Collections.Generic;
using System.Text;

namespace Easy.Domain.RepositoryFramework
{
    public interface IRepository<T,TKey>
    {
        T FindBy(TKey key);
        IList<T> FindAll();
        void Add(T item);
        void Update(T item);
        void Remove(T item);
        void RemoveAll();
    }

}
