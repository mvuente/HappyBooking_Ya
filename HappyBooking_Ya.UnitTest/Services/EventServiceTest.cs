using Moq;
using System.Net;
using HappyBooking_Ya.Services;
using HappyBooking_Ya.Data;
using HappyBooking_Ya.models;

namespace HappyBooking_Ya.UnitTest.Services
{
    [Fact]
    public class EventServiceTest
    {
        public void getAllEventsTest()
        {
            int count = 1;
            var mockRepository = new Mock<IEventRepository>();
            var eventService = new BasicEventService(mockRepository.Object);
            var events = new List<Event>
                {
                    new (1,
                        "Start",
                        "Первое",
                        DateTime.UtcNow.AddDays(1),
                        DateTime.UtcNow.AddDays(3)),
                    new (2,
                        "Cont",
                        "Второе",
                        DateTime.UtcNow.AddDays(2),
                        DateTime.UtcNow.AddDays(4))
                };

            mockRepository.Setup(method => method.GetEvents(null, null, null, 1, 10)).Returns((events, count));

            var result = eventService.GetAllEvents();

            Assert.Equal(2, result.Count);
            Assert.Equal("Arbat", result.First().Street);
            Assert.Equal("Tverskaya", result.Last().Street);
        }
    }
}
