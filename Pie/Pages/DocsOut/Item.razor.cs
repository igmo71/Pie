using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Pie.Data.Models.Out;
using Pie.Data.Services;
using Pie.Data.Services.Out;
using Pie.Pages.DocsOut.Components;

namespace Pie.Pages.DocsOut
{
    public partial class Item
    {
        [Inject] public required DocOutService DocService { get; set; }
        [Inject] public required ChangeReasonOutService ChangeReasonService { get; set; }
        [Inject] public IJSRuntime? JSRuntime { get; set; }

        [Parameter] public string? Id { get; set; }

        private DocOutVm? docVm;
        private string? barcode;
        private List<ChangeReasonOut>? changeReasons;
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

        private void ScannedBarcode(string barcode)
        {
            this.barcode = barcode;
            pageMessage = barcode ?? string.Empty;
        }

        private async Task SendDocAsync()
        {
            if (docVm == null || docVm.Value == null) return;
            ServiceResult result = await DocService.SendAsync(docVm.Value);
        }

        private async Task PrintAsync()
        {
            if (JSRuntime == null) return;
            await JSRuntime.InvokeVoidAsync("print");
        }
    }
}
