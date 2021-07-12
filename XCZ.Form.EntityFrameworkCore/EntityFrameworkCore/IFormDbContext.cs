using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using XCZ.FormManagement;

namespace XCZ.EntityFrameworkCore
{
    [ConnectionStringName("Business")]
    public interface IFormDbContext : IEfCoreDbContext
    {
        DbSet<Form> Forms { get; set; }

        DbSet<FormField> FormFields { get; set; }
    }
}
