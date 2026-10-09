namespace HappyBooking_Ya.Services
{
    /// <summary>
    /// Абастрактный класс для общих методов сервиса
    /// </summary>
    public abstract class UtilEventService
    {
        /// <summary>
        /// Метод валидации параметров пагинации
        /// </summary>
        /// <param name="page">Номер страницы вывода</param>
        /// <param name="pageSize">Количество записей на странице</param>
        protected void validatePageParameters(int page, int pageSize)
        {
            if (page < 1 || pageSize < 0)
            {
                throw new FluentValidation.ValidationException("Неправильные параметры пагинации");
            }
        }
    }
}
