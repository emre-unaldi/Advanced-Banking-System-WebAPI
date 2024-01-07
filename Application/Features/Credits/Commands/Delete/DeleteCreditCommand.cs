using Application.Features.Credits.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Credits.Commands.Delete;

public class DeleteCreditCommand : IRequest<DeleteCreditResponse>, ICacheRemoverRequest, ILoggableRequest
{
    public int Id { get; set; }

    public string? CacheKey => "DeleteCredit";
    public bool BypassCache => false;
    public string? CacheGroupKey => "GetCredits";

    public class DeleteCreditCommandHandler : IRequestHandler<DeleteCreditCommand, DeleteCreditResponse>
    {
        private readonly ICreditRepository _creditRepository;
        private readonly IMapper _mapper;
        private readonly CreditBusinessRules _creditBusinessRules;

        public DeleteCreditCommandHandler(ICreditRepository creditRepository, IMapper mapper, CreditBusinessRules creditBusinessRules)
        {
            _creditRepository = creditRepository;
            _mapper = mapper;
            _creditBusinessRules = creditBusinessRules;
        }

        public async Task<DeleteCreditResponse> Handle(DeleteCreditCommand request, CancellationToken cancellationToken)
        {
            await _creditBusinessRules.CreditMustBePresent(request.Id);

            Credit? credit = await _creditRepository.GetAsync(predicate: credit => credit.Id == request.Id, cancellationToken: cancellationToken);
            await _creditRepository.DeleteAsync(credit);
            DeleteCreditResponse response = _mapper.Map<DeleteCreditResponse>(credit);

            return response;
        }
    }
}
