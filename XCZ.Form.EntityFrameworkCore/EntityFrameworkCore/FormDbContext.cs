using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using XCZ.FormManagement;

namespace XCZ.EntityFrameworkCore
{
    //[ConnectionStringName("Business")]
    public class FormDbContext : AbpDbContext<FormDbContext>, IFormDbContext
    {
        public DbSet<Form> Forms { get; set; }

        public DbSet<FormField> FormFields { get; set; }

        public FormDbContext(DbContextOptions<FormDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureForm();
        }
    }
}
