using HappyBooking_Ya.UnitTest.Services;

namespace HappyBooking_Ya.Test
{
    /// <summary>
    /// Класс юнит-тестирвоания
    /// </summary>
    public class UnitTest1
    {
        /// <summary>
        /// Основной метод тестов
        /// </summary>
        [Fact]
        public void Test1()
        {
            var serviceTest = new EventServiceTest();
            serviceTest.getEventByIdTest();
            serviceTest.getAllEventsTest();
            serviceTest.createEventTest();
            serviceTest.deleteEventTest();
            serviceTest.replaceEventTest();
            serviceTest.filterTitleEventTest();
            serviceTest.filterStartDateEventTest();
            serviceTest.filterEndDateEventTest();
            serviceTest.pageEventTest();
            serviceTest.getAllEventsFilteredTest();
            serviceTest.incorrectIdExceptionTest();
            serviceTest.incorrectIdUpdateExceptionTest();
            serviceTest.invalidPageParamsExceptionTest();
        }
    }
}
