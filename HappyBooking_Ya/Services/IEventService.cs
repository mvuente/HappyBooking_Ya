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
        /// <param name="title"> Параметр фильтра по названию события </param>
        /// <param name="from"> Параметр фильтра по дате начала периода </param>
        /// <param name="to"> Параметр фильтра по дате окончания периода </param>
        /// <returns> Коллекция экземпляров класса события  </returns>
        public List<Event> GetAllEvents(
            string? title = null,
            DateTime? from = null, 
            DateTime? to = null);

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
        public void ReplaceEvent(int id, EventDTO eventDTO);

        /// <summary>
        /// Объявление метода, выполняющего DELETE запрос
        /// </summary>
        /// <param name="id"> идентификатор события </param>
        /// <returns> численный результат операции </returns>
        public int DeleteEvent(int id);
    }
}
