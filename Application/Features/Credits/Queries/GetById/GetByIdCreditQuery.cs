using Application.Features.Credits.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Credits.Queries.GetById;

public class GetByIdCreditQuery : IRequest<GetByIdCreditResponse>, ICachableRequest, ILoggableRequest
{
    public int Id { get; set; }

    public string CacheKey => $"GetByIdCreditQuery({Id})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "GetCredits";
    public TimeSpan? SlidingExpiration { get; }

    public class GetByIdCreditQueryHandler : IRequestHandler<GetByIdCreditQuery, GetByIdCreditResponse>
    {
        private readonly ICreditRepository _creditRepository;
        private readonly IMapper _mapper;
        private readonly CreditBusinessRules _creditBusinessRules;

        public GetByIdCreditQueryHandler(ICreditRepository creditRepository, IMapper mapper, CreditBusinessRules creditBusinessRules)
        {
            _creditRepository = creditRepository;
            _mapper = mapper;
            _creditBusinessRules = creditBusinessRules;
        }

        public async Task<GetByIdCreditResponse> Handle(GetByIdCreditQuery request, CancellationToken cancellationToken)
        {
            await _creditBusinessRules.CreditMustBePresent(request.Id);

            Credit? credit = await _creditRepository.GetAsync(
                    predicate: credit => credit.Id == request.Id,
                    include: credit => credit.Include(credit => credit.User),
                    cancellationToken: cancellationToken
                );
            GetByIdCreditResponse response = _mapper.Map<GetByIdCreditResponse>(credit);

            return response;
        }
    }
}
