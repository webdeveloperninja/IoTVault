namespace Controllers.Profiles
{
    using AutoMapper;
    using System;

    public class DateTimeProfile : Profile
    {
        public DateTimeProfile()
        {
            CreateMap<string, DateTime>().ConvertUsing(Convert.ToDateTime);
        }
    }
}
