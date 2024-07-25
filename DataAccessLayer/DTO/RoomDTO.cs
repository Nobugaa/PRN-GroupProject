namespace DataAccessLayer.DTO;

public class RoomDTO
{
    public int RoomId { get; set; }

    public string? RoomNumber { get; set; }

    public string? RoomDetailDescription { get; set; }

    public int? RoomMaxCapacity { get; set; }

    public byte? RoomStatus { get; set; }

    public decimal? RoomPricePerDay { get; set; }

    public string? RoomType { get; set; }
}
