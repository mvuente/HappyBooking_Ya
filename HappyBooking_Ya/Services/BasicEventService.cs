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

        public Event CreateEvent(string title, string description, DateTime startAt, DateTime endAt)
        {
            var newId = repoInMemory.Count() + 1;
            var newEvent = new Event(newId, title, description, startAt, endAt);
            repoInMemory.Add(newEvent);

            return newEvent; // проверка на NULL
        }

        public Event ReplaceEvent(int id, string title, string description, DateTime startAt, DateTime endAt)
        {
            var eventToUpdate = repoInMemory.Find(r => r.Id == id);

            if (eventToUpdate != null)
            {
                eventToUpdate.Title         = title;
                eventToUpdate.Description   = description;
                eventToUpdate.StartAt       = startAt;
                eventToUpdate.EndAt         = endAt;
            }
            
            return eventToUpdate; // проверка на NULL
        }
    }
}
