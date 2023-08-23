using StoreTaskMVC.Models;

namespace StoreTaskMVC.Interfaces
{
    public interface ISpaceService
    {
        IEnumerable<Space> Get();
        Space GetById(int id);
        Space Update(Space obj);
        Space Delete(Space obj);
        Space Split(int id, int numberOfSplits);
        Space Merge(int spaceId1,int spaceId2);
    }
}
