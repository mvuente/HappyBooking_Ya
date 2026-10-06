namespace HappyBooking_Ya.Data
{
    public interface IEventRepository
    {
        public Event GetEventById(int id);

        public (List<Event>, int) GetEvents(
            string? title,
            DateTime? from,
            DateTime? to,
            int skip,
            int take);

        public Event CreateEvent(EventDTO eventDTO);

        public void SaveChanges();

        /// <summary>
        /// Объявление метода, удаляющего запись
        /// </summary>
        /// <param name="id"> идентификатор события </param>
        /// <returns> численный результат операции </returns>
        public int DeleteEvent(int id);
    }
}
