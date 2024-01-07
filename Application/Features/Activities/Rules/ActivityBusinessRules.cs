using Application.Features.Accounts.Constants;
using Application.Features.Activities.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Activities.Rules;

public class ActivityBusinessRules : BaseBusinessRules
{
    private readonly IActivityRepository _activityRepository;

    public ActivityBusinessRules(IActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }

    public async Task ActivityMustBePresent(int id)
    {
        Activity? activity = await _activityRepository.GetAsync(predicate: account => account.Id == id);

        if (activity is null)
            throw new BusinessException(ActivitiesMessages.ActivityNotFound);
    }
}
