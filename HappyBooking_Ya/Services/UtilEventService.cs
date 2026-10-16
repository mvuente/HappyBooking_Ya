using System.Drawing;

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
        protected void ValidatePageParameters(int page, int pageSize)
        {
            long skipLong = (long)(page - 1) * pageSize;

            if (page < 1 || pageSize < 1 || skipLong > int.MaxValue)
            {
                throw new FluentValidation.ValidationException("Неправильные параметры пагинации");
            }
        }
    }
}
