using Microsoft.AspNetCore.Components;

namespace Realta.Frontend.Components;

public partial class DeleteModal
{
    [Parameter] public EventCallback<string> OnDeletedData { get; set; }
    private string _id { get; set; }
    private string _state;
    private string _display;
    private string _message = "Data will be deleted!";

    public void Show(string id, string msg)
    {
        _message = msg;
        _id = id;
        _state = "show";        
        _display = "block;";

    }
  
    public void Hide()
    {
        _state = "";
        _display = "none;";
    }
  
    public async Task OnDeleteConfirmed(string id)
    {
        await OnDeletedData.InvokeAsync(id);
        Hide();
    }   
}