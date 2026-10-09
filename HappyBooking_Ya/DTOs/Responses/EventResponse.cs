namespace HappyBooking_Ya.DTOs
{
    /// <summary>
    /// DTO для ответа на запрос get по id
    /// </summary>
    /// <param name="Id"> Идентификатор события </param>
    /// <param name="Title"> Заголовок события </param>
    /// <param name="Description"> Описание события </param>
    /// <param name="StartAt"> Дата начала события </param>
    /// <param name="EndAt"> Дата конца события </param>
    public record EventResponse(
        int Id,       
        string Title,        
        string Description,
        DateTime StartAt,
        DateTime EndAt)
    {
    }
}
