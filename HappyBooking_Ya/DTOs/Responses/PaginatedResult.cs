namespace HappyBooking_Ya.DTOs
{
    /// <summary>
    /// DTO для ответа на запрос get/events
    /// </summary>
    /// <param name="TotalEventsNumber"> Общее количество событий, отвечающих параметрам фильтрации </param>
    /// <param name="Events"> Массив отфильтрованных событий </param>
    /// <param name="Page"> Номер страницы к выдаче </param>
    /// <param name="PageSize"> Размер страницы </param>
    public record PaginatedResult(      
        int TotalEventsNumber,       
        IEnumerable<Event> Events,
        int Page,
        int PageSize)
    { }
}


