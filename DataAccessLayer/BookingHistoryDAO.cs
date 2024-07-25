using BusinessObjects;
using DataAccessLayer.DTO;
using DataAccessObjects.DTO.Request;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class BookingHistoryDAO
{
    /// <summary>
    /// Lấy lịch đặt phòng theo id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static BookingReservation? GetBookingById(int id)
    {
        using var db = new FuminiHotelManagementContext();
        return db.BookingReservations.FirstOrDefault(b => b.Equals(id));
    }
    /// <summary>
    /// Lấy lịch đặt phòng theo id khách hàng
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static List<BookingHistoryDTO> GetBookingByCusId(int id)
    {
        using var db = new FuminiHotelManagementContext();
        return db.BookingDetails
            .Include(bd => bd.BookingReservation)
                .ThenInclude(br => br.Customer)
            .Include(bd => bd.Room)
            .Where(bd => bd.BookingReservation.CustomerId == id
            && bd.BookingReservation.BookingStatus == 1)
            .Select(bd => new BookingHistoryDTO
            {
                BookingReservationId = bd.BookingReservationId,
                RoomId = bd.RoomId,
                RoomNumber = bd.Room.RoomNumber,
                StartDate = bd.StartDate,
                EndDate = bd.EndDate,
                ActualPrice = bd.ActualPrice,
                BookingDate = bd.BookingReservation.BookingDate,
                TotalPrice = bd.BookingReservation.TotalPrice,
                CustomerId = bd.BookingReservation.CustomerId,
                BookingStatus = bd.BookingReservation.BookingStatus
            })
            .ToList();
    }
    /// <summary>
    /// Lấy số lượng phòng được đặt
    /// </summary>
    /// <returns></returns>
    public static int CountBookings()
    {
        using var db = new FuminiHotelManagementContext();
        return db.BookingReservations
            .Where(b => b.BookingStatus == 1)
            .Count();
    }
    /// <summary>
    /// Lấy tổng doanh thu 
    /// </summary>
    /// <returns></returns>
    public static decimal? CalcRevenue()
    {
        using var db = new FuminiHotelManagementContext();
        return db.BookingReservations
            .Where(b => b.BookingStatus == 1)
            .Sum(b => b.TotalPrice);
    }
    /// <summary>
    /// Tạo lịch đặt phòng
    /// </summary>
    /// <param name="bookingDto"></param>
    /// <returns></returns>
    public static BookingReservation CreateBooking(BookingDTO bookingDto)
    {
        using var db = new FuminiHotelManagementContext();
        // cập nhật thông tin đặt phòng
        var bookingReservation = new BookingReservation
        {
            BookingReservationId = db.BookingReservations.Count() + 1,
            BookingDate = DateOnly.FromDateTime((DateTime)bookingDto.BookingDate),
            CustomerId = bookingDto.CustomerId,
            BookingStatus = bookingDto.BookingStatus,
            BookingDetails = new List<BookingDetail>(),
            TotalPrice = bookingDto.TotalPrice,
        };

        var booking = db.BookingReservations.Add(bookingReservation);

        db.SaveChanges();
        // cập nhật thông tin chi tiết
        foreach (var detailDto in bookingDto.BookingDetails)
        {
            var room = db.RoomInformations.Find(detailDto.Room.RoomId);

            room.RoomStatus = 0;
            db.SaveChanges();

            var detail = new BookingDetail
            {
                BookingReservationId = booking.Entity.BookingReservationId,
                RoomId = detailDto.Room.RoomId,
                StartDate = DateOnly.FromDateTime(detailDto.StartDate),
                EndDate = DateOnly.FromDateTime(detailDto.EndDate),
                ActualPrice = detailDto.ActualPrice,
                Room = room,
            };

            booking.Entity.BookingDetails.Add(detail);
        }

        db.BookingReservations.Update(booking.Entity);

        db.SaveChanges();

        return bookingReservation;
    }
    /// <summary>
    /// Lấy phòng theo id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static BookingDTO? GetBookingDTOById(int id)
    {
        using var db = new FuminiHotelManagementContext();
        return db.BookingReservations
            .Where(b => b.BookingReservationId == id)
            .Include(b => b.BookingDetails)
                .ThenInclude(bd => bd.Room)
            .Select(b => new BookingDTO
            {
            }).FirstOrDefault();
    }
    /// <summary>
    /// Cập nhật lịch đặt phòng
    /// </summary>
    /// <param name="bookingDTO"></param>
    /// <returns></returns>
    public static void UpdateBooking(BookingHistoryDTO bookingDTO)
    {
        using var db = new FuminiHotelManagementContext();
        var bookingReservation = db.BookingReservations
            .Where(b => bookingDTO.BookingReservationId == b.BookingReservationId).FirstOrDefault();
        bookingReservation.BookingStatus = 0;
        db.BookingReservations.Update(bookingReservation);
        db.SaveChanges();
    }
    /// <summary>
    /// Cập nhật lịch đặt phòng
    /// </summary>
    /// <param name="booking"></param>
    /// <returns></returns>
    public static void UpdateBooking(BookingReservation booking)
    {
        using var db = new FuminiHotelManagementContext();
        booking.BookingStatus = 0;
        db.BookingReservations.Update(booking);
        db.SaveChanges();
    }
}
