using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.Contracts;
using Realta.Domain.RequestFeatures;
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
            //CategoryGroupList = await CategoryGroupRepository.GetCategoryGroup();
            await GetPaging();
        }

        private CategoryGroupParameter _categoryGroupParameter = new CategoryGroupParameter();
        private MetaData MetaData { get; set; } = new MetaData();

        private async Task SelectedPage(int page)
        {
            _categoryGroupParameter.PageNumber = page;
            await GetPaging();
        }

        private async Task GetPaging()
        {
            var response = await CategoryGroupRepository.GetCategoryGroupPaging(_categoryGroupParameter);
            CategoryGroupList = response.Items;
            MetaData = response.MetaData;
        }

        private async Task SearchChange(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _categoryGroupParameter.PageNumber = 1;
            _categoryGroupParameter.SearchTerm = searchTerm;
            await GetPaging();
        }
    }
}
