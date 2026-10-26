namespace HappyBooking_Ya.Data
{
    /// <summary>
    /// Реализация интерфейса репозитория бронирований для in memory хранения
    /// </summary>
    public class InMemoryBookingRepository : IBookingRepository
    {
        /// <summary>
        /// Хранимый в памяти репозиторий бронирований в виде списка
        /// </summary>
        private readonly List<Booking> repoInMemory = new List<Booking>();

        /// <summary>
        /// Реализация метода получения бронирования по id
        /// </summary>
        /// <param name="id">Id бронирования</param>
        /// <returns> Завершенная задача с кземпляром класса бронирования </returns>
        public Task<Booking?> GetBookingById(Guid id)
        {
            var BookingFound = repoInMemory.FirstOrDefault(r => r.Id == id);
            return Task.FromResult(BookingFound);
        }

        /// <summary>
        /// Реализация метода получения массива всех бронирований
        /// </summary>
        /// <returns> Массив бронирований </returns>
        public IEnumerable<Booking> GetBookings()
        {
            IEnumerable<Booking> query = repoInMemory;

            return query;
        }

        /// <summary>
        /// Реализация метода, удаляющего запись о бронировании
        /// </summary>
        /// <param name="id"> идентификатор бронирования </param>
        /// <returns> численный результат операции </returns>
        public int DeleteBooking(Guid id)
        {
            return repoInMemory.RemoveAll(r => r.Id == id);
        }

        /// <summary>
        /// Реализация метода создания бронирования
        /// </summary>
        /// <param name="eventId"> Идентификатор события для бронирования </param>
        /// <returns> Завершенная задача с экземпляром класса бронирования </returns>
        public Task<Booking> CreateBooking(Guid eventId)
        {
            var newBooking = new Booking(
                Guid.NewGuid(),
                eventId,
                BookingStatus.Pending,
                DateTime.UtcNow,
                DateTime.MinValue
                );

            return Task.FromResult(newBooking);
        }

            /// <summary>
            /// Сохранение изменений в репозитории.
            /// Метод пустой, так как в данной реализации репозиторий находитс в памяти и не требует операций с базой
            /// </summary>
            public void SaveChanges()
            {
                return;
            }
        }
    }
