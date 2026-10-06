using HappyBooking_Ya.Data;
using HappyBooking_Ya.DTOs;
using HappyBooking_Ya.models;
using HappyBooking_Ya.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System.Net;

namespace HappyBooking_Ya.UnitTest.Services
{
    public class EventServiceTest
    {
        private List<Event> Events;
        private Mock<IEventRepository> MockRepository;
        private BasicEventService EventService;
        private DateTime Start1;
        private DateTime Start2;
        private DateTime End1;
        private DateTime End2;
        public EventServiceTest()
        {
            MockRepository = new Mock<IEventRepository>();
            EventService = new BasicEventService(MockRepository.Object);
            Start1 = DateTime.UtcNow.AddDays(1);
            Start2 = DateTime.UtcNow.AddDays(2);
            End1 = DateTime.UtcNow.AddDays(3);
            End2 = DateTime.UtcNow.AddDays(4);

            Events = new List<Event>
                {
                    new (1,
                        "Start",
                        "Первое",
                        Start1,
                        End1),
                    new (2,
                        "Cont",
                        "Второе",
                        Start2,
                        End2)
                };
        }
        public void getEventByIdTest()
        {
            MockRepository.Setup(method => method.GetEventById(1)).Returns(Events.First());

            var result = EventService.GetEvent(1);

            Assert.Equal(1, result.Id);
            Assert.Equal("Start", result.Title);
            Assert.Equal("Первое", result.Description);
            Assert.Equal(Start1, result.StartAt);
            Assert.Equal(End1, result.EndAt);
        }
        public void getAllEventsTest()
        {
            int count = 2;           

            MockRepository.Setup(method => method.GetEvents(null, null, null, 1, 10)).Returns((Events, count));

            var pagedResult = EventService.GetAllEvents();

            Assert.Equal(2, pagedResult.TotalEventsNumber);
            Assert.Equal("Start", pagedResult.Events.First().Title);
            Assert.Equal("Cont", pagedResult.Events.Last().Title);
        }

        public void createEventTest()
        {
            var startAt3 = DateTime.UtcNow.AddDays(2);
            var endAt3 = DateTime.UtcNow.AddDays(4);

            EventDTO request = new EventDTO {
                Title = "newEvent",
                Description = "CreatedFromTest",
                StartAt = startAt3,
                EndAt = endAt3 };

            Event createdEvent = new Event (
                3,
                "newEvent",
                "CreatedFromTest",
                startAt3,
                endAt3);
                
            MockRepository.Setup(method => method.CreateEvent(request)).Returns(createdEvent);

            var createdResult = EventService.CreateEvent(request);

            Assert.Equal(createdEvent, createdResult);
            MockRepository.Verify(repo => repo.CreateEvent(request), Times.Once);

        }

        public void deleteEventTest()
        {
            MockRepository.Setup(method => method.DeleteEvent(1)).Returns(1);

            var methodResult = EventService.DeleteEvent(1);

            Assert.Equal(methodResult, 1);
            MockRepository.Verify(repo => repo.DeleteEvent(1), Times.Once);
        }

        public void replaceEventTest()
        {
            EventDTO requestUpdate = new EventDTO
            {
                Title = "updatedEvent",
                Description = "CreatedFromTest",
                StartAt = Start2,
                EndAt = End2
            };

            Event foundEvent = new Event(
                2,
                "Cont",
                "Второе",
                Start2,
                End2);

            MockRepository.Setup(method => method.GetEventById(2)).Returns(foundEvent);
            EventService.ReplaceEvent(2, requestUpdate);
  
            MockRepository.Verify(repo => repo.GetEventById(2), Times.Once);
            Assert.Equal("updatedEvent", foundEvent.Title);
            Assert.Equal("CreatedFromTest", foundEvent.Description);    
        }
    }
}
