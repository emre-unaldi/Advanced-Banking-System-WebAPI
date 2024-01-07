using Application.Services.Repositories;
using Application.Services.UserService;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Credits.Commands.Application;

public class ApplicationCreditCommand : IRequest<ApplicationCreditResponse>, ICacheRemoverRequest, ILoggableRequest
{
    public string Name { get; set; }
    public double RequestedLoanAmount { get; set; }
    public double TotalPaymentAmount { get; set; }
    public double MonthlyPaymentAmount { get; set; }
    public string ReferredBank { get; set; }
    public short MonthlyPaymentDate { get; set; }
    public int UserId { get; set; }

    public string? CacheKey => "ApplicationCredit";
    public bool BypassCache => false;
    public string? CacheGroupKey => "GetCredits";

    public class ApplicationCreditCommandHandler : IRequestHandler<ApplicationCreditCommand, ApplicationCreditResponse>
    {
        private readonly ICreditRepository _creditRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ApplicationCreditCommandHandler(ICreditRepository creditRepository, IMapper mapper, IUserService userService)
        {
            _creditRepository = creditRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ApplicationCreditResponse> Handle(ApplicationCreditCommand request, CancellationToken cancellationToken)
        {
            await _userService.CheckUserExistById(request.UserId);

            Credit credit = _mapper.Map<Credit>(request);
            credit.ApprovalStatus = false;

            await _creditRepository.AddAsync(credit);
            ApplicationCreditResponse response = _mapper.Map<ApplicationCreditResponse>(credit);

            return response;
        }
    }
}
