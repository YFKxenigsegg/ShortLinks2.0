using AutoMapper;
using ShortLinks.Domain.Entities;

namespace ShortLinks.Application.LinkManagment.Models;
public partial class LinkInfo
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateRequest, Link>()
            .ForMember(x => x.LinkValue, opt => opt.MapFrom(src => "default link")) //TODO: logic for reduce the OriginalLink //in external class
            .ForMember(x => x.Created, opt => opt.MapFrom(src => DateOnly.FromDateTime(DateTime.UtcNow)))
            .ForMember(x => x.ExpirationDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(DateTime.UtcNow).AddDays(2))); //TODO: temporary -> make mapping with parametr

        profile.CreateMap<Link, LinkInfo>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => src.LinkId))
            .ForMember(x => x.Link, opt => opt.MapFrom(src => src.LinkValue))
            .ReverseMap();

        profile.CreateMap<int, GetRequest>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => src));

        profile.CreateMap<UpdateRequest, GetRequest>();

        profile.CreateMap<UpdateRequest, LinkInfo>();

        profile.CreateMap<DeleteRequest, GetRequest>();
    }
}
