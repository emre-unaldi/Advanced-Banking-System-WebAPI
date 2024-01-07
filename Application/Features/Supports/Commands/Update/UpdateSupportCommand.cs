using Application.Features.Supports.Rules;
using Application.Services.Repositories;
using Application.Services.UserService;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Supports.Commands.Update;

public class UpdateSupportCommand : IRequest<UpdateSupportResponse>, ICacheRemoverRequest, ILoggableRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }

    public string? CacheKey => "UpdateSupport";
    public bool BypassCache => false;
    public string? CacheGroupKey => "GetSupports";

    public class UpdateSupportCommandHandler : IRequestHandler<UpdateSupportCommand, UpdateSupportResponse>
    {
        private readonly ISupportRepository _supportRepository;
        private readonly IMapper _mapper;
        private readonly SupportBusinessRules _supportBusinessRules;
        private readonly IUserService _userService;

        public UpdateSupportCommandHandler(ISupportRepository supportRepository, IMapper mapper, SupportBusinessRules supportBusinessRules, IUserService userService)
        {
            _supportRepository = supportRepository;
            _mapper = mapper;
            _supportBusinessRules = supportBusinessRules;
            _userService = userService;
        }

        public async Task<UpdateSupportResponse> Handle(UpdateSupportCommand request, CancellationToken cancellationToken)
        {
            await _userService.CheckUserExistById(request.UserId);

            await _supportBusinessRules.SupportMustBePresent(request.Id);
            await _supportBusinessRules.SupportTitleCannotBeDuplicated(request.Title);

            Support? support = await _supportRepository.GetAsync(predicate: support => support.Id == request.Id, cancellationToken: cancellationToken);
            support = _mapper.Map(request, support);

            await _supportRepository.UpdateAsync(support);
            UpdateSupportResponse response = _mapper.Map<UpdateSupportResponse>(support);

            return response;
        }
    }
}
