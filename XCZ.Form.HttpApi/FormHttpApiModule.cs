using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace XCZ
{
    [DependsOn(
        typeof(FormApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule)
    )]
    public class FormHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(FormHttpApiModule).Assembly);
            });
        }
    }
}
