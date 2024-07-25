using BusinessObjects;
using DataAccessLayer;
using DataAccessLayer.DTO;
using DataAccessObjects.DTO.Request;
using Repositories.Interface;

namespace Repositories
{
    public class BookingHistoryRepository : IBookingHistoryRepository
    {
        public  BookingReservation? GetBookingById(int id) =>  BookingHistoryDAO.GetBookingById(id);

        public List<BookingHistoryDTO> GetBookingByCusId(int id) =>  BookingHistoryDAO.GetBookingByCusId(id);

        public BookingReservation CreateBooking(BookingDTO booking) => BookingHistoryDAO.CreateBooking(booking);

        public void  UpdateBooking(BookingHistoryDTO booking) =>  BookingHistoryDAO.UpdateBooking(booking);

        public void UpdateBooking(BookingReservation booking) =>  BookingHistoryDAO.UpdateBooking(booking);

        public int CountBookings() => BookingHistoryDAO.CountBookings();

        public decimal? CalcRevenue() => BookingHistoryDAO.CalcRevenue();
    }
}
