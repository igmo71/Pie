using Microsoft.AspNetCore.Components;
using Pie.Data.Models;
using Pie.Data.Services;

namespace Pie.Pages.DocsOut
{
    public partial class List : IDisposable
    {

        [Inject] public required DocOutService DocService { get; set; }
        [Inject] public required QueueOutService QueueService { get; set; }

        private Dictionary<int, List<DocOut>> docs = new();
        private List<QueueOut> queues = new();

        protected async override Task OnInitializedAsync()
        {
            await GetQueuesAsync();
            await GetDocsAsync();
            await base.OnInitializedAsync();
        }

        private async Task GetQueuesAsync()
        {
            queues = await QueueService.GetQueuesAsync();
        }

        private async Task GetDocsAsync()
        {
            docs = await DocService.GetDictionaryByQueue();
        }

        public void Dispose()
        {

        }
    }
}
