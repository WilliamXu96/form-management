using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using XCZ.FormManagement;

namespace XCZ.EntityFrameworkCore
{
    public static class FormDbContextModelBuilderExtensions
    {
        public static void ConfigureForm([NotNull] this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Form>(b =>
            {
                b.ToTable("base_form");

                b.ConfigureByConvention();

                b.Property(x => x.FormName).IsRequired().HasMaxLength(50);
                b.Property(x => x.Namespace).HasMaxLength(50);
                b.Property(x => x.EntityName).HasMaxLength(50);
                b.Property(x => x.TableName).HasMaxLength(50);
                b.Property(x => x.Remark).HasMaxLength(200);
                b.Property(x => x.Api).IsRequired().HasMaxLength(200);
                b.Property(x => x.DisplayName).IsRequired().HasMaxLength(100);
                b.Property(x => x.Description).IsRequired().HasMaxLength(200);
            });

            builder.Entity<FormField>(b =>
            {
                b.ToTable("base_form_fields");

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();

                b.Property(x => x.FieldName).IsRequired().HasMaxLength(50);
                b.Property(x => x.FieldType).IsRequired().HasMaxLength(50);
                b.Property(x => x.DataType).IsRequired().HasMaxLength(50);
                b.Property(x => x.Label).IsRequired().HasMaxLength(128);
                b.Property(x => x.Placeholder).HasMaxLength(50);
                b.Property(x => x.DefaultValue).HasMaxLength(256);
                b.Property(x => x.Icon).HasMaxLength(50);
                b.Property(x => x.IsIndex).HasDefaultValue(false);
                b.Property(x => x.Span).HasDefaultValue(24);
            });
        }
    }
}
