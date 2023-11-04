using Microsoft.AspNetCore.Components;

namespace RecipeCookingBook.Client.Components.Search;

public partial class Search
{
    private Timer _timer;

    public string? SearchTerm { get; set; } = String.Empty;

    [Parameter] public EventCallback<string> OnSearchChanged { get; set; }
    
    private void SearchWhileIdle()
    {
        if (_timer != null)
            _timer.Dispose();
        _timer = new Timer(OnTimerElapsed, null, 500, 0);
    }
    private void OnTimerElapsed(object sender)
    {
        OnSearchChanged.InvokeAsync(SearchTerm);
        _timer.Dispose();
    }
}