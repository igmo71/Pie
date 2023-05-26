using Microsoft.AspNetCore.Components;
using Pie.Data.Models.In;
using Pie.Data.Services.In;

namespace Pie.Pages.DocsIn
{
    public partial class List : IDisposable
    {

        [Inject] public required DocInService DocService { get; set; }
        [Inject] public required QueueInService QueueService { get; set; }

        private Dictionary<int, List<DocIn>> docs = new();
        private List<QueueIn> queues = new();

        protected async override Task OnInitializedAsync()
        {
            await GetQueuesAsync();
            await GetDocsAsync();
            await base.OnInitializedAsync();
        }

        private async Task GetQueuesAsync()
        {
            queues = await QueueService.GetListAsync();
        }

        private async Task GetDocsAsync()
        {
            docs = await DocService.GetDictionaryByQueueAsync();
        }

        public void Dispose()
        {

        }
    }
}
