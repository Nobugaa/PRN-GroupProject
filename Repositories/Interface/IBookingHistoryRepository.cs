using BusinessObjects;
using DataAccessLayer.DTO;
using DataAccessObjects.DTO.Request;

namespace Repositories.Interface
{
    public interface IBookingHistoryRepository
    {
        BookingReservation? GetBookingById(int id);
        List<BookingHistoryDTO> GetBookingByCusId(int id);
        BookingReservation CreateBooking(BookingDTO booking);
        void UpdateBooking(BookingHistoryDTO booking);
        void UpdateBooking(BookingReservation booking);
        int CountBookings();
        decimal? CalcRevenue();
    }
}
