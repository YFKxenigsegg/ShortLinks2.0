using AutoMapper;

namespace ShortLinks.Kernel.Interfaces;
public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()).ReverseMap();
}
