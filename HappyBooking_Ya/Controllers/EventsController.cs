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
        /// <returns> Коллекция событий </returns>
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status200OK)]
        [Produces("application/json")]
        [HttpGet]
        public ApiResult<List<Event>> Get()
        {
            return new ApiResult<List<Event>>
            {
                Data = _eventService.GetAllEvents(),
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Получаем все события из коллекции"
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
        public ActionResult<ApiResult<Event>> Get(int id)
        {
            var eventFound = _eventService.GetEvent(id);

            if (eventFound != null)
            {
                return new OkObjectResult(new ApiResult<Event>
                {
                    Data = eventFound,
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "Получаем событие по его id из коллекции"
                });
            }
            else
            {
                return new BadRequestObjectResult(new ApiResult
                {
                    Success = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Не удалось найти событие по id или id некорректный"
                });
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
            var eventReplaced = _eventService.ReplaceEvent(id, updatedEventDTO);

            if (eventReplaced != null)
            {
                return new NoContentResult();
            }
            else
            {
                return new NotFoundObjectResult(new ApiResult
                {
                    Success = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "id некорректный"
                });
            }
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

