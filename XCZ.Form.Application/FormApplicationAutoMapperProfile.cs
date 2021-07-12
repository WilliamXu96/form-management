using AutoMapper;
using Volo.Abp.AutoMapper;
using XCZ.FormManagement.Dto;
using XCZ.FormManagement;
using XCZ.FormBuildManagement.Dto;

namespace XCZ
{
    public class FormApplicationAutoMapperProfile : Profile
    {
        public FormApplicationAutoMapperProfile()
        {
            CreateMap<Form, FormDto>()
                .Ignore(dto => dto.Fields);
            CreateMap<Form, FormBuildDto>();
            CreateMap<FormField, FormFieldDto>();
        }
    }
}
