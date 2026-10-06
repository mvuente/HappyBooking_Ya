namespace HappyBooking_Ya.DTOs
{
    /// <summary>
    /// DTO для ответа на запрос get по id
    /// </summary>
    public record EventResponse(
        /// <param name="Id"> Идентификатор события </param>
        int Id,
        /// <param name="Title"> Заголовок события </param>
        string Title,
        /// <param name="Description"> Описание события </param>
        string Description,
        /// <param name="StartAt"> Дата начала события </param>
        DateTime StartAt,
        /// <param name="EndAt"> Дата конца события </param>
        DateTime EndAt)
    {
    }
}
