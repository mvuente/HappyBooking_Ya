namespace HappyBooking_Ya.Exceptions
{
    /// <summary>
    /// Кастомный класс исключения
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Тип ресурса
        /// </summary>
        public string ResourceType { get; }

        /// <summary>
        /// Id ресурса
        /// </summary>
        public int ResourceId { get; }

        /// <summary>
        /// Констурктор класса
        /// </summary>
        /// <param name="resourceType"> Тип ресурса </param>
        /// <param name="resourceId"> Id ресурса </param>
        public NotFoundException(string resourceType, int resourceId)
            : base($"{resourceType} с ID {resourceId} не найден")
        {
            ResourceType = resourceType;
            ResourceId = resourceId;
        }
    }
}