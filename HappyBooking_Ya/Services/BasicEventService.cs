namespace HappyBooking_Ya.Services
{
    /// <summary>
    /// Реализация интерфейса сервиса приложения для случая общего события и хранения in memory
    /// </summary>
    public class BasicEventService : UtilEventService, IEventService
    {
        /// <summary>
        /// Коллекция событий
        /// </summary>
        private readonly IEventRepository eventRepository;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="_eventRepository">  Экземпляр репозитория </param>
        public BasicEventService(IEventRepository _eventRepository)
        {
            eventRepository = _eventRepository;
        }

        /// <summary>
        /// Реализация метода, выполняющего GET запрос
        /// </summary>
        /// <param name="id"> идентификатор события </param>
        /// <returns> Неизменяемый экземпляр события с заданным id </returns>
        public EventResponse GetEvent(Guid id)
        {
            var EventFound = eventRepository.GetEventById(id);

            if (EventFound == null)
            {
                throw new NotFoundException("событие", id);
            }

            return new EventResponse(
                EventFound.Id,
                EventFound.Title,
                EventFound.Description,
                EventFound.StartAt,
                EventFound.EndAt);
        }

        /// <summary>
        /// Реализация метода, выполняющего GET запрос
        /// </summary>
        /// <param name="title"> Параметр фильтра по названию события </param>
        /// <param name="from"> Параметр фильтра по дате начала периода </param>
        /// <param name="to"> Параметр фильтра по дате окончания периода </param>
        /// <param name="page"> Номер страницы возвращаемого массива </param>
        /// <param name="pageSize"> Размер страницы возвращаемого массива </param>
        /// <returns> Постраничный массив событий </returns>
        public PaginatedResult GetAllEvents(
            string? title = null,
            DateTime? from = null,
            DateTime? to = null,
            int page = 1,
            int pageSize = 10)
        {
            ValidatePageParameters(page, pageSize);
            
            var query = eventRepository.GetEvents();
            EventFilter filter = new EventFilter();
            query = filter.FilterByTitle(query, title);
            query = filter.FilterByStart(query, from);
            query = filter.FilterByEnd(query, to);

            var count = query.Count();
            var events = filter.PaginateEvents(query, page, pageSize).ToList();
            var response = events
                .Select(e => new EventResponse(e.Id, e.Title, e.Description, e.StartAt, e.EndAt))
                .ToList();

            return new PaginatedResult(count, response, page, events.Count());
        }

        /// <summary>
        /// Реализация метода, выполняющего DELETE запрос
        /// </summary>
        /// <param name="id"> идентификатор события </param>
        /// <returns> численный результат операции </returns>
        public int DeleteEvent(Guid id)
        {
            var deleteResult = eventRepository.DeleteEvent(id);

            if (deleteResult == 0)
            {
                throw new NotFoundException("событие", id);
            }

            return deleteResult;
        }

        /// <summary>
        /// Реализация метода, выполняющего POST запрос
        /// </summary>
        /// <param name="eventDTO"> экземпляр класса с параметрами события </param>
        /// <returns> экземпляр созданного класса события </returns>
        public Event CreateEvent(EventDTO eventDTO)
        {
            return eventRepository.CreateEvent(eventDTO);
        }

        /// <summary>
        /// Реализация метода, выполняющего PUT запрос
        /// </summary>
        /// <param name="id"> идентификатор события </param>
        /// <param name="eventDTO"> экземпляр класса с параметрами события </param>
        public void ReplaceEvent(Guid id, EventDTO eventDTO)
        {
            var eventToUpdate = eventRepository.GetEventById(id);

            if (eventToUpdate == null)
            {
                throw new NotFoundException("событие", id);
            }
            
            eventToUpdate.Title = eventDTO.Title;
            eventToUpdate.Description = eventDTO.Description;
            eventToUpdate.StartAt = eventDTO.StartAt;
            eventToUpdate.EndAt = eventDTO.EndAt;

            eventRepository.SaveChanges();
        }
    }
}
