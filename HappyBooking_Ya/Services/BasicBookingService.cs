namespace HappyBooking_Ya.Services
{
    /// <summary>
    /// Реализация интерфейса сервиса обработки бронирований
    /// </summary>
    public class BasicBookingService : IBookingService
    {
        /// <summary>
        /// Коллекция бронирований
        /// </summary>
        private readonly IBookingRepository bookingRepository;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="_bookingRepository">  Экземпляр репозитория бронирований</param>
        public BasicBookingService(IBookingRepository _bookingRepository)
        {
            bookingRepository = _bookingRepository;
        }

        /// <summary>
        /// Реализация метода, выполняющего получение брони по идентификатору
        /// </summary>
        /// <param name="bookingId"> идентификатор брони </param>
        /// <returns> Неизменяемый экземпляр бронирования с заданным id </returns>
        public async Task<BookingResponse> GetBookingByIdAsync(Guid bookingId)
        {
            var BookingFound = await bookingRepository.GetBookingById(bookingId);

            if (BookingFound == null)
            {
                throw new NotFoundException("бронирование", bookingId);
            }

            return new BookingResponse(
                BookingFound.Id,
                BookingFound.EventId,
                BookingFound.Status,
                BookingFound.CreatedAt,
                BookingFound.ProcessedAt);
        }

        /// <summary>
        /// Реализация метода, создающего бронь для указанного события
        /// </summary>
        /// <param name="eventId"> идентификатор события </param>
        /// <returns> экземпляр созданного класса брони </returns>
        public async Task<Booking> CreateBookingAsync(Guid eventId)
        {
            return await bookingRepository.CreateBooking(eventId);
        }
    }
}
