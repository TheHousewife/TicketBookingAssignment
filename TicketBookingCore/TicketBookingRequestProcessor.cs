using TicketBookingCore;

public class TicketBookingRequestProcessor
{
    private readonly ITicketBookingRepository _ticketBookingRepository;

    public TicketBookingRequestProcessor(ITicketBookingRepository ticketBookingRepository)
    {
        _ticketBookingRepository = ticketBookingRepository;
    }

    public TicketBookingResponse Book(TicketBookingRequest request)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var ticketBooking = TicketBookingMapper.MapToTicketBooking(request);

        _ticketBookingRepository.Save(ticketBooking);

        return new TicketBookingResponse
        {
            FirstName = ticketBooking.FirstName,
            LastName = ticketBooking.LastName,
            Email = ticketBooking.Email
        };
    }
}
