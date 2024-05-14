using AutoMapper;
using MyProject.Application.Features.Streamers.Commands;
using MyProject.Application.Features.Videos.Queries.GetVideosList;
using MyProject.Domain;

namespace MyProject.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Video, VideosVm>();
        CreateMap<CreateStreamerCommand, Streamer>();
    }
}
