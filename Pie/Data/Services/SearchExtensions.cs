﻿using Pie.Common;
using Pie.Data.Models;

namespace Pie.Data.Services
{
    public static class SearchExtensions
    {
        public static IQueryable<DocOut> Search(this IQueryable<DocOut> query, SearchParameters parameters)
        {
            if (parameters.IsBarcode && !string.IsNullOrEmpty(parameters.SearchBarcode))
            {
                var id = BarcodeGuidConvert.FromNumericString(parameters.SearchBarcode);
                query = query.Where(e => e.Id == id || e.BaseDocs.Any(bd => bd.BaseDocId == id));
            }

            if (parameters.IsForm)
            {
                if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
                    query = query.Where(e => !string.IsNullOrEmpty(e.Name) && e.Name.ToLower().Contains(parameters.SearchTerm.Trim().ToLower()));

                if (parameters.WarehouseId != null && parameters.WarehouseId != Guid.Empty)
                    query = query.Where(e => e.WarehouseId == parameters.WarehouseId);
            }

            return query;
        }

        public static IQueryable<DocOut> SearchByStatus(this IQueryable<DocOut> query, SearchParameters parameters)
        {
            if (parameters.IsStatus && parameters.StatusKey != null )
            {
                query = query.Where(e => e.StatusKey == parameters.StatusKey);
            }

            return query;
        }
    }
}
