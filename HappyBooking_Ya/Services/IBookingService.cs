namespace HappyBooking_Ya.Services
{
    /// <summary>
    /// Интерфейс сервиса обработки бронирований
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// Объявление метода, выполняющего получение брони по идентификатору
        /// </summary>
        /// <param name="bookingId"> идентификатор брони </param>
        /// <returns> Неизменяемый экземпляр бронирования с заданным id </returns>
        public Task<BookingResponse>  GetBookingByIdAsync(Guid bookingId);

        /// <summary>
        /// Объявление метода, создающего бронь для указанного события
        /// </summary>
        /// <param name="eventId"> идентификатор события </param>
        /// <returns> экземпляр созданного класса брони </returns>
        public Booking CreateBookingAsync(Guid eventId);
    }
}
