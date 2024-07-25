using BusinessObjects;
using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTO.Request
{
    public class BookingDetailsDTO
    {
        public RoomDTO Room { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? ActualPrice { get; set; }

        // Calculate the actual price based on the room price per day and duration of stay
        public void CalculateActualPrice()
        {
            int days = (EndDate - StartDate).Days;
            ActualPrice = days * Room.RoomPricePerDay;
        }
    }
}
