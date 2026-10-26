namespace HappyBooking_Ya.Data
{
    /// <summary>
    /// Реализация интерфейса репозитория событий для in memory хранения
    /// </summary>
    public class InMemoryEventRepository : IEventRepository
    {
        /// <summary>
        /// Хранимый в памяти репозиторий в виде списка
        /// </summary>
        private readonly List<Event> repoInMemory = new List<Event>();
        
        /// <summary>
        /// Реализация метода получения события по id
        /// </summary>
        /// <param name="id">Id события</param>
        /// <returns> Экземпляр класса события </returns>
        public Event? GetEventById(Guid id)
        {
            return repoInMemory.FirstOrDefault(r => r.Id == id);
        }

        /// <summary>
        /// Реализация метода получения массива всех событий
        /// </summary>
        /// <returns> Массив событий </returns>
        public IEnumerable<Event> GetEvents()
        {
            IEnumerable<Event> query = repoInMemory;

            return query;
        }

        /// <summary>
        /// Реализация метода, удаляющего запись
        /// </summary>
        /// <param name="id"> идентификатор события </param>
        /// <returns> численный результат операции </returns>
        public int DeleteEvent(Guid id)
        {
            return repoInMemory.RemoveAll(r => r.Id == id);
        }

        /// <summary>
        /// Реализация метода создания события
        /// </summary>
        /// <param name="eventDTO">  Экземпляр DTO с параметрами события </param>
        /// <returns> Экземпляр класса события </returns>
        public Event CreateEvent(EventDTO eventDTO)
        {
            var newId = Guid.NewGuid();
            var newEvent = new Event(newId, eventDTO.Title, eventDTO.Description, eventDTO.StartAt, eventDTO.EndAt);
            repoInMemory.Add(newEvent);

            return newEvent;
        }

        /// <summary>
        /// Сохранение изменений в репозитории.
        /// Метод пустой, так как в даннйо реализации репозиторий находитс в памяти и не требует операций с базой
        /// </summary>
        public void SaveChanges()
        {
            return;
        }
    }
}
