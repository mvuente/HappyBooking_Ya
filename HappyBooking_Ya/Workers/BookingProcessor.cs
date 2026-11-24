namespace HappyBooking_Ya.Workers
{
    public class BookingProcessor : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                /*
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IBookingRepository>();
                */
                // Работа с repository
                await Task.Delay(5000, ct);
            }
        }
    }
}
