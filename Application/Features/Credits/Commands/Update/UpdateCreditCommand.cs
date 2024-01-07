using Application.Features.Credits.Commands.Approval;
using Application.Features.Credits.Rules;
using Application.Services.Repositories;
using Application.Services.UserService;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Credits.Commands.Update;

public class UpdateCreditCommand : IRequest<UpdateCreditResponse>, ICacheRemoverRequest, ILoggableRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double RequestedLoanAmount { get; set; }
    public double TotalPaymentAmount { get; set; }
    public double MonthlyPaymentAmount { get; set; }
    public string ReferredBank { get; set; }
    public short MonthlyPaymentDate { get; set; }
    public bool ApprovalStatus { get; set; }
    public int UserId { get; set; }

    public string? CacheKey => "UpdateCredit";
    public bool BypassCache => false;
    public string? CacheGroupKey => "GetCredits";

    public class UpdateCreditCommandHandler : IRequestHandler<UpdateCreditCommand, UpdateCreditResponse>
    {
        private readonly ICreditRepository _creditRepository;
        private readonly IMapper _mapper;
        private readonly CreditBusinessRules _creditBusinessRules;
        private readonly IUserService _userService;

        public UpdateCreditCommandHandler(ICreditRepository creditRepository, IMapper mapper, CreditBusinessRules creditBusinessRules, IUserService userService)
        {
            _creditRepository = creditRepository;
            _mapper = mapper;
            _creditBusinessRules = creditBusinessRules;
            _userService = userService;
        }
        public async Task<UpdateCreditResponse> Handle(UpdateCreditCommand request, CancellationToken cancellationToken)
        {
            await _userService.CheckUserExistById(request.UserId);

            await _creditBusinessRules.CreditMustBePresent(request.Id);

            Credit? credit = await _creditRepository.GetAsync(predicate: credit => credit.Id == request.Id, cancellationToken: cancellationToken);
            credit = _mapper.Map(request, credit);

            await _creditRepository.UpdateAsync(credit);
            UpdateCreditResponse response = _mapper.Map<UpdateCreditResponse>(credit);

            return response;
        }
    }
}
