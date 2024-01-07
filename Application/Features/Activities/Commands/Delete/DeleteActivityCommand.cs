using Application.Features.Activities.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Activities.Commands.Delete;

public class DeleteActivityCommand : IRequest<DeleteActivityResponse>, ICacheRemoverRequest, ILoggableRequest
{
    public int Id { get; set; }

    public string? CacheKey => $"DeleteActivity({Id})";
    public bool BypassCache => false;
    public string? CacheGroupKey => "GetActivities";

    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, DeleteActivityResponse>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private readonly ActivityBusinessRules _activityBusinessRules;

        public DeleteActivityCommandHandler(IActivityRepository activityRepository, IMapper mapper, ActivityBusinessRules activityBusinessRules)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
            _activityBusinessRules = activityBusinessRules;
        }

        public async Task<DeleteActivityResponse> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            await _activityBusinessRules.ActivityMustBePresent(request.Id);

            Activity? activity = await _activityRepository.GetAsync(predicate: activity => activity.Id == request.Id, cancellationToken: cancellationToken);
            await _activityRepository.DeleteAsync(activity);
            DeleteActivityResponse response = _mapper.Map<DeleteActivityResponse>(activity);

            return response;
        }
    }
}
