using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Json;
using Volo.Abp.Modularity;

namespace XCZ
{
    [DependsOn(
       typeof(FormDomainModule),
       typeof(FormApplicationContractsModule),
       typeof(AbpDddApplicationModule),
       typeof(AbpAutoMapperModule)
    )]
    public class FormApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<FormApplicationModule>();
            });
        }

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpJsonOptions>(option =>
            {
                option.UseHybridSerializer = false;
            });
        }
    }
}
