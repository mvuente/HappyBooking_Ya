using System.ComponentModel;

namespace HappyBooking_Ya.Enums
{
    /// <summary>
    /// Перечисление, содержащее статусы бронирования
    /// </summary>
    public enum BookingStatus
    {
        /// <summary>
        /// Бронь создана и ожидает обработки
        /// </summary>
        [Description("Бронь создана")]
        Pending,

        /// <summary>
        /// Бронь подтверждена
        /// </summary>
        [Description("Бронь подтверждена")]
        Confirmed,

        /// <summary>
        /// Бронь отклонена
        /// </summary>
        [Description("Бронь отклонена")]
        Rejected
    }
}
