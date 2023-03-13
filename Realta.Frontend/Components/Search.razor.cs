using Microsoft.AspNetCore.Components;

namespace Realta.Frontend.Components
{
    public partial class Search
    {
        public string SearchTerm;

        [Parameter] public EventCallback<string> OnSearchChanged { get; set; }

    }
}
