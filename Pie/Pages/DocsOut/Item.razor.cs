using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Pie.Data.Models.Out;
using Pie.Data.Services;
using Pie.Data.Services.Identity;
using Pie.Data.Services.Out;
using Pie.Pages.Components;

namespace Pie.Pages.DocsOut
{
    public partial class Item
    {
        [Inject] public required DocOutService DocService { get; set; }
        [Inject] public required ChangeReasonOutService ChangeReasonService { get; set; }
        [Inject]
        public required AppUserService AppUserService { get; set; }
        [Inject] public IJSRuntime? JSRuntime { get; set; }

        [Parameter] public string? Id { get; set; }

        private DocOutVm? docVm;
        private string? barcode;
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
            this.barcode = barcode;

            var user = await AppUserService.GetUserByBarcodeAsync(barcode);

            await notification.ShowAndHideAsync(this.barcode, 1);
        }

        private async Task SendDocAsync()
        {
            if (docVm == null || docVm.Value == null) return;

            notification.Show("Запрос изменения статуса ...");

            ServiceResult serviceResult = await DocService.SendAsync(docVm.Value);

            if (!serviceResult.IsSuccess)
            {
                await notification.SetMessageAndHideAsync($"Ошибка изменения статуса: {serviceResult.Message}", 2);
                return;
            }
            await notification.SetMessageAndHideAsync($"Запрос изменения статуса - OK", 2);
        }

        private async Task PrintAsync()
        {
            if (JSRuntime == null) return;
            await JSRuntime.InvokeVoidAsync("print");
        }
    }
}
