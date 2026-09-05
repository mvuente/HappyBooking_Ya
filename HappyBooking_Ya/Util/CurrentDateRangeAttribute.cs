using Microsoft.OpenApi.MicrosoftExtensions;
using System.ComponentModel.DataAnnotations;

namespace HappyBooking_Ya.Util
{
    /// <summary>
    /// Класс валидации дат на предмет ненулевого значения
    /// </summary>
    public class CurrentDateRangeAttribute : RangeAttribute
    {
        /// <summary>
        /// Конструктор класс валидации дат
        /// </summary>
        public CurrentDateRangeAttribute()
            : base(typeof(DateTime), DateTime.Now.AddDays(1).ToString(), DateTime.Now.AddYears(1).ToString())
        {
            ErrorMessage = "Дата должна быть больше текущей и в пределах одного года.";
        }
    }

}
