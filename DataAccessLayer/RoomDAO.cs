using BusinessObjects;
using DataAccessLayer.DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class RoomDAO
{
    /// <summary>
    /// lấy danh sách phòng khách sạn
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static List<RoomDTO> GetRooms(Func<RoomInformation, bool> predicate)
    {
        using var db = new FuminiHotelManagementContext();
        return db.RoomInformations
                        .Include(r => r.RoomType)
                        .Where(predicate)
                        .Select(r => new RoomDTO
                        {
                            RoomId = r.RoomId,
                            RoomNumber = r.RoomNumber,
                            RoomDetailDescription = r.RoomDetailDescription,
                            RoomMaxCapacity = r.RoomMaxCapacity,
                            RoomStatus = r.RoomStatus,
                            RoomPricePerDay = r.RoomPricePerDay,
                            RoomType = r.RoomType.RoomTypeName,
                        })
                        .ToList();
    }
    /// <summary>
    /// Lấy số lượng phòng khách sạn
    /// </summary>
    /// <returns></returns>
    public static int CountRooms()
    {
        using var db = new FuminiHotelManagementContext();
        return db.RoomInformations
             .Where(r => r.RoomStatus == 1)
             .Count();
    }
    /// <summary>
    /// Thêm phòng khách sạn
    /// </summary>
    /// <param name="room"></param>
    public static void AddRoom(RoomDTO room)
    {
        using var db = new FuminiHotelManagementContext();
        // tạo phòng mới
        var newRoom = new RoomInformation
        {
            RoomNumber = room.RoomNumber,
            RoomDetailDescription = room.RoomDetailDescription,
            RoomMaxCapacity = room.RoomMaxCapacity,
            RoomStatus = room.RoomStatus,
            RoomPricePerDay = room.RoomPricePerDay,
            RoomType = db.RoomTypes.FirstOrDefault(rt => rt.RoomTypeName == room.RoomType)
        };
        //thêm phòng vào dbset
        db.RoomInformations.Add(newRoom);
        // thêm phòng vào database
        db.SaveChanges();
    }
    /// <summary>
    /// Cập nhật phòng khách sạn
    /// </summary>
    /// <param name="room"></param>
    public static void UpdateRoom(RoomDTO room)
    {
        using var db = new FuminiHotelManagementContext();
        // lấy phòng cần cập nhật
        var existingRoom = db.RoomInformations.Find(room.RoomId);
        if (existingRoom != null)
        {
            // gán lại những thuộc tính thay đổi
            existingRoom.RoomNumber = room.RoomNumber;
            existingRoom.RoomDetailDescription = room.RoomDetailDescription;
            existingRoom.RoomMaxCapacity = room.RoomMaxCapacity;
            existingRoom.RoomStatus = room.RoomStatus;
            existingRoom.RoomPricePerDay = room.RoomPricePerDay;
            existingRoom.RoomType = db.RoomTypes.FirstOrDefault(rt => rt.RoomTypeName == room.RoomType);
            // cập nhật phòng theo id
            db.RoomInformations.Update(existingRoom);
            // lưu thay đổi vào database
            db.SaveChanges();
        }
    }
    /// <summary>
    /// Xóa phòng khách sạn
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    public static void DeleteRoom(int roomId)
    {
        using var db = new FuminiHotelManagementContext();
        // lấy ra phòng cần xóa theo id
        var room = db.RoomInformations.Find(roomId);
        if (room != null)
        {
            // thực hiện xóa phòng
            db.RoomInformations.Remove(room);
            // thực hiện cập nhật database
            db.SaveChanges();
        }
    }
}
