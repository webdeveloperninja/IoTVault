namespace Controllers.Profiles
{
    using AutoMapper;

    public class PlantProfile : Profile
    {
        public PlantProfile()
        {
            CreateMap<Models.Plant, Core.Models.Plant>();
        }
    }
}
