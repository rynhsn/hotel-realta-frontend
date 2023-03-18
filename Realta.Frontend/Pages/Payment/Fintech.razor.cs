using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.RequestFeatures;
using Microsoft.AspNetCore.Components;
using Realta.Frontend.Components;
using Realta.Frontend.HttpRepository.Payment;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Pages.Payment;

public partial class Fintech
{
    private FintechParameters _param = new();
    public MetaData MetaData { get; set; } = new();
    private FintechDto _fintech = new();
    private ModalDelete _del;
    private SuccessNotification _notif;

    [Inject]
    public IFintechHttpRepository FintechsRepo { get; set; }
    public List<FintechDto> FintechList { get; set; } = new List<FintechDto>();
   
    protected async override Task OnInitializedAsync() 
    {
        await FetchFintech();
    }
    
    private async Task FetchFintech()
    {
        var response = await FintechsRepo.GetFintechsPaging(_param);
        FintechList = response.Items;
        MetaData = response.MetaData;
    }
    
    private async Task SelectedPage(int page)
    {
        _param.PageNumber = page;
        await FetchFintech();
    }

    private async Task SearchChanged(string keyword)
    {
        _param.PageNumber = 1;
        _param.SearchTerm = keyword;
        await FetchFintech();
    }
    
    private async Task PageSizeChanged(int size)
    {
        _param.PageNumber = 1;
        _param.PageSize = size;
        await FetchFintech();
    }
    
    private async Task OnUpdate(FintechDto fintech)
    {
        _fintech.Id = fintech.Id;
        _fintech.Code = fintech.Code;
        _fintech.Name = fintech.Name;
    }
    
    private async Task onUpdateConfirmed()
    {
        await FintechsRepo.Update(_fintech);
        await FetchFintech();
    }
    
    private async Task onCreateConfirmed()
    {
        await FintechsRepo.Create(_fintech);
        await FetchFintech();
    } 

    
    private async Task OnDelete(int id)
    {
        _del.Show(id, $"Fintech {id} will be deleted!");
        await Task.Delay(100);
    }


    private async Task OnDeleteConfirmed(object id)
    {
        _del.Hide();
        await FintechsRepo.Delete((int)id);
        _param.PageNumber = 1;
        _notif.Show(NavigationManager.Uri);
        await FetchFintech();
    }
}