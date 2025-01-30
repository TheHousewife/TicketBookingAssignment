
using System.Runtime.CompilerServices;

namespace TicketBookingCore
{
    public class TicketBookingRequestProcessor
    {
        public TicketBookingRequestProcessor()
        {
        }

        public TicketBookingRequestProcessor(ITicketBookingRepository
        ticketBookingRepository)
        {
            _ITicket = ticketBookingRepository;
        }

        private readonly ITicketBookingRepository _ITicket;


        public TicketBookingResponse Book(TicketBookingRequest request)
        {

            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            _ITicket.Save(new TicketBooking
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            });
            //refractor för att returnera en ny TicketBookingResponse
            return new TicketBookingResponse
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };
            
        }
    }
}