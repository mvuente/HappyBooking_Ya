namespace HappyBooking_Ya.Services
{
    /// <summary>
    /// Интерфейс сервиса приложения
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Объявление метода, выполняющего GET запрос
        /// </summary>
        /// <param name="id"> идентификатор события </param>
        /// <returns> Экземпляр класса события с заданным id </returns>
        public Event GetEvent(int id);

        /// <summary>
        /// Объявление метода, выполняющего GET запрос
        /// </summary>
        /// <returns> Коллекция экземпляров класса события  </returns>
        public List<Event> GetAllEvents();

        /// <summary>
        /// Объявление метода, выполняющего POST запрос
        /// </summary>
        /// <param name="eventDTO"> экземпляр класса с параметрами события </param>
        /// <returns> экземпляр созданного класса события </returns>
        public Event CreateEvent(EventDTO eventDTO);

        /// <summary>
        /// Объявление метода, выполняющего PUT запрос
        /// </summary>
        /// <param name="id"> идентификатор события </param>
        /// <param name="eventDTO"> экземпляр класса с параметрами события </param>
        /// <returns> обновленный экземпляр класса события </returns>
        public Event ReplaceEvent(int id, EventDTO eventDTO);

        /// <summary>
        /// Объявление метода, выполняющего DELETE запрос
        /// </summary>
        /// <param name="id"> идентификатор события </param>
        /// <returns> численный результат операции </returns>
        public int DeleteEvent(int id);
    }
}
