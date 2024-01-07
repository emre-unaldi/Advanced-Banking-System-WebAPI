using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands.Delete;

public class DeleteUserCommand : IRequest<DeleteUserResponse>, ICacheRemoverRequest, ILoggableRequest
{
    public int Id { get; set; }

    public string? CacheKey => "DeleteUser";
    public bool BypassCache => false;
    public string? CacheGroupKey => "GetUsers";

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public DeleteUserCommandHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserMustBePresent(request.Id);

            User? user = await _userRepository.GetAsync(predicate: user => user.Id == request.Id, cancellationToken: cancellationToken);
            await _userRepository.DeleteAsync(user);
            DeleteUserResponse response = _mapper.Map<DeleteUserResponse>(user);

            return response;
        }
    }
}
