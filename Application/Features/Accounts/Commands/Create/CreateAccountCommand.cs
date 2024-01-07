using Application.Features.Accounts.Rules;
using Application.Services.Repositories;
using Application.Services.UserService;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Accounts.Commands.Create;

public class CreateAccountCommand : IRequest<CreateAccountResponse>, ICacheRemoverRequest, ILoggableRequest
{
    public int AccountType { get; set; }
    public string Password { set; get; }
    public double Balance { get; set; }
    public string Bank { get; set; }
    public int UserId { get; set; }

    public string? CacheKey => "CreateAccount";
    public bool BypassCache => false;
    public string? CacheGroupKey => "GetAccounts";

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountResponse>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly AccountBusinessRules _accountBusinessRules;
        private readonly IUserService _userService;

        public CreateAccountCommandHandler(IAccountRepository accountRepository, IMapper mapper, AccountBusinessRules accountBusinessRules, IUserService userService)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _accountBusinessRules = accountBusinessRules;
            _userService = userService;
        }

        public async Task<CreateAccountResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            await _userService.CheckUserExistById(request.UserId);

            Account account = _mapper.Map<Account>(request);
            account.AccountNumber = Guid.NewGuid().ToString("D");

            await _accountRepository.AddAsync(account);
            CreateAccountResponse response = _mapper.Map<CreateAccountResponse>(account);

            return response;
        }
    }
}
