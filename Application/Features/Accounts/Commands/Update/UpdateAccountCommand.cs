using Application.Features.Accounts.Rules;
using Application.Services.Repositories;
using Application.Services.UserService;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Accounts.Commands.Update;

public class UpdateAccountCommand : IRequest<UpdateAccountResponse>, ICacheRemoverRequest, ILoggableRequest
{
    public int Id { get; set; }
    public int AccountType { get; set; }
    public string Password { set; get; }
    public double Balance { get; set; }
    public string Bank { get; set; }
    public int UserId { get; set; }

    public string? CacheKey => "UpdateAccount";
    public bool BypassCache => false;
    public string? CacheGroupKey => "GetAccounts";

    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, UpdateAccountResponse>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly AccountBusinessRules _accountBusinessRules;
        private readonly IUserService _userService;

        public UpdateAccountCommandHandler(IAccountRepository accountRepository, IMapper mapper, AccountBusinessRules accountBusinessRules, IUserService userService)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _accountBusinessRules = accountBusinessRules;
            _userService = userService;
        }

        public async Task<UpdateAccountResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            await _accountBusinessRules.AccountMustBePresent(request.Id);
            await _userService.CheckUserExistById(request.UserId);

            Account? account = await _accountRepository.GetAsync(predicate: account => account.Id == request.Id, cancellationToken: cancellationToken);
            account = _mapper.Map(request, account);

            await _accountRepository.UpdateAsync(account);
            UpdateAccountResponse response = _mapper.Map<UpdateAccountResponse>(account);

            return response;
        }
    }
}
