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

namespace Application.Features.Activities.Queries.GetList;

public class GetListActivityQuery : IRequest<GetListResponse<GetListActivityListItemResponse>>, ICachableRequest, ILoggableRequest
{
    public PageRequest PageRequest { get; set; }

    public string CacheKey => $"GetListActivityQuery({PageRequest.PageIndex},{PageRequest.PageSize})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "GetActivities";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListActivityQueryHandler : IRequestHandler<GetListActivityQuery, GetListResponse<GetListActivityListItemResponse>>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;

        public GetListActivityQueryHandler(IActivityRepository activityRepository, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListActivityListItemResponse>> Handle(GetListActivityQuery request, CancellationToken cancellationToken)
        {
            Paginate<Activity> activities = await _activityRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    include: activity => activity.Include(activity => activity.User),
                    cancellationToken: cancellationToken
                );

            GetListResponse<GetListActivityListItemResponse> response = _mapper.Map<GetListResponse<GetListActivityListItemResponse>>(activities);

            return response;
        }
    }
}
