using Application.Features.Credits.Commands.Application;
using Application.Features.Credits.Rules;
using Application.Services.Repositories;
using Application.Services.UserService;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Credits.Commands.Approval;

public class ApprovalCreditCommand : IRequest<ApprovalCreditResponse>, ICacheRemoverRequest, ILoggableRequest
{
    public int Id { get; set; }

    public string? CacheKey => "ApprovalCredit";
    public bool BypassCache => false;
    public string? CacheGroupKey => "GetCredits";

    public class ApprovalCreditCommandHandler : IRequestHandler<ApprovalCreditCommand, ApprovalCreditResponse>
    {
        private readonly ICreditRepository _creditRepository;
        private readonly IMapper _mapper;
        private readonly CreditBusinessRules _creditBusinessRules;

        public ApprovalCreditCommandHandler(ICreditRepository creditRepository, IMapper mapper, CreditBusinessRules creditBusinessRules)
        {
            _creditRepository = creditRepository;
            _mapper = mapper;
            _creditBusinessRules = creditBusinessRules;
        }

        public async Task<ApprovalCreditResponse> Handle(ApprovalCreditCommand request, CancellationToken cancellationToken)
        {
            await _creditBusinessRules.CreditMustBePresent(request.Id);

            Credit? credit = await _creditRepository.GetAsync(predicate: credit => credit.Id == request.Id, cancellationToken: cancellationToken);
            credit.ApprovalStatus = true;

            await _creditRepository.UpdateAsync(credit);
            ApprovalCreditResponse response = _mapper.Map<ApprovalCreditResponse>(credit);

            return response;
        }
    }
}
