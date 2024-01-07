using Application.Features.Activities.Rules;
using Application.Services.AccountService;
using Application.Services.Repositories;
using Application.Services.UserService;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Activities.Commands.WithdrawMoney;

public class WithdrawMoneyActivitiesCommand : IRequest<WithdrawMoneyActivitiesResponse>, ITransactionalRequest, ICacheRemoverRequest, ILoggableRequest
{
    public string AccountNumber { get; set; }
    public double Amount { get; set; }
    public int UserId { get; set; }

    public string? CacheKey => $"WithdrawMoneyActivities({UserId})";
    public bool BypassCache => false;
    public string? CacheGroupKey => "GetActivities";

    public class WithdrawMoneyActivitiesCommandHandler : IRequestHandler<WithdrawMoneyActivitiesCommand, WithdrawMoneyActivitiesResponse>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private readonly ActivityBusinessRules _activityBusinessRules;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public WithdrawMoneyActivitiesCommandHandler(IActivityRepository activityRepository, IMapper mapper, ActivityBusinessRules activityBusinessRules, IAccountService accountService, IUserService userService)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
            _activityBusinessRules = activityBusinessRules;
            _accountService = accountService;
            _userService = userService;
        }
        public async Task<WithdrawMoneyActivitiesResponse> Handle(WithdrawMoneyActivitiesCommand request, CancellationToken cancellationToken)
        {
            await _userService.CheckUserExistById(request.UserId);

            await _accountService.WithdrawMoney(request.AccountNumber, request.Amount);

            Activity activity = _mapper.Map<Activity>(request);
            activity.TransactionType = TransactionType.WithdrawMoney;

            await _activityRepository.AddAsync(activity);
            WithdrawMoneyActivitiesResponse response = _mapper.Map<WithdrawMoneyActivitiesResponse>(activity);

            return response;
        }
    }
}
