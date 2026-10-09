namespace HappyBooking_Ya.Controllers
{
    /// <summary>
    /// Класс контроллер приложения
    /// </summary>
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
        /// <param name="title"> Параметр фильтра по названию события </param>
        /// <param name="from"> Параметр фильтра по дате начала периода </param>
        /// <param name="to"> Параметр фильтра по дате окончания периода </param>
        /// <param name="page"> Номер страницы возвращаемого массива </param>
        /// <param name="pageSize"> Размер страницы возвращаемого массива </param>
        /// <returns> Постраничный массив событий </returns>
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status200OK)]
        [Produces("application/json")]
        [HttpGet]
        public ApiResult<PaginatedResult> Get(
            [FromQuery] string? title = null,
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            return new ApiResult<PaginatedResult>
            {
                Data = _eventService.GetAllEvents(title, from, to, page, pageSize),
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Получаем события из коллекции в соответствии с параметрами запроса"
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
        [HttpGet("{id}")]
        public ActionResult<ApiResult<EventResponse>> Get(int id)
        {
            var eventFound = _eventService.GetEvent(id);
     
            return new OkObjectResult(new ApiResult<EventResponse>
            {
                Data = eventFound,
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Получаем событие по его id из коллекции"
            });      
        }

        /// <summary>
        /// Метод создает новое событие в коллекции
        /// </summary>
        /// <param name="newEventDTO"> Экземпляр класса с параметрами события с валидируемыми параметрами события </param>
        /// <returns> JSON струткура с деталями ответа </returns>
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status201Created)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<ApiBaseResult> Create([FromBody] EventDTO newEventDTO)
        {
            var createdEvent = _eventService.CreateEvent(newEventDTO);
            var createdEventLocation = Url.Action(nameof(Get), new { id = createdEvent.Id }) ??
                                        throw new InvalidOperationException("Не удалось сформировать адрес события");

            return new CreatedResult(createdEventLocation, new ApiResult
            {
                Success = true,
                StatusCode = HttpStatusCode.Created,
                Message = "Добавлено новое событие в коллекцию и возвращен HTTP 201 Created"
            });
        }

        /// <summary>
        /// Метод заменяет параметры события в коллекции данными, передаваемыми в запросе
        /// </summary>
        /// <param name="id"> идентификатор события </param>
        /// <param name="updatedEventDTO"> Экземпляр класса с параметрами события с валидируемыми параметрами события </param>
        /// <returns> JSON струткура с деталями ответа </returns>
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public ActionResult<ApiBaseResult> Put(int id, [FromBody] EventDTO updatedEventDTO)
        {
            _eventService.ReplaceEvent(id, updatedEventDTO);
      
            return new NoContentResult();      
        }

        /// <summary>
        /// Метод удаляет событие из коллекции по его id
        /// </summary>
        /// <param name="id"> идентификатор события </param>
        /// <returns> JSON струткура с деталями ответа </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status404NotFound)]
        public ActionResult<ApiBaseResult> Delete(int id)
        {    
                _eventService.DeleteEvent(id);
                return new NoContentResult();       
        }
    }
}

