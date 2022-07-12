using AutoMapper;
using Volo.Abp.AutoMapper;
using XCZ.FormManagement.Dto;
using XCZ.FormManagement;
using XCZ.FormBuildManagement.Dto;
using XCZ.FormDataManagement.Dto;

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
            CreateMap<FormFieldOption, FieldOption>();
            CreateMap<FormData, FormDataDto>();
        }
    }
}
