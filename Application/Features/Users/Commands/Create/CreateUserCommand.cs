using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommand : IRequest<CreateUserResponse>, ICacheRemoverRequest, ILoggableRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string IdentityNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public int FindexScore { get; set; }

    public string? CacheKey => "CreateUser";
    public bool BypassCache => false;
    public string? CacheGroupKey => "GetUsers";

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserEmailCannotBeDuplicated(request.Email);
            await _userBusinessRules.UserPhoneNumberCannotBeDuplicated(request.PhoneNumber);
            await _userBusinessRules.UserIdentityNumberCannotBeDuplicated(request.IdentityNumber);

            User user = _mapper.Map<User>(request);
            await _userRepository.AddAsync(user);
            CreateUserResponse response = _mapper.Map<CreateUserResponse>(user);

            return response;
        }
    }
}
