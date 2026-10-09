namespace HappyBooking_Ya.Data
{
    /// <summary>
    /// Реализация интерфейса репозитория для in memory хранения
    /// </summary>
    public class InMemoryEventRepository : IEventRepository
    {
        /// <summary>
        /// Хранимый в памяти репозиторий в виде списка
        /// </summary>
        private readonly List<Event> repoInMemory = new List<Event>();

        /// <summary>
        /// Коллекция освобожденных идентификаторов (начинаются с 1) коллекции событий, пригодных для повторного использования
        /// </summary>
        private List<int> freeIndexes = new List<int>();

        /// <summary>
        /// Метод проверяет наличие высвободившихся идентификаторов и возвращает первый имеющийся 
        /// </summary>
        /// <returns> идентификатор события </returns>
        private int getFreeIndex()
        {
            int freeIndex = -1;

            if (freeIndexes.Count() != 0)
            {
                freeIndex = freeIndexes[0];
                freeIndexes.RemoveAt(0);
            }

            return freeIndex;
        }

        /// <summary>
        /// Реализация метода получения события по id
        /// </summary>
        /// <param name="id">Id события</param>
        /// <returns> Экземпляр класса события </returns>
        public Event GetEventById(int id)
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
        public int DeleteEvent(int id)
        {
            var removeResult = repoInMemory.RemoveAll(r => r.Id == id);

            if (removeResult > 0)
            {
                freeIndexes.Add(id);
            }

            return removeResult;
        }

        /// <summary>
        /// Реализация метода создания события
        /// </summary>
        /// <param name="eventDTO">  Экземпляр DTO с параметрами события </param>
        /// <returns> Экземпляр класса события </returns>
        public Event CreateEvent(EventDTO eventDTO)
        {
            var localIndex = getFreeIndex();
            var newId = localIndex < 0 ? repoInMemory.Count() + 1 : localIndex;
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
