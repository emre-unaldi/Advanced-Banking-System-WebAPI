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

namespace Application.Features.Activities.Commands.DepositMoney;

public class DepositMoneyActivitiesCommand : IRequest<DepositMoneyActivitiesResponse>, ITransactionalRequest, ICacheRemoverRequest, ILoggableRequest
{
    public string AccountNumber { get; set; }
    public double Amount { get; set; }
    public int UserId { get; set; }

    public string? CacheKey => $"DepositMoneyActivities({UserId})";
    public bool BypassCache => false;
    public string? CacheGroupKey => "GetActivities";

    public class DepositMoneyTransactionsCommandHandler : IRequestHandler<DepositMoneyActivitiesCommand, DepositMoneyActivitiesResponse>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private readonly ActivityBusinessRules _activityBusinessRules;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public DepositMoneyTransactionsCommandHandler(IActivityRepository activityRepository, IMapper mapper, ActivityBusinessRules activityBusinessRules, IAccountService accountService, IUserService userService)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
            _activityBusinessRules = activityBusinessRules;
            _accountService = accountService;
            _userService = userService;
        }

        public async Task<DepositMoneyActivitiesResponse> Handle(DepositMoneyActivitiesCommand request, CancellationToken cancellationToken)
        {
            await _userService.CheckUserExistById(request.UserId);

            await _accountService.DepositMoney(request.AccountNumber, request.Amount);

            Activity activity = _mapper.Map<Activity>(request);
            activity.TransactionType = TransactionType.DepositMoney;

            await _activityRepository.AddAsync(activity);
            DepositMoneyActivitiesResponse response = _mapper.Map<DepositMoneyActivitiesResponse>(activity);

            return response;
        }
    }
}
