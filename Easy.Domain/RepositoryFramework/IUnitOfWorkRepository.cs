using Easy.Domain.Base;
namespace Easy.Domain.RepositoryFramework
{
    public interface IUnitOfWorkRepository
    {
        void PersistNewItem(IAggregateRoot item);
        void PersistUpdatedItem(IAggregateRoot item);
        void PersistDeletedItem(IAggregateRoot item);
    }
}
