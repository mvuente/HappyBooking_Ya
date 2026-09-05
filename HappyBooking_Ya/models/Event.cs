using System.Diagnostics.CodeAnalysis;

namespace HappyBooking_Ya.models
{
    public class Event
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required DateTime StartAt { get; set; }
        public required DateTime EndAt { get; set; }

        [SetsRequiredMembers]
        public Event(int id, string title, string description, DateTime startAt, DateTime endAt) 
        {
            Id = id;
            Title = title;
            Description = description ?? string.Empty;
            StartAt = startAt;
            EndAt = endAt;
        } //конструктор базовый

        public Event(Event OrigEvent) // конструктор копирования
        {
            Id          = OrigEvent.Id;
            Title       = OrigEvent.Title;
            Description = OrigEvent.Description ?? string.Empty;
            StartAt     = OrigEvent.StartAt;
            EndAt       = OrigEvent.EndAt;
        }

        public Event CloneEvent(Event origEvent) => new Event(origEvent.Id, origEvent.Title, origEvent.Description, origEvent.StartAt, origEvent.EndAt);
        
    }
}
