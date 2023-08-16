using Pie.Common;
using Pie.Data.Models.Out;

namespace Pie.Data.Services.Out
{
    public static class SearchOutExtensions
    {
        public static IQueryable<DocOut> Search(this IQueryable<DocOut> query, SearchOutParameters parameters)
        {
            if (parameters.IsBarcode && !string.IsNullOrEmpty(parameters.SearchBarcode))
            {
                Guid id = GuidBarcodeConvert.GuidFromNumericString(parameters.SearchBarcode);
                query = query.Where(e => e.Id == id || e.BaseDocs.Any(bd => bd.BaseDocId == id));
            }

            if (parameters.IsForm)
            {
                if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
                    query = query.Where(e =>
                        (!string.IsNullOrEmpty(e.Name) && e.Name.ToLower().Contains(parameters.SearchTerm.Trim().ToLower())) ||
                        (!string.IsNullOrEmpty(e.QueueNumber) && e.QueueNumber.ToLower().Contains(parameters.SearchTerm.Trim().ToLower()))
                    );

                if (parameters.WarehouseId != null && parameters.WarehouseId != Guid.Empty)
                    query = query.Where(e => e.WarehouseId == parameters.WarehouseId);

                //if (parameters.DeliveryAreaId != null && parameters.DeliveryAreaId != Guid.Empty)
                //    query = query.Where(e => e.DeliveryAreaId == parameters.DeliveryAreaId);
            }

            if (parameters.IsStatus && parameters.StatusKey != null)
            {
                query = query.Where(e => e.StatusKey != null && e.StatusKey == parameters.StatusKey);
            }

            return query;
        }
    }
}
