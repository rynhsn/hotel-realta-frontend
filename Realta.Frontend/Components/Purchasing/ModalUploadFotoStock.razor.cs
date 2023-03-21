using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.RequestFeatures;
using Realta.Frontend.HttpRepository.Purchasing;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Components.Purchasing
{
    public partial class ModalUploadFotoStock
    {
        private string _modalDisplay;
        private string _modalClass;

        private List<string> _srcImg ;
        private bool _showBackdrop;
        [Parameter] public EventCallback getPaging { get; set; }
        //public List<StockPhotoDto> PhotoList { get; set; } = new List<StockPhotoDto>();

        private SuccessNotification _notification;

        [Inject]
        public IStocksHttpRepository StocksHttpRepository { get; set; }

        public async Task Show(List<StockPhotoDto>? stockPhotoDtoList)
        {
            _modalDisplay = "block;";
            _modalClass = "show";
            _showBackdrop = true;
            _srcImg = new List<string>(4);

            if (stockPhotoDtoList.Count > 0)
            {
                foreach (var item in stockPhotoDtoList)
                {
                    _srcImg.Add(item.SphoPhotoFileName);
                }
            }

            for (int i = 0; i < (4 - stockPhotoDtoList.Count); i++)
            {
                _srcImg.Add("defaultImage.jpg");
            }

            StateHasChanged();
        }

        private void Hide()
        {
            _modalDisplay = "none;";
            _modalClass = "";
            _showBackdrop = false;
            StateHasChanged();
        }

    }
}
