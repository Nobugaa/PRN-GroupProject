using BusinessObjects;
using DataAccessLayer;
using DataAccessLayer.DTO;
using Repositories.Interface;

namespace Repositories
{
    public class RoomRepository : IRoomRepository
    {
        public void AddRoom(RoomDTO room) =>  RoomDAO.AddRoom(room);

        public void DeleteRoom(int roomId) =>  RoomDAO.DeleteRoom(roomId);

        public List<RoomDTO> GetRooms(Func<RoomInformation, bool> predicate) => RoomDAO.GetRooms(predicate);

        public void UpdateRoom(RoomDTO room) =>  RoomDAO.UpdateRoom(room);

        public int CountRooms() => RoomDAO.CountRooms();
    }
}
