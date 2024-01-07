using Application.Features.Supports.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Supports.Queries.GetById;

public class GetByIdSupportQuery : IRequest<GetByIdSupportResponse>, ICachableRequest, ILoggableRequest
{
    public int Id { get; set; }

    public string CacheKey => $"GetByIdSupportQuery({Id})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "GetSupports";
    public TimeSpan? SlidingExpiration { get; }

    public class GetByIdSupportQueryHandler : IRequestHandler<GetByIdSupportQuery, GetByIdSupportResponse>
    {
        private readonly ISupportRepository _supportRepository;
        private readonly IMapper _mapper;
        private readonly SupportBusinessRules _supportBusinessRules;

        public GetByIdSupportQueryHandler(ISupportRepository supportRepository, IMapper mapper, SupportBusinessRules supportBusinessRules)
        {
            _supportRepository = supportRepository;
            _mapper = mapper;
            _supportBusinessRules = supportBusinessRules;
        }
        public async Task<GetByIdSupportResponse> Handle(GetByIdSupportQuery request, CancellationToken cancellationToken)
        {
            await _supportBusinessRules.SupportMustBePresent(request.Id);

            Support? support = await _supportRepository.GetAsync(
                    predicate: support => support.Id == request.Id, 
                    include: support => support.Include(support => support.User),
                    cancellationToken: cancellationToken
                );
            GetByIdSupportResponse response = _mapper.Map<GetByIdSupportResponse>(support);

            return response;
        }
    }
}
