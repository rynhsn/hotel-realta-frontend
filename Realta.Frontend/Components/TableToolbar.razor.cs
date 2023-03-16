using Microsoft.AspNetCore.Components;

namespace Realta.Frontend.Components;

public partial class TableToolbar
{
    private Timer _timer;
    private int _pageSize;

    [Parameter] public EventCallback<string> OnSearchChanged { get; set; }
    [Parameter] public EventCallback<int> OnPageSizeChanged { get; set; }
    [Parameter] public string Placeholder { get; set; }
    
    private void SearchChanged(ChangeEventArgs e)
    {
        if (_timer != null)
            _timer.Dispose();
        _timer = new Timer(OnTimerElapsed, e.Value, 500, 0);
    }
    
    private void OnTimerElapsed(object? sender)
    {
        OnSearchChanged.InvokeAsync(sender.ToString());
        _timer.Dispose();
    }

    private async void PageSizeChanged(ChangeEventArgs e)
    {
        _pageSize = Int32.Parse(e.Value?.ToString() ?? string.Empty);
        await OnPageSizeChanged.InvokeAsync(_pageSize);
    }

}