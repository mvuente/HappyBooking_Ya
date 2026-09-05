namespace HappyBooking_Ya.models
{
    /// <summary>
    /// DTO модель события
    /// </summary>
    public class EventDTO
    {
        /// <summary>
        /// Заголовок события
        /// </summary>
        [Required(ErrorMessage = "Название события обязательно для заполнения.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Название события должно быть от 3 до 100 символов")]
        public string Title { get; set; }

        /// <summary>
        /// Описание события
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Дата начала события
        /// </summary>
        [Required(ErrorMessage = "Дата начала события обязательно для заполнения.")]
        [CurrentDateRange]   
        public required DateTime StartAt { get; set; }

        /// <summary>
        /// Дата окончания события
        /// </summary>
        [Required(ErrorMessage = "Дата окончания события обязательно для заполнения.")]
        [CurrentDateRange]
        public required DateTime EndAt { get; set; }
    }
}
