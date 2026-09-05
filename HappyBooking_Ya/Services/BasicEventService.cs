namespace HappyBooking_Ya.Services
{
    public class BasicEventService : IEventService
    {
        private List<Event> repoInMemory = new List<Event>();

        public Event GetEvent(int id)
        {
            return repoInMemory.Find(r => r.Id == id); // проверка на NULL
        }

        public List<Event> GetAllEvents()
        {
            return repoInMemory.Select(e => e.CloneEvent(e)).ToList();
        }

        public void DeleteEvent(int id) //void, тк возможно повтороный запрос с тем же id
        {
            repoInMemory.RemoveAll(r => r.Id == id);
        }

        public Event CreateEvent(EventDTO eventDTO)
        {
            var newId = repoInMemory.Count() + 1; //переписать логику получения id
            var newEvent = new Event(newId, eventDTO.Title, eventDTO.Description, eventDTO.StartAt, eventDTO.EndAt);
            repoInMemory.Add(newEvent);

            return newEvent; 
        }

        public Event ReplaceEvent(int id, EventDTO eventDTO)
        {
            var eventToUpdate = repoInMemory.Find(r => r.Id == id);

            if (eventToUpdate != null)
            {
                eventToUpdate.Title         = eventDTO.Title;
                eventToUpdate.Description   = eventDTO.Description;
                eventToUpdate.StartAt       = eventDTO.StartAt;
                eventToUpdate.EndAt         = eventDTO.EndAt;
            }
            
            return eventToUpdate; // проверка на NULL
        }
    }
}
