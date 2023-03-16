using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository.Purchasing;
using Realta.Frontend.Shared;

namespace Realta.Frontend.Components.Purchasing
{
    public partial class ModalCreateStock
    {
        public StocksDto _createStock { get; set; } = new StocksDto();

        private SuccessNotification _notification;
        private bool isDisabled = true;

        [Inject]
        public IStocksHttpRepository StocksHttpRepository { get; set; }


        private async Task Create()
        {
            await StocksHttpRepository.CreateStock(_createStock);
            _notification.Show("/purchasing/stocks");
        }

    }
}
