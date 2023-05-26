using Microsoft.AspNetCore.Components;
using Pie.Data.Models;
using Pie.Data.Models.In;
using Pie.Data.Services;
using Pie.Data.Services.In;

namespace Pie.Pages.DocsIn
{
    public partial class List : IDisposable
    {
        [Inject] public required DocInService DocService { get; set; }
        [Inject] public required QueueInService QueueService { get; set; }
        [Inject] public required StatusInService StatusService { get; set; }
        [Inject] public required WarehouseService WarehouseService { get; set; }
        [Inject] public required SearchInParameters SearchParameters { get; set; }
        [Inject] public required NavigationManager NavigationManager { get; set; }

        private Dictionary<int, List<DocIn>> docs = new();
        private List<QueueIn> queues = new();
        private List<StatusIn> statuses = new();
        private Dictionary<int, int>? countByStatus = new();
        private List<Warehouse> warehouses = new();

        protected async override Task OnInitializedAsync()
        {
            await GetStatusesAsync();
            await GetQueuesAsync();
            await GetWarehousesAsync();
            await GetCountByStatusAsync();
            await GetDocsAsync();
            await base.OnInitializedAsync();
        }

        private async Task GetStatusesAsync()
        {
            statuses = await StatusService.GetListActiveAsync();
        }

        private async Task GetQueuesAsync()
        {
            queues = await QueueService.GetListActiveAsync();
        }

        private async Task GetWarehousesAsync()
        {
            warehouses = await WarehouseService.GetListActiveAsync();
        }

        private async Task GetCountByStatusAsync()
        {
            countByStatus = await DocService.GetCountByStatusAsync(SearchParameters);
        }

        private async Task GetDocsAsync()
        {
            docs = await DocService.GetDictionaryByQueueAsync(SearchParameters);
        }

        private async Task SearchHandle()
        {
            await GetDocsAsync();
            await GetCountByStatusAsync();
            await InvokeAsync(StateHasChanged);
            TryOpenSingleDoc();
        }

        private void TryOpenSingleDoc()
        {
            if (SearchParameters.IsBarcode)
            {
                if (IsDocumentSingle(out Guid? id))
                {
                    SearchParameters.ClearSearchByBarcode();
                    NavigationManager?.NavigateTo($"DocsIn/Item/{id}");
                }
                else
                    SearchParameters.StatusKey = null;
            }
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
            //EventDispatcher.DocInCreated += async (object? sender, Guid args) => await DocCreatedHandle(sender, args);
            DocInService.DocCreated += async (object? sender, Guid args) => await DocCreatedHandle(sender, args);
            SearchParameters.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            //EventDispatcher.DocInCreated -= async (object? sender, Guid args) => await DocCreatedHandle(sender, args);
            DocInService.DocCreated -= async (object? sender, Guid args) => await DocCreatedHandle(sender, args);
            SearchParameters.OnChange -= StateHasChanged;
        }

        private async Task DocCreatedHandle(object? sender, Guid e)
        {
            await SearchHandle();
        }
    }
}
