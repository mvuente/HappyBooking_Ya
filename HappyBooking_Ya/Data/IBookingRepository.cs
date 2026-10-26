namespace HappyBooking_Ya.Data
{
    /// <summary>
    /// Интерфейс репозитория бронирований
    /// </summary>
    public interface IBookingRepository
    {
        /// <summary>
        /// Метод получения бронирования по id
        /// </summary>
        /// <param name="id">Id события</param>
        /// <returns> Завершенная задача с кземпляром класса бронирования </returns>
        public Task<Booking?> GetBookingById(Guid id);

        /// <summary>
        /// Метод получения массива всех бронирований
        /// </summary>
        /// <returns> Массив бронирований </returns>
        public IEnumerable<Booking> GetBookings();

        /// <summary>
        /// Создание бронирования
        /// </summary>
        /// <param name="EventId"> Идентификатор события для бронирования </param>
        /// <returns> Завершенная задача с экземпляром класса бронирования </returns>
        public Task<Booking> CreateBooking(Guid EventId);

        /// <summary>
        /// Сохранение изменений в репозитории
        /// </summary>
        public void SaveChanges();

        /// <summary>
        /// Объявление метода, удаляющего запись
        /// </summary>
        /// <param name="id"> идентификатор бронирования </param>
        /// <returns> численный результат операции </returns>
        public int DeleteBooking(Guid id);
    }
}
