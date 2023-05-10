using Microsoft.AspNetCore.Components;
using Pie.Data.Models.Out;
using Pie.Data.Services;
using Pie.Data.Services.Application;
using Pie.Data.Services.Out;
using Pie.Pages.DocsOut.Components;

namespace Pie.Pages.DocsOut
{
    public partial class Item
    {
        [Inject] public required DocOutService DocService { get; set; }
        [Inject] public required CurrentUserService CurrentUserService { get; set; }
        [Inject] public required ChangeReasonOutService ChangeReasonService { get; set; }

        [Parameter] public string? Id { get; set; }

        private DocOut? doc;
        private Guid userId;
        private string? barcode;
        private List<ChangeReasonOut>? changeReasons;
        private string pageMessage = string.Empty;
        private EditDialog? EditDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetDocAsync();
            await GetChangeReasonsAsync();
            userId = CurrentUserService.UserId;
        }

        private async Task GetChangeReasonsAsync()
        {
            changeReasons = await ChangeReasonService.GetAsync();
        }

        private async Task GetDocAsync()
        {
            if (string.IsNullOrEmpty(Id)) return;

            doc = await DocService.GetDocAsync(Guid.Parse(Id));
        }

        private void ScannedBarcode(string barcode)
        {
            this.barcode = barcode;
            pageMessage = barcode ?? string.Empty;
        }

        private void EditDoc(DocOutProduct product)
        {
            EditDialog?.Open(product);
        }

        private void UpdateDoc()
        {
            EditDialog?.Close();
        }

        private async Task SendDocAsync()
        {
            if (doc == null) return;
            ServiceResult result = await DocService.SendAsync(doc);
        }
    }
}
