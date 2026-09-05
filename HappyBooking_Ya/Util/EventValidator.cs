namespace HappyBooking_Ya.Util
{
    public class EventValidator : AbstractValidator<EventDTO>
    {
        public EventValidator()
        {
            // Валидация: StartAt должна быть строго меньше EndAt
            RuleFor(x => x.StartAt)
                .LessThan(x => x.EndAt)
                .WithMessage("Дата начала события должна быть раньше даты его окончания.");
        }
    }
}
