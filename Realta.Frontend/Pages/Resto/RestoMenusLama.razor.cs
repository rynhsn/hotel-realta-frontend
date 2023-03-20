﻿using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Frontend.HttpRepository;
using Realta.Frontend.HttpRepository.Resto;

namespace Realta.Frontend.Pages.Resto
{
    public partial class RestoMenusLama
    {
        private int userid = 1;
        [Inject]
        private IOrmeDetailHttpRepository OrmeRepo { get; set; }
        private List<OrderMenusDto> OrmeList { get; set; } = new List<OrderMenusDto>();
        private OrderMenusDto _toGet = new();
        protected async override Task OnInitializedAsync()
        {
            OrmeList = await OrmeRepo.GetOrderMenus();

            OrmeList = await OrmeRepo.GetOrderMenus();
            _toGet = OrmeList.LastOrDefault(r => r.OrmeUserId == userid && r.OrmeStatus == "Ordered");
            Console.WriteLine(_toGet.OrmeCardnumber);
        }


    }

}

