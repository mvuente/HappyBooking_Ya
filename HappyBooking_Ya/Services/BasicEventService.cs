namespace HappyBooking_Ya.Services
{
    public class BasicEventService : IEventService
    {
        private List<Event> repoInMemory = new List<Event>();
        private List<int> freeIndexes = new List<int>();

        private int getFreeIndex() //содержит индексы коллекции событий, увеличенные на 1 
        {
            int freeIndex = -1;

            if (freeIndexes.Count() != 0)
            {
                freeIndex = freeIndexes[0];
                freeIndexes.RemoveAt(0);
            }

            return freeIndex;
        }

        public Event GetEvent(int id)
        {
            var EventFound = repoInMemory.Find(r => r.Id == id);

            return EventFound.CloneEvent(EventFound); // не валидирую, так как в контроллере есть обработка exception
        }

        public List<Event> GetAllEvents()
        {
            return repoInMemory.Select(e => e.CloneEvent(e)).ToList();
        }

        public int DeleteEvent(int id) //void, тк возможно повтороный запрос с тем же id
        {
            var removeResult = repoInMemory.RemoveAll(r => r.Id == id);

            if (removeResult > 0)
            {
                freeIndexes.Add(id);
            }

            return removeResult;
        }

        public Event CreateEvent(EventDTO eventDTO)
        {
            var localIndex  = getFreeIndex();
            var newId       = localIndex < 0 ? repoInMemory.Count() + 1 : localIndex;
            var newEvent    = new Event(newId, eventDTO.Title, eventDTO.Description, eventDTO.StartAt, eventDTO.EndAt);
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
            
            return eventToUpdate; 
        }
    }
}
