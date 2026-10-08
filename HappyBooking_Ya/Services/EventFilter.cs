using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyBooking_Ya.Services
{
    public class EventFilter
    {
        public IEnumerable<Event> filterByTitle(IEnumerable<Event> events, string? title)
        {
            var result = events;

            if (!string.IsNullOrEmpty(title))
            {
                result = result.Where(q => q.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            return result;
        }

        public IEnumerable<Event> filterByStart(IEnumerable<Event> events, DateTime? from)
        {
            var result = events;

            if (from.HasValue)
            {
                result = result.Where(q => from <= q.StartAt);
            }

            return result;
        }

        public IEnumerable<Event> filterByEnd(IEnumerable<Event> events, DateTime? to)
        {
            var result = events;

            if (to.HasValue)
            {
                result = result.Where(q => q.EndAt <= to);
            }

            return result;
        }

        public IEnumerable<Event> paginateEvents(IEnumerable<Event> events, int skip, int take)
        {
            return events
                .Skip((skip - 1) * take)
                .Take(take);
        }
    }
}
