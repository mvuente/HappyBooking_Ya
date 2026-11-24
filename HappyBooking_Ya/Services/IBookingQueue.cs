namespace HappyBooking_Ya.Services
{
    /// <summary>
    /// Интерфейс работы с очередью бронирований
    /// </summary>
    public interface IBookingQueue
    {
        /// <summary>
        /// Объявление метода помещения бронирования в очередь на обработку
        /// </summary>
        /// <param name="bookingToProcess"> Бронирование для постановки в очередь </param>
        void Enqueue(Booking bookingToProcess);

        /// <summary>
        /// Объявление метода извлечения бронирования из очереди
        /// </summary>
        /// <param name="bookingProcessed"> Бронирование, извлекаемое из очереди </param>
        /// <returns> Извлеченное бронирование </returns>
        bool TryDequeue(out Booking bookingProcessed);
    }
}
