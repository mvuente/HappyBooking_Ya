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

        public (List<Event>, int) GetEvents(
            string? title,
            DateTime? from,
            DateTime? to,
            int skip,
            int take)
        {
            IEnumerable<Event> query = repoInMemory;

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(q => q.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            if (from.HasValue)
            {
                query = query.Where(q => from <= q.StartAt);
            }

            if (to.HasValue)
            {
                query = query.Where(q => q.EndAt <= to);
            }

            var count = query.Count();

            var events = query
                .Skip((skip - 1) * take)
                .Take(take)
                .ToList();

            return (events, count);
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
