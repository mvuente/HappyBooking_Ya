using System.Diagnostics.CodeAnalysis;

namespace HappyBooking_Ya.models
{
    /// <summary>
    /// Класс, содержащий данные о бронировании
    /// </summary>
    public class Booking
    {
        /// <summary>
        /// Уникальный идентификатор брони
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Идентификатор события, к которому относится бронь
        /// </summary>
        public required Guid EventId { get; set; }

        /// <summary>
        /// Текущий статус брони
        /// </summary>
        public required BookingStatus Status { get; set; }

        /// <summary>
        /// Дата и время создания брони
        /// </summary>
        public required DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата и время обработки брони
        /// </summary>
        public DateTime? ProcessedAt { get; set; }

        /// <summary>
        /// Базовый конструктор класса
        /// </summary>
        /// <param name="id"> Уникальный идентификатор брони </param>
        /// <param name="eventId"> Идентификатор события, к которому относится бронь </param>
        /// <param name="status"> Текущий статус брони </param>
        /// <param name="createdAt"> Дата и время создания брони </param>
        /// <param name="processedAt"> Дата и время обработки брони </param>
        [SetsRequiredMembers]
        public Booking(Guid id, Guid eventId, BookingStatus status, DateTime createdAt, DateTime? processedAt)
        {
            Id = id;
            EventId = eventId;
            Status = status;
            CreatedAt = createdAt;
            ProcessedAt = processedAt;
        }
    }
}
