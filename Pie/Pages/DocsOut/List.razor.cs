using Microsoft.AspNetCore.Components;
using Pie.Data.Models;
using Pie.Data.Models.Identity;
using Pie.Data.Models.Out;
using Pie.Data.Services;
using Pie.Data.Services.Identity;
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
        [Inject] public required AppUserService AppUserService { get; set; }

        private Dictionary<int, List<DocOut>> docs = new();
        private List<QueueOut> queues = new();
        private List<StatusOut> statuses = new();
        private Dictionary<int, int>? countByStatus = new();
        private List<Warehouse> warehouses = new();
        private AppUser? currentUser;

        protected async override Task OnInitializedAsync()
        {
            await GetCurrentUserAsync();
            await GetStatusesAsync();
            await GetQueuesAsync();
            await GetWarehousesAsync();
            SetSearchParameters();
            await GetCountByStatusAsync();
            await GetDocsAsync();
            await base.OnInitializedAsync();
        }

        private void SetSearchParameters()
        {
            if (currentUser?.WarehouseId != null)
                SearchParameters.WarehouseId = currentUser.WarehouseId;
            SearchParameters.StatusKey = statuses.Min(s => s.Key);
        }

        private async Task GetCurrentUserAsync()
        {
            currentUser = await AppUserService.GetCurrentUserAsync();
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
                    NavigationManager?.NavigateTo($"DocsOut/Item/{id}");
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
            //EventDispatcher.DocOutCreated += async (object? sender, Guid args) => await DocCreatedHandle(sender, args);
            DocOutService.DocCreated += async (object? sender, Guid args) => await DocCreatedHandle(sender, args);
            SearchParameters.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            //EventDispatcher.DocOutCreated -= async (object? sender, Guid args) => await DocCreatedHandle(sender, args);
            DocOutService.DocCreated -= async (object? sender, Guid args) => await DocCreatedHandle(sender, args);
            SearchParameters.OnChange -= StateHasChanged;
        }

        private async Task DocCreatedHandle(object? sender, Guid e)
        {
            await SearchHandle();
        }
    }
}
