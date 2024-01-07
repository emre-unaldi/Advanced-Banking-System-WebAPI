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

namespace Application.Features.Activities.Commands.MoneyTransfer;

public class MoneyTransferActivitiesCommand : IRequest<MoneyTransferActivitiesResponse>, ITransactionalRequest, ICacheRemoverRequest, ILoggableRequest
{
    public string AccountNumber { get; set; }
    public string TargetAccountNumber { get; set; }
    public double Amount { get; set; }
    public int UserId { get; set; }

    public string? CacheKey => $"MoneyTransferActivities({UserId})";
    public bool BypassCache => false;
    public string? CacheGroupKey => "GetActivities";

    public class MoneyTransferActivitiesCommandHandler : IRequestHandler<MoneyTransferActivitiesCommand, MoneyTransferActivitiesResponse>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public MoneyTransferActivitiesCommandHandler(IActivityRepository activityRepository, IMapper mapper, IAccountService accountService, IUserService userService)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
            _accountService = accountService;
            _userService = userService;
        }

        public async Task<MoneyTransferActivitiesResponse> Handle(MoneyTransferActivitiesCommand request, CancellationToken cancellationToken)
        {
            await _userService.CheckUserExistById(request.UserId);

            await _accountService.MoneyTransfer(request.AccountNumber, request.TargetAccountNumber, request.Amount);

            Activity activity = _mapper.Map<Activity>(request);
            activity.TransactionType = TransactionType.MoneyTransfer;

            await _activityRepository.AddAsync(activity);
            MoneyTransferActivitiesResponse response = _mapper.Map<MoneyTransferActivitiesResponse>(activity);

            return response;
        }
    }
}
