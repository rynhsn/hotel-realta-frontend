using Microsoft.AspNetCore.Components;

namespace Realta.Frontend.Shared;
public partial class SuccessNotification
{
    
    private string _modalDisplay;
    private string _modalClass;
    private bool _showBackdrop;
    private string _displayMessage;

    [Parameter]
    public string PathRoute { get; set; }

    [Inject]
    public NavigationManager Navigation { get; set; }

    public void Show(string path, string displayMessage)
    {
        _modalDisplay = "block;";
        _modalClass = "show";
        _showBackdrop = true;
        _displayMessage = displayMessage;
        PathRoute = path;
        StateHasChanged();
    }

    public void ShowWithoutPath()
    {
        _modalDisplay = "block;";
        _modalClass = "show";
        _showBackdrop = true;
        StateHasChanged();
    }

    private void Hide()
    {
        _modalDisplay = "none;";
        _modalClass = "";
        _showBackdrop = false;
        StateHasChanged();
        Navigation.NavigateTo(PathRoute);
    }
}

