using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingCore
{
    public class TicketBookingMapper
    {
        public static TicketBooking MapToTicketBooking(TicketBookingRequest request)
        {
            return new TicketBooking
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };
        }
    }

}
