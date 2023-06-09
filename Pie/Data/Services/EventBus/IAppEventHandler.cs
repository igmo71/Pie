namespace Pie.Data.Services.EventBus
{
    public interface IAppEventHandler<TEvent> where TEvent : IAppEvent
    {
        Task HandleAsync(TEvent appEvent);
    }
}