using AutoMapper;
using webproject1920.Domain.Entities;
using webproject1920.ViewModels;

namespace webproject1920.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Stadiums, StadiumVM>();
            CreateMap<Locations, LocationVM>();
            CreateMap<Competitions, CompetitionVM>();
            CreateMap<Teams, TeamVM>().ForMember(dest => dest.Stadium,
                opts => opts.MapFrom(
                    src => src.Stadium));

            CreateMap<Match, MatchVM>()
                .ForMember(dest => dest.Stadium,
                    opts => opts.MapFrom(
                        src => src.Stadium
                        ))
                .ForMember(dest => dest.TeamAway,
                    opts => opts.MapFrom(
                        src => src.TeamAwayNavigation
                        ))
                .ForMember(dest => dest.TeamHome,
                    opts => opts.MapFrom(
                        src => src.TeamHomeNavigation
                        )); ;

            CreateMap<Match, MatchCalendarDataVM>()
                .ForMember(dest => dest.StartTime,
                    opts => opts.MapFrom(
                        src => src.Start))
                .ForMember(dest => dest.StadiumName,
                    opts => opts.MapFrom(
                        src => src.Stadium.Name))
                .ForMember(dest => dest.TeamAwayName,
                    opts => opts.MapFrom(
                        src => src.TeamAwayNavigation.Name))
                .ForMember(dest => dest.TeamHomeName,
                    opts => opts.MapFrom(
                        src => src.TeamHomeNavigation.Name));

            CreateMap<StadiumLocations, StadiumLocationVM>()
                .ForMember(dest => dest.Location,
                    opts => opts.MapFrom(src => src.Location.Location));

            CreateMap<MatchStadiumLocation, MSLVM>()
                .ForMember(dest => dest.Match, 
                    opts => opts.MapFrom(
                        src => src.Match
                    ))
                .ForMember(dest => dest.StadiumLocation,
                    opts => opts.MapFrom(
                        src => src.StadiumLocation));

            CreateMap<TeamCompetitionLocation, TCLVM>()
                .ForMember(dest => dest.Team,
                    opts => opts.MapFrom(
                        src => src.Team))
                .ForMember(dest => dest.Competition,
                    opts => opts.MapFrom(
                        src => src.Competition))
                .ForMember(dest => dest.Location,
                    opts => opts.MapFrom(
                        src => src.Location));

            CreateMap<OrderLine, OrderLineVM>()
                .ForMember(dest => dest.Subscription, 
                    opts => opts.MapFrom(
                        src => src.Subscription
                    ))
                .ForMember(dest => dest.Ticket, 
                    opts => opts.MapFrom(
                        src => src.Ticket
                    ));

            CreateMap<Order, OrderVM>();
        }
    }
}
