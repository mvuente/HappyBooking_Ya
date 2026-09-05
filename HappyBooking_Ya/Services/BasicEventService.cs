namespace HappyBooking_Ya.Services
{
    /// <summary>
    /// Реализация интерфейса сервиса приложения для случая общего события и хранения in memory
    /// </summary>
    public class BasicEventService : IEventService
    {
        /// <summary>
        /// Коллекция событий
        /// </summary>
        private List<Event> repoInMemory = new List<Event>();

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
        /// Реализация метода, выполняющего GET запрос
        /// </summary>
        /// <param name="id"> идентификатор события </param>
        /// <returns> Экземпляр класса события с заданным id </returns>
        public Event GetEvent(int id)
        {
            var EventFound = repoInMemory.Find(r => r.Id == id);

            return EventFound.CloneEvent(EventFound); // не валидирую, так как в контроллере есть обработка exception
        }

        /// <summary>
        /// Реализация метода, выполняющего GET запрос
        /// </summary>
        /// <returns> Коллекция экземпляров класса события  </returns>
        public List<Event> GetAllEvents()
        {
            return repoInMemory.Select(e => e.CloneEvent(e)).ToList();
        }

        /// <summary>
        /// Реализация метода, выполняющего DELETE запрос
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
        /// Реализация метода, выполняющего POST запрос
        /// </summary>
        /// <param name="eventDTO"> экземпляр класса с параметрами события </param>
        /// <returns> экземпляр созданного класса события </returns>
        public Event CreateEvent(EventDTO eventDTO)
        {
            var localIndex  = getFreeIndex();
            var newId       = localIndex < 0 ? repoInMemory.Count() + 1 : localIndex;
            var newEvent    = new Event(newId, eventDTO.Title, eventDTO.Description, eventDTO.StartAt, eventDTO.EndAt);
            repoInMemory.Add(newEvent);

            return newEvent; 
        }

        /// <summary>
        /// Реализация метода, выполняющего PUT запрос
        /// </summary>
        /// <param name="id"> идентификатор события </param>
        /// <param name="eventDTO"> экземпляр класса с параметрами события </param>
        /// <returns> обновленный экземпляр класса события </returns>
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
