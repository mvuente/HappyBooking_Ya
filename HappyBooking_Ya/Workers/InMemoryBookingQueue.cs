

namespace HappyBooking_Ya.Workers
{
    /// <summary>
    /// Реализация интерфейса работы с очередью бронирований
    /// </summary>
    public class InMemoryBookingQueue : IBookingQueue
    {
        /// <summary>
        /// Очередь бронирований
        /// </summary>
        private readonly ConcurrentQueue<Booking> _bookingQueue = new();

        /// <summary>
        /// Реализация метода помещения бронирования в очередь на обработку
        /// </summary>
        /// <param name="bookingToProcess"> Бронирование для постановки в очередь </param>
        public void Enqueue(Booking bookingToProcess)
        {
            _bookingQueue.Enqueue(bookingToProcess);
        }

        /// <summary>
        /// Реализация метода извлечения бронирования из очереди
        /// </summary>
        /// <param name="bookingProcessed"> Бронирование, извлекаемое из очереди </param>
        /// <returns> Извлеченное бронирование </returns>
        public bool TryDequeue(out Booking bookingProcessed)
        {
            return _bookingQueue.TryDequeue(out bookingProcessed);
        }
        
    }
}
