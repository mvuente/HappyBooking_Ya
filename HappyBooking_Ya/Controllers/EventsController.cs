namespace HappyBooking_Ya.Controllers
{
    /// <summary>
    /// Класс контроллер приложения
    /// </summary>
    /// <param name="_eventService"> Экземпляр класса сервиса приложения </param>
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        /// <summary>
        /// Экземпляр класса сервиса приложения
        /// </summary>
        private readonly IEventService? _eventService;

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        /// <param name="eventService"> Экземпляр класса сервиса приложения </param>
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Метод, возвращающий все события, имеющиеся в коллекции
        /// </summary>
        /// <returns> Коллекция событий </returns>
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status200OK)]
        [Produces("application/json")]
        [HttpGet]
        public ApiResult<List<Event>> Get()
        {
            return new ApiResult<List<Event>>
            {
                Data        = _eventService.GetAllEvents(),
                Success     = true,
                StatusCode  = HttpStatusCode.OK,
                Message     = "Получаем все события из коллекции" 
            };
        }

        /// <summary>
        /// Метод, возвращающий конкретное событие по его id
        /// </summary>
        /// <param name="id"> Идентификатор события </param>
        /// <returns> JSON структура с деталями ответа </returns>
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        //[HttpGet("{id: int}")]
        [HttpGet("{id}")] //test
        public ApiBaseResult Get(int id)
        {
            try 
            {
                return new ApiResult<Event>
                {
                    Data        = _eventService.GetEvent(id),
                    Success     = true,
                    StatusCode  = HttpStatusCode.OK,
                    Message     = "Получаем событие по его id из коллекции"
                };
            }
            catch(ArgumentOutOfRangeException ex)
            {
                return new ApiResult
                {
                    Success     = false,
                    StatusCode  = HttpStatusCode.NotFound,
                    Message     = $"Не удалось найти событие по id или id некорректный: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Метод создает новое событие в коллекции
        /// </summary>
        /// <param name="newEventDTO"> Экземпляр класса с параметрами события с валидируемыми параметрами события </param>
        /// <returns> JSON струткура с деталями ответа </returns>
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status201Created)]
        [Consumes("application/json")]
        //[HttpPost("{event: Event}")] //test
        [HttpPost("{event}")]
        public ApiBaseResult Create([FromBody] EventDTO newEventDTO)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResult
                {
                    Success     = false,
                    StatusCode  = HttpStatusCode.BadRequest,
                    Message     = "Некорректные параметры события"
                };
            }

            var createdEvent = _eventService.CreateEvent(newEventDTO);

            return new ApiResult
            {
                Success     = true,
                StatusCode  = HttpStatusCode.Created,
                Message     = "Добавлено новое событие в коллекцию и возвращен HTTP 201 Created"
            };
        }

        /// <summary>
        /// Метод заменяет параметры событяи в коллекции данными, передаваемыми в запросе
        /// </summary>
        /// <param name="id"> идентификатор события </param>
        /// <param name="updatedEventDTO"> Экземпляр класса с параметрами события с валидируемыми параметрами события </param>
        /// <returns> JSON струткура с деталями ответа </returns>
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status204NoContent)]
        //[HttpPut("{id: int}, {updatedEventDTO: EventDTO}")]
        [HttpPut("{id}, {updatedEventDTO}")] //test
        public ApiBaseResult Put(int id, [FromBody] EventDTO updatedEventDTO)
        {
            if (!ModelState.IsValid || id < 0)
            {
                return new ApiResult
                {
                    Success     = false,
                    StatusCode  = HttpStatusCode.BadRequest,
                    Message     = "Неверные данные; HTTP 400 Bad Request"
                };
            }

            var eventReplaced = _eventService.ReplaceEvent(id, updatedEventDTO);

            if (eventReplaced != null)
            {
                return new ApiResult
                {
                    Success     = true,
                    StatusCode  = HttpStatusCode.NoContent,
                    Message     = "Меняем данные события по id и возвращаем HTTP 204 No Content"
                };
            }
            else
            {
                return new ApiResult
                {
                    Success     = false,
                    StatusCode  = HttpStatusCode.NotFound,
                    Message     = "id некорректный"
                };
            }
        }

        /// <summary>
        /// Метод удаляет событие из коллекции по его id
        /// </summary>
        /// <param name="id"> идентификатор события </param>
        /// <returns> JSON струткура с деталями ответа </returns>
        //[HttpDelete("{id: int}")]
        [HttpDelete("{id}")] //test
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status404NotFound)]
        public ApiBaseResult Delete(int id)
        {
            try
            {
                _eventService.DeleteEvent(id);
                
                return new ApiResult
                {
                    Success     = true,
                    StatusCode  = HttpStatusCode.NoContent,
                    Message     = "Удалено событие с id и возвращен HTTP 204 No Content"
                };            
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return new ApiResult
                {
                    Success     = false,
                    StatusCode  = HttpStatusCode.NotFound,
                    Message     = $"id некорректный: {ex.Message}"
                };             
            }
        }
    }
}

