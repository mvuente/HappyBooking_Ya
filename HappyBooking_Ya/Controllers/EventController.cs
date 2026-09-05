using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HappyBooking_Ya.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController(IEventService _eventService) : ControllerBase
    {
        private readonly IEventService? _eventService;

        [HttpGet]
        public ApiResult<List<Event>> Get()
        {
            return new ApiResult<List<Event>>
            {
                Data        = _eventService.GetAllEvents(),
                Success     = true,
                StatusCode  = HttpStatusCode.OK,
                Message     = "Получаем все события из коллекции" //а если список пуст?
            };
        }

        [HttpGet("{id: int}")]
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

        [HttpPost("{event: Event}")] //проверить правильность пути
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

        [HttpPut("{id: int}, {updatedEventDTO: EventDTO}")]
        public ApiBaseResult Put(int id, [FromBody] EventDTO updatedEventDTO)
        {
            if (!ModelState.IsValid || id < 0)
            {
                return new ApiResult
                {
                    Success     = false,
                    StatusCode  = HttpStatusCode.BadRequest,
                    Message = "Неверные данные; HTTP 400 Bad Request"
                };
            }

            var eventReplaced = _eventService.ReplaceEvent(id, updatedEventDTO);

            if (eventReplaced != null)
            {
                return new ApiResult
                {
                    Success = true,
                    StatusCode = HttpStatusCode.NoContent,
                    Message = "Меняем данные события по id и возвращаем HTTP 204 No Content"
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

        [HttpDelete("{id: int}")]
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

