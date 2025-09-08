

using System;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using Daric.Core.Domain.ValueObjects.Common;
using Daric.Infra.Data.Commands;
using Daric.Infra.Data.Commands.Extensions;
using DaricTemplate.Core.Domain.BaseInfo.CountryLocations.Entities;
using DaricTemplate.Infra.Data.Commands.Common;
using Microsoft.EntityFrameworkCore;

namespace DaricTemplate.Infra.Data.Commands.Context;

public class DaricTemplateCommandDbContext : BaseCommandDbContext
{
    public DbSet<CountryLocation> CountryLocations { get; set; } 
    public DaricTemplateCommandDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        base.OnModelCreating(builder);

        builder.AddBusinessId();

        builder.AddCustomEntitiesMappings();

    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<Title>().HaveConversion<TitleConversion>();
        configurationBuilder.Properties<BusinessId>().HaveConversion<BusinessIdConversion>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }


}
