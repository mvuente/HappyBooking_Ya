using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HappyBooking_Ya.Controllers
{
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

        [HttpPost("{event: Event}")]
        public IActionResult Create([FromBody] Event newEvent)
        {
            var createdEvent = _eventService.CreateEvent(newEvent);

            //return createdEvent? Ok(createdEvent.Id) : 
            return Ok(createdEvent.id);
        }

        [HttpPut("{id: int}, {updatedEvent: Event}")]
        public void Put(int id, [FromBody] Event updatedEvent)
        {
            
        }

        [HttpDelete("{id: int}")]
        public void Delete(int id)
        {
            
        }
    }
}

