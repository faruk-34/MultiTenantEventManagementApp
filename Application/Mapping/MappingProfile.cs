using Application.Models.SubRequestModel;
using Application.Models.SubResponseModel;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Users, UsersVM>().ReverseMap();
            CreateMap<Users, RequestUsers>().ReverseMap();

            CreateMap<Tenant, TenantVM>().ReverseMap();
            CreateMap<Tenant, RequestTenant>().ReverseMap();

            CreateMap<Event, EventVM>().ReverseMap();
            CreateMap<Event, RequestEvent>().ReverseMap();

            CreateMap<Registration, RegistrationVM>().ReverseMap();
            CreateMap<Registration, RequestRegistration>().ReverseMap();

            CreateMap<List<Registration>, RegistrationVM>().ReverseMap();
            CreateMap<List<Registration>, RequestRegistration>().ReverseMap();

            CreateMap<Role, RoleVM>().ReverseMap();
            CreateMap<Role, RequestRole>().ReverseMap();

            CreateMap<UserRole, UserRoleVM>().ReverseMap();
            CreateMap<UserRole, RequestUserRole>().ReverseMap();
        }
    }
}
