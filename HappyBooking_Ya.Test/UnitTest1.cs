using HappyBooking_Ya.UnitTest.Services;

namespace HappyBooking_Ya.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var serviceTest = new EventServiceTest();
            //serviceTest.prepareObjects();
            serviceTest.getEventByIdTest();
            serviceTest.getAllEventsTest();
            serviceTest.createEventTest();
            serviceTest.deleteEventTest();
            serviceTest.replaceEventTest();
        }
    }
}
