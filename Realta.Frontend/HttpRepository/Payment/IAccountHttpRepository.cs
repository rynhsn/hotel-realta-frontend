﻿using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.Dto;
using HotelRealtaPayment.Domain.RequestFeatures;
using Realta.Frontend.Features;

namespace Realta.Frontend.HttpRepository.Payment;

public interface IAccountHttpRepository
{
    public Task<List<AccountDto>> GetAccounts();
    public Task<List<AccountUser>> GetAccountInfo(int id);
    Task<HttpResponseMessage> TopUpFintech(TransactionTopUpDto topUp);
    Task<PagingResponse<AccountDto>> GetAccountsPaging(AccountParameters accountParameters);
}