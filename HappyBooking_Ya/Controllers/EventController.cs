using HappyBooking_Ya.models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HappyBooking_Ya.Controllers
{
    public class EventController : ControllerBase
    {
        [HttpGet]
        public void Get()
        {
            
        }

        [HttpGet("{id: int}")]
        public void Get(int id)
        {
            
        }

        [HttpPost("{event: Event}")]
        public void Create([FromBody] Event newEvent)
        {
            
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

