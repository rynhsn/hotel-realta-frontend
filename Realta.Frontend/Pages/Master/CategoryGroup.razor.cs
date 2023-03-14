using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.Contracts;
using Realta.Frontend.HttpRepository.Master.CategoryGroup;

namespace Realta.Frontend.Pages.Master
{
    public partial class CategoryGroup 
     {
            [Inject]
            public ICategoryGroupHttpRepository CategoryGroupRepository { get; set; }
            public List<CategoryGroupDto> CategoryGroupList { get; set; } = new List<CategoryGroupDto>();

        protected async override Task OnInitializedAsync()
        {
            CategoryGroupList = await CategoryGroupRepository.GetCategoryGroup();
        }

    }
}
