using BusinessObjects;
using DataAccessLayer.DTO;

namespace Repositories.Interface
{
    public interface IRoomRepository
    {
        List<RoomDTO> GetRooms(Func<RoomInformation, bool> predicate);
        void AddRoom(RoomDTO room);
        void UpdateRoom(RoomDTO room);
        void DeleteRoom(int roomId);
        int CountRooms();
    }
}
