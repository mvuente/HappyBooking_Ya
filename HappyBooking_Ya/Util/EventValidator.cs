namespace HappyBooking_Ya.Util
{
    /// <summary>
    /// Класс валидации параметров события
    /// </summary>
    public class EventValidator : AbstractValidator<EventDTO>
    {
        /// <summary>
        /// Конструктор класса. Валидирует соотношение начальной и конечной дат
        /// </summary>
        public EventValidator()
        {
            RuleFor(x => x.StartAt)
                .LessThan(x => x.EndAt)
                .WithMessage("Дата начала события должна быть раньше даты его окончания.");
        }
    }
}
