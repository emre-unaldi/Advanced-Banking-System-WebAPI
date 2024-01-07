using Application.Features.Activities.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Activities.Queries.GetById;

public class GetByIdActivityQuery : IRequest<GetByIdActivityResponse>, ICachableRequest, ILoggableRequest
{
    public int Id { get; set; }

    public string CacheKey => $"GetByIdActivityQuery({Id})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "GetActivities";
    public TimeSpan? SlidingExpiration { get; }

    public class GetByIdActivityQueryHandler : IRequestHandler<GetByIdActivityQuery, GetByIdActivityResponse>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private readonly ActivityBusinessRules _activityBusinessRules;

        public GetByIdActivityQueryHandler(IActivityRepository activityRepository, IMapper mapper, ActivityBusinessRules activityBusinessRules)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
            _activityBusinessRules = activityBusinessRules;
        }

        public async Task<GetByIdActivityResponse> Handle(GetByIdActivityQuery request, CancellationToken cancellationToken)
        {
            await _activityBusinessRules.ActivityMustBePresent(request.Id);

            Activity? activity = await _activityRepository.GetAsync(
                    predicate: activity => activity.Id == request.Id,
                    include: activity => activity.Include(activity => activity.User),
                    cancellationToken: cancellationToken
                );

            GetByIdActivityResponse response = _mapper.Map<GetByIdActivityResponse>(activity);

            return response;
        }
    }
}
