namespace DataAccessLayer.DTO;

public class BookingHistoryDTO
{
    public int BookingReservationId { get; set; }

    public int RoomId { get; set; }

    public string RoomNumber { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal? ActualPrice { get; set; }

    public DateOnly? BookingDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public int CustomerId { get; set; }

    public byte? BookingStatus { get; set; }
}
