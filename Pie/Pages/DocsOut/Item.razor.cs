using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Pie.Data.Models.Out;
using Pie.Data.Services.Identity;
using Pie.Data.Services.Out;
using Pie.Pages.Components;

namespace Pie.Pages.DocsOut
{
    public partial class Item
    {
        [Inject] public required DocOutService DocService { get; set; }
        [Inject] public required ChangeReasonOutService ChangeReasonService { get; set; }
        [Inject] public required AppUserService AppUserService { get; set; }
        [Inject] public required NavigationManager NavigationManager { get; set; }
        [Inject] public IJSRuntime? JSRuntime { get; set; }
        [Inject] public ILogger<Item>? Logger { get; set; }

        [Parameter] public string? Id { get; set; }

        private DocOutVm? docVm;
        private List<ChangeReasonOut>? changeReasons;
        private Notification notification = new();
        private string pageMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await GetDocAsync();
            await GetChangeReasonsAsync();
        }

        private async Task GetChangeReasonsAsync()
        {
            changeReasons = await ChangeReasonService.GetListAsync();
        }

        private async Task GetDocAsync()
        {
            if (string.IsNullOrEmpty(Id)) return;

            docVm = await DocService.GetVmAsync(Guid.Parse(Id));
        }

        private async Task ScannedBarcodeAsync(string barcode)
        {
            await notification.ShowAndHideAsync(barcode, 1);
            await SendDocAsync(barcode);
        }

        private async Task SendDocAsync(string? barcode = null)
        {
            if (docVm == null || docVm.Value == null) return;

            notification.Show("Запрос изменения статуса ...");

            try
            {
                await DocService.SendTo1cAsync(docVm.Value, barcode);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "DocOut.Item SendDocAsync {Message}", ex.Message);
                await notification.SetMessageAndHideAsync(ex.Message, 2);
                return;
            }

            await notification.SetMessageAndHideAsync($"Запрос изменения статуса - OK", 2);

            NavigationManager.NavigateTo($"DocsOut/List");
        }

        private async Task DeleteDocAsync()
        {
            if (docVm == null || docVm.Value == null) return;
            
            //notification.Show("Удаление документа ...");

            await DocService.DeleteAsync(docVm.Value.Id);

            //await notification.SetMessageAndHideAsync($"Документ удален!", 2);

            NavigationManager.NavigateTo($"DocsOut/List");
        }

        private async Task PrintAsync()
        {
            if (JSRuntime == null) return;
            await JSRuntime.InvokeVoidAsync("print");
        }
    }
}
