namespace Pie.Data.Services.EventBus
{
    public class DocOutEventHandler : IAppEventHandler<DocOutDtoReceivedEvent>, IAppEventHandler<DocOutCreatedEvent>
    {
        public async Task HandleAsync(DocOutDtoReceivedEvent appEvent)
        {
            await Console.Out.WriteLineAsync($"{appEvent.Value.Name} - received");
        }

        public async Task HandleAsync(DocOutCreatedEvent appEvent)
        {
            await Console.Out.WriteLineAsync($"{appEvent.Value.Name} - created");
        }
    }
}
