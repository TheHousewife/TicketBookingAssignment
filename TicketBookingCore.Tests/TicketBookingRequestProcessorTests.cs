using Moq;
using System;
using Xunit;

namespace TicketBookingCore.Tests
{
    public class TicketBookingRequestProcessorTests
    {
        private readonly Mock<ITicketBookingRepository> _ticketBookingRepositoryMock;
        private readonly TicketBookingRequestProcessor _processor;

        public TicketBookingRequestProcessorTests()
        {
            _ticketBookingRepositoryMock = new Mock<ITicketBookingRepository>();
            _processor = new TicketBookingRequestProcessor(_ticketBookingRepositoryMock.Object);
        }

        private TicketBookingRequest GetSampleRequest(string firstName = "Mattis", string lastName = "Ericsson Bergman", string email = "mattis@gmail.com")
        {
            return new TicketBookingRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
        }

        private void AssertTicketBookingResponse(TicketBookingResponse response, TicketBookingRequest request)
        {
            Assert.NotNull(response);
            Assert.Equal(request.FirstName, response.FirstName);
            Assert.Equal(request.LastName, response.LastName);
            Assert.Equal(request.Email, response.Email);
        }

        [Fact]
        public void ShouldReturnTicketBookingResultWithRequestValues()
        {
            // Arrange
            var request = GetSampleRequest();

            // Act
            TicketBookingResponse response = _processor.Book(request);

            // Assert
            AssertTicketBookingResponse(response, request);
        }

        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.Book(null));
            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void ShouldSaveToDatabase()
        {
            // Arrange
            TicketBooking savedTicketBooking = null;

            _ticketBookingRepositoryMock.Setup(x => x.Save(It.IsAny<TicketBooking>()))
                .Callback<TicketBooking>((ticketBooking) =>
                {
                    savedTicketBooking = ticketBooking;
                });

            var newRequest = GetSampleRequest("Milena", "Avramovic", "milenaavramovic@gmail.com");

            // Act
            TicketBookingResponse response = _processor.Book(newRequest);

            // Assert
            _ticketBookingRepositoryMock.Verify(x => x.Save(It.IsAny<TicketBooking>()), Times.Once);

            Assert.NotNull(savedTicketBooking);

            var expectedResponse = new TicketBookingResponse
            {
                FirstName = savedTicketBooking.FirstName,
                LastName = savedTicketBooking.LastName,
                Email = savedTicketBooking.Email
            };

            AssertTicketBookingResponse(expectedResponse, newRequest);
        }
    }
}
