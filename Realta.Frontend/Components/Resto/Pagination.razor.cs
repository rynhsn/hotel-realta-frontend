using Microsoft.AspNetCore.Components;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.Components.Resto
{
   public partial class Pagination
    {

        [Parameter]
        public MetaData MetaData { get; set; }

        [Parameter]
        public int Spread { get; set; }

        [Parameter]
        public EventCallback<int> SelectedPage { get; set; }

        private List<PagingLink> _links;

        protected override void OnParametersSet()
        {
            CreatePaginationLinks();
        }

        private void CreatePaginationLinks()
        {
            _links = new List<PagingLink>();

            if (MetaData.CurrentPage > 1)
            {
                _links.Add(new PagingLink(MetaData.CurrentPage - 1, MetaData.HasPrevious, "<"));
            }

            int maxPageLinks = 5; // jumlah maksimum tautan halaman yang ingin ditampilkan
            int startPage = MetaData.CurrentPage - (maxPageLinks / 2);
            int endPage = startPage + maxPageLinks - 1;

            if (startPage < 1)
            {
                endPage += Math.Abs(startPage) + 1;
                startPage = 1;
            }

            if (endPage > MetaData.TotalPages)
            {
                startPage -= (endPage - MetaData.TotalPages);
                endPage = MetaData.TotalPages;
            }

            for (int i = 1; i <= endPage; i++) // dimulai dari 1
            {
                if (i >= startPage)
                {
                    _links.Add(new PagingLink(i, true, i.ToString()) { Active = MetaData.CurrentPage == i });
                }
            }

            if (MetaData.CurrentPage < MetaData.TotalPages)
            {
                _links.Add(new PagingLink(MetaData.CurrentPage + 1, MetaData.HasNext, ">"));
            }



        }

        private async Task OnSelectedPage(PagingLink link)
        {
            if (link.Page == MetaData.CurrentPage || !link.Enabled)
                return;

            MetaData.CurrentPage = link.Page;
            await SelectedPage.InvokeAsync(link.Page);
        }
    }
}
