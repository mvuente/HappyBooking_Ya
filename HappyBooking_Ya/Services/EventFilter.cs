using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyBooking_Ya.Services
{
    /// <summary>
    /// Класс, выполняющий фильтрацию
    /// </summary>
    public class EventFilter
    {
        /// <summary>
        /// Метод фильтрации по заголовку события
        /// </summary>
        /// <param name="events"> Коллекция событий </param>
        /// <param name="title"> Искомый фрагмент строки заголовка </param>
        /// <returns> Отфильтрованный по параметру массив </returns>
        public IEnumerable<Event> filterByTitle(IEnumerable<Event> events, string? title)
        {
            var result = events;

            if (!string.IsNullOrEmpty(title))
            {
                result = result.Where(q => q.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            return result;
        }

        /// <summary>
        /// Метод фильтрации по дате начала события
        /// </summary>
        /// <param name="events"> Коллекция событий </param>
        /// <param name="from"> Нижняя граница даты начала </param>
        /// <returns> Отфильтрованный по параметру массив </returns>
        public IEnumerable<Event> filterByStart(IEnumerable<Event> events, DateTime? from)
        {
            var result = events;

            if (from.HasValue)
            {
                result = result.Where(q => from <= q.StartAt);
            }

            return result;
        }

        /// <summary>
        /// Метод фильтрации по дате окончания события
        /// </summary>
        /// <param name="events"> Коллекция событий </param>
        /// <param name="to"> Верхняя граница даты окончания </param>
        /// <returns> Отфильтрованный по параметру массив </returns>
        public IEnumerable<Event> filterByEnd(IEnumerable<Event> events, DateTime? to)
        {
            var result = events;

            if (to.HasValue)
            {
                result = result.Where(q => q.EndAt <= to);
            }

            return result;
        }

        /// <summary>
        /// Пагинация массива событий по заданным параметрам
        /// </summary>
        /// <param name="events"> Коллекция событий </param>
        /// <param name="skip"> Номер страницы к выдаче </param>
        /// <param name="take"> Размер страницы </param>
        /// <returns> Заданная страница выдачи </returns>
        public IEnumerable<Event> paginateEvents(IEnumerable<Event> events, int skip, int take)
        {
            return events
                .Skip((skip - 1) * take)
                .Take(take);
        }
    }
}
