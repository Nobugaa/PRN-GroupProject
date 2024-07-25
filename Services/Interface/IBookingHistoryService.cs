using BusinessObjects;
using DataAccessLayer.DTO;
using DataAccessObjects.DTO.Request;

namespace Services.Interface
{
    public interface IBookingHistoryService
    {
        BookingReservation? GetBookingById(int id);
        List<BookingHistoryDTO> GetBookingByCusId(int id);
        BookingReservation CreateBooking(BookingDTO booking);
        void UpdateBooking(BookingHistoryDTO booking);
        int CountBookings();
        decimal? CalcRevenue();
    }
}
