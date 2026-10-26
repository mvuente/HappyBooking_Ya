namespace HappyBooking_Ya.DTOs
{
    // <summary>
    /// DTO для ответа на запрос получения бронирования по id
    /// </summary>
    /// <param name="Id"> Идентификатор бронирования </param>
    /// <param name="EventId"> Идентификатор события </param>
    /// <param name="Status"> Статус бронирования </param>
    /// <param name="CreatedAt"> Дата создания бронирования </param>
    /// <param name="ProcessedAt"> Дата обработки бронирования </param>
    public record BookingResponse(
        Guid Id,
        Guid EventId,
        BookingStatus Status,
        DateTime CreatedAt,
        DateTime? ProcessedAt)
    {
    }
}
