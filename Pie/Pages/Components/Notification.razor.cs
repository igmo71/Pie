﻿using Microsoft.AspNetCore.Components;

namespace Pie.Pages.Components
{
    public partial class Notification
    {
        [Parameter]
        public string? Message { get; set; }

        [Parameter]
        public RenderFragment? RenderFragment { get; set; }

        private string? _modalDisplay;
        private string? _modalClass;
        private bool _showBackdrop;

        public void Show(string message)
        {
            Message = message;

            _modalDisplay = "block;";
            _modalClass = "show";
            _showBackdrop = true;
            StateHasChanged();
        }

        public void Hide()
        {
            _modalDisplay = "none;";
            _modalClass = "";
            _showBackdrop = false;
            StateHasChanged();
        }

        public async Task ShowAndHideAsync(string message, int delay)
        {
            Show(message);
            await Task.Delay(1000 * delay);
            Hide();
        }

        public async Task SetMessageAndHideAsync(string message, int delay)
        {
            Message = message;
            StateHasChanged();
            await Task.Delay(1000 * delay);
            Hide();
        }
    }
}
