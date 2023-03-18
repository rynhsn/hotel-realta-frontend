using Microsoft.AspNetCore.Components;

namespace Realta.Frontend.Components;

public partial class ErrorNotifModal
{
    private string _modalClass;
    private string _modalDisplay;
    private string _message = "Error data missing value!";

    public void Show(string msg)
    {
        _message = msg;
        _modalClass = "show";
        _modalDisplay = "block;";
        StateHasChanged();
    }
  
    public void Hide()
    {
        _modalClass = "";
        _modalDisplay = "none;";
        StateHasChanged();
    }
}