using BusinessObjects;
using DataAccessLayer.DTO;
using Repositories;
using Repositories.Interface;
using Services.Interface;

namespace Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repo;

        public RoomService()
        {
            _repo = new RoomRepository();
        }

        public void AddRoom(RoomDTO room) =>  _repo.AddRoom(room);

        public void DeleteRoom(int roomId) =>  _repo.DeleteRoom(roomId);

        public List<RoomDTO> GetRooms(Func<RoomInformation, bool> predicate) => _repo.GetRooms(predicate);

        public void UpdateRoom(RoomDTO room) =>  _repo.UpdateRoom(room);

        public int CountRooms() => _repo.CountRooms();
    }
}
