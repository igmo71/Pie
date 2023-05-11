namespace Pie.Data.Services
{
    public class EventDispatcher
    {
        public event EventHandler<Guid>? DocOutCreated;

        public void OnDocOutCreated(Guid id)
        {
            DocOutCreated?.Invoke(this, id);
        }
    }
}
