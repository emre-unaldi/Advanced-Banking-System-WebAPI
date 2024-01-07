using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries.GetList;

public class GetListUserQuery : IRequest<GetListResponse<GetListUserListItemResponse>>, ICachableRequest, ILoggableRequest
{
    public PageRequest PageRequest { get; set; }

    public string CacheKey => $"GetListUserQuery({PageRequest.PageIndex},{PageRequest.PageSize})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "GetUsers";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, GetListResponse<GetListUserListItemResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetListUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListUserListItemResponse>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            Paginate<User> users = await _userRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    include: user => user.Include(user => user.Accounts),
                    cancellationToken: cancellationToken
                );

            GetListResponse<GetListUserListItemResponse> response = _mapper.Map<GetListResponse<GetListUserListItemResponse>>(users);

            return response;
        }
    }
}
