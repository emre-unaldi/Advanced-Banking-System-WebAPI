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

namespace Application.Features.Credits.Queries.GetList;

public class GetListCreditQuery : IRequest<GetListResponse<GetListCreditListItemResponse>>, ICachableRequest, ILoggableRequest
{
    public PageRequest PageRequest { get; set; }

    public string CacheKey => $"GetListCreditQuery({PageRequest.PageIndex},{PageRequest.PageSize})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "GetCredits";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListCreditQueryHandler : IRequestHandler<GetListCreditQuery, GetListResponse<GetListCreditListItemResponse>>
    {
        private readonly ICreditRepository _creditRepository;
        private readonly IMapper _mapper;

        public GetListCreditQueryHandler(ICreditRepository creditRepository, IMapper mapper)
        {
            _creditRepository = creditRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCreditListItemResponse>> Handle(GetListCreditQuery request, CancellationToken cancellationToken)
        {
            Paginate<Credit> credits = await _creditRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    include: credit => credit.Include(credit => credit.User),
                    cancellationToken: cancellationToken
                );

            GetListResponse<GetListCreditListItemResponse> response = _mapper.Map<GetListResponse<GetListCreditListItemResponse>>(credits);

            return response;
        }
    }
}
