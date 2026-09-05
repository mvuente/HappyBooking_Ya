namespace HappyBooking_Ya
{
    /// <summary>
    /// Базовый класс с основными возвращаемыми параметрами
    /// </summary>
    /// <typeparam name="T"> Тип данных </typeparam>
    public class ApiResult<T> : ApiBaseResult
    {
        /// <summary>
        /// Данные, содержащиеся в запросе
        /// </summary>
        public required T Data { get; set; }
    }

    public class ApiResult : ApiBaseResult { }

    public class ApiBaseResult
    {
        /// <summary>
        /// Флаг, указывающий на успешность выполненного запроса
        /// </summary>
        public required bool Success { get; set; }
        /// <summary>
        /// Возвращаемый HTTP-код
        /// </summary>
        public required HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// Сообщение с дополнительной информацией о результате выполненного действия
        /// </summary>
        public required string Message { get; set; }
    }
}
