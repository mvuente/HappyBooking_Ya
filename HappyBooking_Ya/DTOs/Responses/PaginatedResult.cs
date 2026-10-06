namespace HappyBooking_Ya.DTOs
{
    /// <summary>
    /// DTO для ответа на запрос get/events
    /// </summary>
    public record PaginatedResult(
        /// <param name="TotalEventsNumber"> Общее количество событий, отвечающих параметрам фильтрации </param>
        int TotalEventsNumber,
        /// <param name="Events"> Массив отфильтрованных событий </param>
        IEnumerable<Event> Events,
        /// <param name="Page"> Номер страницы к выдаче </param>
        int Page,
        /// <param name="PageSize"> Размер страницы </param>
        int PageSize)
    { }
}


