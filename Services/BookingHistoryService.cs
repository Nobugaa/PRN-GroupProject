using BusinessObjects;
using DataAccessLayer.DTO;
using DataAccessObjects.DTO.Request;
using Repositories;
using Repositories.Interface;
using Services.Interface;

namespace Services
{
    public class BookingHistoryService : IBookingHistoryService
    {
        private readonly IBookingHistoryRepository _repo;

        public BookingHistoryService()
        {
            _repo = new BookingHistoryRepository();
        }

        public BookingReservation? GetBookingById(int id) =>  _repo.GetBookingById(id);

        public List<BookingHistoryDTO> GetBookingByCusId(int id) =>  _repo.GetBookingByCusId(id);

        public BookingReservation CreateBooking(BookingDTO booking) => _repo.CreateBooking(booking);

        public void UpdateBooking(BookingHistoryDTO booking) =>  _repo.UpdateBooking(booking);

        public int CountBookings() => _repo.CountBookings();

        public decimal? CalcRevenue() => _repo.CalcRevenue();
    }
}
