namespace HappyBooking_Ya.Data
{
    public class InMemoryEventRepository : IEventRepository
    {
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

        public Event GetEventById(int id)
        {
            return repoInMemory.FirstOrDefault(r => r.Id == id);
        }

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

        public Event CreateEvent(EventDTO eventDTO)
        {
            var localIndex = getFreeIndex();
            var newId = localIndex < 0 ? repoInMemory.Count() + 1 : localIndex;
            var newEvent = new Event(newId, eventDTO.Title, eventDTO.Description, eventDTO.StartAt, eventDTO.EndAt);
            repoInMemory.Add(newEvent);

            return newEvent;
        }

        public void SaveChanges()
        {
            return;
        }
    }
}
