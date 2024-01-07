using Application.Features.Activities.Commands.Delete;
using Application.Features.Activities.Commands.DepositMoney;
using Application.Features.Activities.Commands.MoneyTransfer;
using Application.Features.Activities.Commands.WithdrawMoney;
using Application.Features.Activities.Queries.GetById;
using Application.Features.Activities.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Activities.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Activity, DepositMoneyActivitiesCommand>().ReverseMap();
        CreateMap<Activity, DepositMoneyActivitiesResponse>()
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType.ToString()))
            .ReverseMap();

        CreateMap<Activity, WithdrawMoneyActivitiesCommand>().ReverseMap();
        CreateMap<Activity, WithdrawMoneyActivitiesResponse>()
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType.ToString()))
            .ReverseMap();

        CreateMap<Activity, MoneyTransferActivitiesCommand>().ReverseMap();
        CreateMap<Activity, MoneyTransferActivitiesResponse>()
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType.ToString()))
            .ReverseMap();

        CreateMap<Activity, DeleteActivityCommand>().ReverseMap();
        CreateMap<Activity, DeleteActivityResponse>().ReverseMap();

        CreateMap<Activity, GetByIdActivityResponse>()
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType.ToString()))
            .ReverseMap();

        CreateMap<Activity, GetListActivityListItemResponse>()
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType.ToString()))
            .ReverseMap();
        CreateMap<Paginate<Activity>, GetListResponse<GetListActivityListItemResponse>>().ReverseMap();

        CreateMap<User, GetByIdActivityUserResponseDto>().ReverseMap();
        CreateMap<User, GetListActivityUserResponseDto>().ReverseMap();
    }
}
