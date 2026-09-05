using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HappyBooking_Ya.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public void Get()
        {
            
        }

        [HttpGet("{id: int}")]
        public void Get(int id)
        {
            
        }

        [HttpPost("{event: Event}")] //проверить правильность пути
        public IActionResult Create([FromBody] EventDTO newEventDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdEvent = _eventService.CreateEvent(newEventDTO);

            return CreatedAtAction(nameof(Create), new { id = createdEvent.Id }, createdEvent);
        }

        [HttpPut("{id: int}, {updatedEventDTO: EventDTO}")]
        public IActionResult Put(int id, [FromBody] EventDTO updatedEventDTO)
        {
            
                return BadRequest(ModelState);
            


        }

        [HttpDelete("{id: int}")]
        public void Delete(int id)
        {
            
        }
    }
}

