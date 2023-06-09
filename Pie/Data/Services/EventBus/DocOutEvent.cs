using Pie.Data.Models.Out;

namespace Pie.Data.Services.EventBus
{
    public class DocOutCreatedEvent : IAppEvent
    {
        public required DocOut Value { get; set; }
    }

    public class DocOutDtoReceivedEvent : IAppEvent
    {
        public required DocOutDto Value { get; set; }
    }
}
