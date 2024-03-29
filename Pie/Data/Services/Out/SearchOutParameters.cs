﻿namespace Pie.Data.Services.Out
{
    public class SearchOutParameters
    {
        public SearchOutParameters()
        {
            IsBarcode = false;
            IsForm = true;
            IsStatus = true;
        }

        public bool IsBarcode { get; set; }
        public bool IsForm { get; set; }
        public bool IsStatus { get; set; }

        // Barcode
        public string? SearchBarcode { get; set; }

        // Form
        public string? SearchTerm { get; set; }
        public Guid? WarehouseId { get; set; }
        public Guid? DeliveryAreaId { get; set; }

        // Status
        public int? StatusKey { get; set; }


        public Guid? UserId { get; set; }

        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public void SetSearchByBarcode(string? barcode)
        {
            IsBarcode = true;
            IsForm = false;
            IsStatus = false;

            SearchBarcode = barcode;
            //SearchTerm = null;
            StatusKey = null;

            NotifyStateChanged();
        }

        public void ClearSearchByBarcode()
        {
            IsBarcode = false;
            IsForm = true;
            IsStatus = true;

            SearchBarcode = null;

            NotifyStateChanged();
        }

        public void SetSearchByForm()
        {
            IsForm = true;
            NotifyStateChanged();
        }

        public void ClearSearchByForm()
        {
            SearchTerm = null;
            WarehouseId = Guid.Empty;
            DeliveryAreaId = Guid.Empty;
            NotifyStateChanged();
        }

        public void ClearSearchByTerm()
        {
            SearchTerm = null;
            NotifyStateChanged();
        }

        public void ClearSearchByWarehouse()
        {
            WarehouseId = Guid.Empty;
            NotifyStateChanged();
        }

        public void ClearSearchByDeliveryArea()
        {
            DeliveryAreaId = Guid.Empty;
            NotifyStateChanged();
        }

        public void SetSearchByStatus(int? statusKey)
        {
            IsStatus = true;
            StatusKey = statusKey;
            NotifyStateChanged();
        }

        public void ClearSearchByStatus()
        {
            IsStatus = false;
            StatusKey = null;
            NotifyStateChanged();
        }
    }

    public static class StatusExtensions
    {
        public static SearchOutParameters ExceptStatus(this SearchOutParameters searchParameters)
        {
            SearchOutParameters parameters = new()
            {
                IsStatus = false,
                StatusKey = searchParameters.StatusKey,
                IsBarcode = searchParameters.IsBarcode,
                IsForm = searchParameters.IsForm,
                SearchBarcode = searchParameters.SearchBarcode,
                SearchTerm = searchParameters.SearchTerm,
                WarehouseId = searchParameters.WarehouseId,
                DeliveryAreaId = searchParameters.DeliveryAreaId,
            };
            return parameters;
        }
    }
}
