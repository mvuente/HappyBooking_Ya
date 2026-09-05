using Microsoft.OpenApi.MicrosoftExtensions;
using System.ComponentModel.DataAnnotations;

namespace HappyBooking_Ya.Util
{
    public class CurrentDateRangeAttribute : RangeAttribute
    {
        public CurrentDateRangeAttribute()
            : base(typeof(DateTime), DateTime.Now.AddDays(1).ToString(), DateTime.Now.AddYears(1).ToString())
        {
            ErrorMessage = "Дата должна быть больше текущей и в пределах одного года.";
        }
    }

}
