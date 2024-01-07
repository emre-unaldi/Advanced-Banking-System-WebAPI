using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Accounts.Queries.GetList;

public class GetListAccountQuery : IRequest<GetListResponse<GetListAccountListItemResponse>>, ICachableRequest, ILoggableRequest
{
    public PageRequest PageRequest { get; set; }

    public string CacheKey => $"GetListAccountQuery({PageRequest.PageIndex},{PageRequest.PageSize})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "GetAccounts";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListAccountQueryHandler : IRequestHandler<GetListAccountQuery, GetListResponse<GetListAccountListItemResponse>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetListAccountQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAccountListItemResponse>> Handle(GetListAccountQuery request, CancellationToken cancellationToken)
        {
            Paginate<Account> accounts = await _accountRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    include: account => account.Include(account => account.User),
                    cancellationToken: cancellationToken
                );
            GetListResponse<GetListAccountListItemResponse> response = _mapper.Map<GetListResponse<GetListAccountListItemResponse>>(accounts);

            return response;
        }
    }
}