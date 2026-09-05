namespace HappyBooking_Ya.models
{
    public class EventDTO
    {
        [Required(ErrorMessage = "Название события обязательно для заполнения.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Название события должно быть от 3 до 100 символов")]
        public string Title { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "Дата начала события обязательно для заполнения.")]
        [CurrentDateRange]   
        public required DateTime StartAt { get; set; }

        [Required(ErrorMessage = "Дата окончания события обязательно для заполнения.")]
        [CurrentDateRange]
        public required DateTime EndAt { get; set; }
    }
}
