using Microsoft.AspNetCore.Components;
using Realta.Contract.Models;
using Realta.Domain.Dto;
using Realta.Domain.Entities;
using Realta.Frontend.HttpRepository.Resto;

namespace Realta.Frontend.Pages.Resto
{
    public partial class CompleteOrder
    {

        private int userid = 1;
        [Inject]
        public IOrmeDetailHttpRepository OrmeRepo { get; set; }
        public List<OrderMenusDto> OrmeList { get; set; } = new List<OrderMenusDto>();
        private OrderMenusDto _toUpdate = new();
        private OrderMenusDto _toGet = new();
        public string Ormetype { get; set; } = "B";
        public string OrmeCardNumb { get; set; }
        protected async override Task OnInitializedAsync()
        {
            OrmeList = await OrmeRepo.GetOrderMenus();
            _toUpdate = OrmeList.FirstOrDefault(r => r.OrmeUserId == userid && r.OrmeStatus == "Open");
          
        }

        //public Cart? Cart { get; set; }

        public async Task OnUpdate()
        {


            Console.WriteLine(_toUpdate.OrmeCardnumber);
            Console.WriteLine(_toUpdate.OrmeTotalItem);
            _toUpdate.OrmeStatus = "Ordered";
            _toUpdate.OrmePayType = Ormetype;
            _toUpdate.OrmeCardnumber = OrmeCardNumb;
            _toUpdate.OrmeIsPaid = "P";

            await OrmeRepo.UpdateDetail(_toUpdate);



            //foreach(var i in Cart.Instance.Items)
            //{


            //    _tocreate.OmdeRemeId = i.RestoMenusOrder.RemeId;
            //    _tocreate.OrmePrice = i.RestoMenusOrder.RemePrice;
            //    _tocreate.OrmeQty = (short)i.Quantity;
            //    _tocreate.OrmeDiscount =  0;
            //    _tocreate.OrmePayType = "DA";
            //    _tocreate.OrmeCardnumber = "sdfsdf";
            //    _tocreate.OrmeIsPaid = "P";
            //    _tocreate.OrmeUserId = 1;
            //    _tocreate.OrmeStatus = "Open";
            //    Console.WriteLine(i.RestoMenusOrder.RemePrice);

            //    Console.WriteLine(_tocreate.OrmeCardnumber);
            //await OrmeRepo.CreateDetail(_tocreate);
            //}
            //IEnumerator<int> o = null;
            // @omde_reme_id = 58
            //,@orme_price = 20000
            //,@orme_qty = 3
            //,@orme_discount = 5000
            //,@orme_pay_type = 'CA'
            //,@orme_cardnumber = 'kjk'
            //,@orme_is_paid = 'P'
            //,@orme_user_id = 1
            //,@orme_status = 'Ordered'
            //return o;

        }


    }


}
