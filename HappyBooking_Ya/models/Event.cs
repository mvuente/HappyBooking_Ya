using System.Diagnostics.CodeAnalysis;

namespace HappyBooking_Ya.models
{
    /// <summary>
    /// Класс модель приложения
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Идентификатор события. Нумерация начинается с 1
        /// </summary>
        public required int Id { get; set; }

        /// <summary>
        /// Заголовок события
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Описание события
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Дата и время начала события
        /// </summary>
        public required DateTime StartAt { get; set; }

        /// <summary>
        /// Дата и время окончания события
        /// </summary>
        public required DateTime EndAt { get; set; }

        /// <summary>
        /// Базовый конструктор класса
        /// </summary>
        /// <param name="id"> Идентификатор события </param>
        /// <param name="title"> Заголовок события </param>
        /// <param name="description"> Описание события </param>
        /// <param name="startAt"> Дата и время начала события </param>
        /// <param name="endAt"> Дата и время окончания события </param>
        [SetsRequiredMembers]
        public Event(int id, string title, string description, DateTime startAt, DateTime endAt) 
        {
            Id          = id;
            Title       = title;
            Description = description ?? string.Empty;
            StartAt     = startAt;
            EndAt       = endAt;
        } 

        /// <summary>
        /// Конструктор копирования
        /// </summary>
        /// <param name="OrigEvent"> Исходный экземпляр класса </param>
        public Event(Event OrigEvent) 
        {
            Id          = OrigEvent.Id;
            Title       = OrigEvent.Title;
            Description = OrigEvent.Description ?? string.Empty;
            StartAt     = OrigEvent.StartAt;
            EndAt       = OrigEvent.EndAt;
        }

        /// <summary>
        /// Метод копирования класса
        /// </summary>
        /// <param name="origEvent"> Исходный экземпляр класса </param>
        /// <returns> Копия экземпляра класса </returns>
        public Event CloneEvent(Event origEvent) => new Event(origEvent.Id, origEvent.Title, origEvent.Description, origEvent.StartAt, origEvent.EndAt);
        
    }
}
