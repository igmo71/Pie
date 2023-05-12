using Microsoft.AspNetCore.Components;
using Pie.Data.Models;
using Pie.Data.Models.Out;
using Pie.Data.Services;
using Pie.Data.Services.Out;

namespace Pie.Pages.DocsOut
{
    public partial class List : IDisposable
    {
        [Inject] public required DocOutService DocService { get; set; }
        [Inject] public required QueueOutService QueueService { get; set; }
        [Inject] public required StatusOutService StatusService { get; set; }
        [Inject] public required WarehouseService WarehouseService { get; set; }
        [Inject] public required SearchOutParameters SearchParameters { get; set; }
        [Inject] public required NavigationManager NavigationManager { get; set; }
        [Inject] public required EventDispatcher EventDispatcher { get; set; }

        private Dictionary<int, List<DocOut>> docs = new();
        private List<QueueOut> queues = new();
        private List<StatusOut> statuses = new();
        private Dictionary<int, int>? countByStatus = new();
        private List<Warehouse> warehouses = new();
        private string pageMessage = "Hello!";

        protected async override Task OnInitializedAsync()
        {
            await GetStatusesAsync();
            await GetCountByStatusAsync();
            await GetQueuesAsync();
            await GetWarehousesAsync();
            await GetDocsAsync();
            await base.OnInitializedAsync();
        }

        private async Task GetStatusesAsync()
        {
            statuses = await StatusService.GetStatusesAsync();
        }

        private async Task GetCountByStatusAsync()
        {
            countByStatus = await StatusService.GetCountByStatusAsync(SearchParameters);
        }

        private async Task GetQueuesAsync()
        {
            queues = await QueueService.GetQueuesAsync();
        }

        private async Task GetWarehousesAsync()
        {
            warehouses = await WarehouseService.GetAsync();
        }

        private async Task GetDocsAsync()
        {
            docs = await DocService.GetDictionaryByQueue(SearchParameters);
        }

        private async Task SearchHandle()
        {
            await GetDocsAsync();
            await GetCountByStatusAsync();
            await InvokeAsync(StateHasChanged);
            //TryOpenItem();
        }

        private void TryOpenItem()
        {
            if (IsDocumentSingle(out Guid? id))
            {
                SearchParameters.ClearSearchByBarcode();
                NavigationManager?.NavigateTo($"DocsOut/Item/{id}");
            }
            else
                SearchParameters.StatusKey = null;
        }

        private bool IsDocumentSingle(out Guid? id)
        {
            var result = docs != null
                && docs.Count == 1
                && docs.FirstOrDefault().Value != null
                && docs.FirstOrDefault().Value.Count == 1;

            id = docs?.FirstOrDefault().Value?.FirstOrDefault()?.Id;

            return result;
        }

        protected override void OnParametersSet()
        {
            //DocService.DocCreated += async (object? sender, Guid args) => await DocCreatedHandle(sender, args);
            EventDispatcher.DocOutCreated += async (object? sender, Guid args) => await DocCreatedHandle(sender, args);
            SearchParameters.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            //DocService.DocCreated -= async (object? sender, Guid args) => await DocCreatedHandle(sender, args);
            EventDispatcher.DocOutCreated -= async (object? sender, Guid args) => await DocCreatedHandle(sender, args);
            SearchParameters.OnChange -= StateHasChanged;
        }

        private async Task DocCreatedHandle(object? sender, Guid e)
        {
            await SearchHandle();
        }
    }
}
