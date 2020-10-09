using Auc_Scholarship_Organizer.DTOs;
using Auc_Scholarship_Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auc_Scholarship_Organizer.App_Start
{
    public static class MappingConfig
    {
        public static void RegisterMapps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<User, UserDto>().ReverseMap();
                config.CreateMap<StudentApplication, StudentApplicationDto>().
                ForMember(dest => dest.ApprovalStatus,
                opt => opt.MapFrom(src => src.StatusId)).ReverseMap();
                config.CreateMap<Status, StatusDto>().ReverseMap();
            });
        }
    }
}