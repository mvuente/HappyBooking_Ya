namespace HappyBooking_Ya.Services
{
    public interface IEventService
    {
        public Event GetEvent(int id);
        public List<Event> GetAllEvents();
        public Event CreateEvent(string title, string description, DateTime startAt, DateTime endAt);
        public Event ReplaceEvent(int id, string title, string description, DateTime startAt, DateTime endAt);
        public void DeleteEvent(int id);
    }
}
