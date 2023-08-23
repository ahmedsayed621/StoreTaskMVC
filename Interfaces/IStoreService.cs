using StoreTaskMVC.Models;

namespace StoreTaskMVC.Interfaces
{
    public interface IStoreService
    {
        IEnumerable<Store> Get();
        Store GetById(int id);
        Store Create(Store obj);
        Store Update(Store obj);
        Store Delete(Store obj);

    }
}
