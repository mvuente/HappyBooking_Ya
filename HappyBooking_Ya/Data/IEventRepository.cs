namespace HappyBooking_Ya.Data
{
    /// <summary>
    /// Интерфейс репозиотрия
    /// </summary>
    public interface IEventRepository
    {
        /// <summary>
        /// Метод получения события по id
        /// </summary>
        /// <param name="id">Id события</param>
        /// <returns> Экземпляр класса события </returns>
        public Event GetEventById(int id);

        /// <summary>
        /// Метод получения массива всех событий
        /// </summary>
        /// <returns> Массив событий </returns>
        public IEnumerable<Event> GetEvents();

        /// <summary>
        /// Создание события
        /// </summary>
        /// <param name="eventDTO">  Экземпляр DTO с параметрами события </param>
        /// <returns> Экземпляр класса события </returns>
        public Event CreateEvent(EventDTO eventDTO);

        /// <summary>
        /// Сохранение изменений в репозитории
        /// </summary>
        public void SaveChanges();

        /// <summary>
        /// Объявление метода, удаляющего запись
        /// </summary>
        /// <param name="id"> идентификатор события </param>
        /// <returns> численный результат операции </returns>
        public int DeleteEvent(int id);
    }
}
