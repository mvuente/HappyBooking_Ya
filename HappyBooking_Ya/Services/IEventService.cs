namespace HappyBooking_Ya.Services
{
    public interface IEventService
    {
        public Event GetEvent(int id);
        public List<Event> GetAllEvents();
        public Event CreateEvent(EventDTO eventDTO);
        public Event ReplaceEvent(int id, EventDTO eventDTO);
        public int DeleteEvent(int id);
    }
}
