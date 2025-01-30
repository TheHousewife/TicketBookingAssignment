using Moq;

namespace TicketBookingCore.Tests
{
    public class TicketBookingRequestProcessorTests
    {


        public class TicketBookingRequestProcessorTests
        {
            private readonly Mock<ITicketBookingRepository> _ticketBookingRepositoryMock;
            private readonly TicketBookingRequestProcessor _processor;
            public TicketBookingRequestProcessorTests()
            {
                _ticketBookingRepositoryMock = new Mock<ITicketBookingRepository>();
                _processor = new
                TicketBookingRequestProcessor(_ticketBookingRepositoryMock.Object);
            }
        }


        private readonly TicketBookingRequestProcessor _processor;
        public TicketBookingRequestProcessorTests()
        {
            _processor = new TicketBookingRequestProcessor();
        }

        [Fact]
        public void ShouldReturnTicketBookningResultWithRequestValues()
        {
            // Arrange

            var request = new TicketBookingRequest
            {
                FirstName = "Mattis",
                LastName = "Ericsson Bergman",
                Email = "mattis@gmail.com"
            };

            // Act
            TicketBookingResponse response = _processor.Book(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(request.FirstName, response.FirstName);
            Assert.Equal(request.LastName, response.LastName);
            Assert.Equal(request.Email, response.Email);
        }
        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.Book(null));

            //Assert
            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void ShouldSaveToDatabase()
        {
            // Arrange
            var request = new TicketBookingRequest
            {
                FirstName = "Mattis",
                LastName = "Ericsson Bergman",
                Email = "mattis@gmail.com"
            };
            // Act
            TicketBookingResponse response = _processor.Book(request);


            // Assert


        }
    }
}