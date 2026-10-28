namespace HappyBooking_Ya.Controllers
{
    /// <summary>
    /// Класс контроллер обработки бронирований
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        /// <summary>
        /// Экземпляр класса сервиса приложения
        /// </summary>
        private readonly IBookingService _bookingService;

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        /// <param name="bookingService"> Экземпляр класса сервиса бронирований </param>
        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Метод, возвращающий конкретное бронирование по его id
        /// </summary>
        /// <param name="id"> Идентификатор бронирования </param>
        /// <returns> JSON структура с деталями ответа </returns>
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiBaseResult), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<BookingResponse>>> Get(Guid id)
        {
            var bookingFound = await _bookingService.GetBookingByIdAsync(id);

            return new OkObjectResult(new ApiResult<BookingResponse>
            {
                Data = bookingFound,
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Получаем бронирование по его id из репозитория"
            });
        }
    }
}
