using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserQuery : IRequest<GetByIdUserResponse>, ICachableRequest, ILoggableRequest
{
    public int Id { get; set; }

    public string CacheKey => $"GetByIdUserQuery({Id})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "GetUsers";
    public TimeSpan? SlidingExpiration { get; }

    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, GetByIdUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public GetByIdUserQueryHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }
        public async Task<GetByIdUserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserMustBePresent(request.Id);

            User? user = await _userRepository.GetAsync(
                    predicate: user => user.Id == request.Id, 
                    include: user => user.Include(user => user.Accounts),
                    cancellationToken: cancellationToken
                );
            GetByIdUserResponse response = _mapper.Map<GetByIdUserResponse>(user);

            return response;
        }
    }
}
