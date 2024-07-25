using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTO.Request
{
    public class BookingDTO
    {
        public DateTime? BookingDate { get; set; }
        public decimal? TotalPrice {  get; set; }
        public int CustomerId { get; set; }
        public byte? BookingStatus { get; set; }
        public List<BookingDetailsDTO> BookingDetails { get; set; } = new List<BookingDetailsDTO>();

        public void CalculateTotalPrice()
        {
            if (BookingDetails == null || BookingDetails.Count == 0)
            {
                TotalPrice = 0;
            }

            decimal? totalPrice = 0;

            foreach (var detail in BookingDetails)
            {
                detail.CalculateActualPrice();
                totalPrice += detail.ActualPrice;
            }

            TotalPrice = totalPrice;
        }
    }
}
