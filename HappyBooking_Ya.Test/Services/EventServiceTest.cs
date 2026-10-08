using HappyBooking_Ya.Data;
using HappyBooking_Ya.DTOs;
using HappyBooking_Ya.Exceptions;
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
        private EventDTO RequestUpdate;
        private readonly EventFilter Filter;
        private DateTime Start1;
        private DateTime Start2;
        private DateTime Start3;
        private DateTime End1;
        private DateTime End2;
        private DateTime End3;
        public EventServiceTest()
        {
            MockRepository = new Mock<IEventRepository>();
            EventService = new BasicEventService(MockRepository.Object);
            Filter = new EventFilter();
            Start1 = DateTime.UtcNow.AddDays(1);
            Start2 = DateTime.UtcNow.AddDays(2);
            Start3 = DateTime.UtcNow.AddDays(4);
            End1 = DateTime.UtcNow.AddDays(3);
            End2 = DateTime.UtcNow.AddDays(4);
            End3 = DateTime.UtcNow.AddDays(6); 

            Events = new List<Event>
                {
                    new (1,
                        "Start",
                        "Первое",
                        Start1,
                        End1),
                    new (2,
                        "Constant",
                        "Второе",
                        Start2,
                        End2),
                    new (3,
                        "Finish",
                        "Последнее",
                        Start3,
                        End3)
                };

            RequestUpdate = new EventDTO
            {
                Title = "updatedEvent",
                Description = "CreatedFromTest",
                StartAt = Start2,
                EndAt = End2
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
            MockRepository.Setup(method => method.GetEvents()).Returns(Events);

            var pagedResult = EventService.GetAllEvents();

            Assert.Equal(3, pagedResult.TotalEventsNumber);
            Assert.Equal("Start", pagedResult.Events.First().Title);
            Assert.Equal("Finish", pagedResult.Events.Last().Title);
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
            var expectedResult = 1;

            Assert.Equal(expectedResult, methodResult);
            MockRepository.Verify(repo => repo.DeleteEvent(1), Times.Once);
        }

        public void replaceEventTest()
        {
            Event foundEvent = new Event(
                2,
                "Constant",
                "Второе",
                Start2,
                End2);

            MockRepository.Setup(method => method.GetEventById(2)).Returns(foundEvent);
            EventService.ReplaceEvent(2, RequestUpdate);
  
            MockRepository.Verify(repo => repo.GetEventById(2), Times.Once);
            Assert.Equal("updatedEvent", foundEvent.Title);
            Assert.Equal("CreatedFromTest", foundEvent.Description);    
        }

        public void filterTitleEventTest()
        {
            var expectedResult = new List<string> { "Start", "Constant" };
            var notExpectedResult = "Finish";
            var stringExist = "ta";
            var stringDoesntExist = "gost";
            string? emptyTitle = null;

            var result = Filter.filterByTitle(Events, stringExist).ToList(); 

            Assert.All(expectedResult, title => Assert.Contains(result, r => r.Title == title));
            Assert.DoesNotContain(notExpectedResult, result.Select(Event => Event.Title));

            result = Filter.filterByTitle(Events, stringDoesntExist).ToList();
            Assert.Empty(result);

            result = Filter.filterByTitle(Events, emptyTitle).ToList();
            Assert.Equal(3, result.Count());
        }

        public void filterStartDateEventTest()
        {
            var notExpectedResult = new List<DateTime> { Start1, Start2 };
            var expectedResult = new List<DateTime>  { Start3 };
            var dateForCorrectRequest = DateTime.UtcNow.AddDays(3);
            var dateForUncorrectRequest = DateTime.UtcNow.AddDays(10);
            DateTime? emptyDate = null;

            var result = Filter.filterByStart(Events, dateForCorrectRequest).ToList();

            Assert.All(expectedResult, startAt => Assert.Contains(result, r => r.StartAt == startAt));
            Assert.All(notExpectedResult, startAt => Assert.DoesNotContain(result, r => r.StartAt == startAt));

            result = Filter.filterByStart(Events, dateForUncorrectRequest).ToList();
            Assert.Empty(result);

            result = Filter.filterByStart(Events, emptyDate).ToList();
            Assert.Equal(3, result.Count());
        }

        public void filterEndDateEventTest()
        {
            var expectedResult = new List<DateTime> { End1, End2 };
            var notExpectedResult = new List<DateTime> { End3 };
            var dateForCorrectRequest = DateTime.UtcNow.AddDays(5);
            var dateForUncorrectRequest = DateTime.UtcNow.AddDays(1);
            DateTime? emptyDate = null;

            var result = Filter.filterByEnd(Events, dateForCorrectRequest).ToList();

            Assert.All(expectedResult, endAt => Assert.Contains(result, r => r.EndAt == endAt));
            Assert.All(notExpectedResult, endAt => Assert.DoesNotContain(result, r => r.EndAt == endAt));

            result = Filter.filterByEnd(Events, dateForUncorrectRequest).ToList();
            Assert.Empty(result);

            result = Filter.filterByEnd(Events, emptyDate).ToList();
            Assert.Equal(3, result.Count());
        }

        public void pageEventTest()
        {
            var result = Filter.paginateEvents(Events, 1, 2);

            Assert.Equal(2, result.Count());
            Assert.Equal("Start", result.First().Title);
            Assert.Equal("Constant", result.Last().Title);

            result = Filter.paginateEvents(Events, 2, 2);
            Assert.Single(result);

            result = Filter.paginateEvents(Events, 2, 4);
            Assert.Empty(result);
        }

        public void getAllEventsFilteredTest()
        {
            MockRepository.Setup(method => method.GetEvents()).Returns(Events);

            var pagedResult = EventService.GetAllEvents("ta", null, DateTime.UtcNow.AddDays(3), 1, 1);

            Assert.Equal(1, pagedResult.TotalEventsNumber);
            Assert.Equal("Start", pagedResult.Events.First().Title);
        }

        public void incorrectIdExceptionTest()
        {
            var exception = Assert.Throws<NotFoundException>(() => EventService.GetEvent(5));

            Assert.Equal("событие с ID 5 не найден", exception.Message);
        }

        public void incorrectIdUpdateExceptionTest()
        {
            var exception = Assert.Throws<NotFoundException>(() => EventService.ReplaceEvent(5, RequestUpdate));

            Assert.Equal("событие с ID 5 не найден", exception.Message);
        }

        public void invalidPageParamsExceptionTest()
        {            
            var exception = Assert.Throws<FluentValidation.ValidationException>(() => EventService.GetAllEvents(null, null, null, 0, 10));
            exception = Assert.Throws<FluentValidation.ValidationException>(() => EventService.GetAllEvents(null, null, null, 1, -10));
        }
    }
}
