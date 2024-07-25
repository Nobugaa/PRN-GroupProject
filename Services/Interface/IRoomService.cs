using BusinessObjects;
using DataAccessLayer.DTO;

namespace Services.Interface
{
    public interface IRoomService
    {
        List<RoomDTO> GetRooms(Func<RoomInformation, bool> predicate);
        void AddRoom(RoomDTO room);
        void UpdateRoom(RoomDTO room);
        void DeleteRoom(int roomId);
        int CountRooms();
    }
}
