using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Daric.Core.Domain.Entities.Common;
using Daric.Infra.Data.Commands.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Daric.Infra.Data.Commands;
public abstract class BaseOutboxCommandDbContext : BaseCommandDbContext
{
    public DbSet<OutBoxEventItem> OutBoxEventItems { get; set; }

    public BaseOutboxCommandDbContext(DbContextOptions<BaseOutboxCommandDbContext> options) : base(options)
    {

    }
     
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(new AddOutBoxEventItemInterceptor());
    }
   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OutBoxEventItem>(entity =>
        {  
            entity.Property(c => c.EventName).HasMaxLength(255);
            entity.Property(c => c.AggregateName).HasMaxLength(255);
            entity.Property(c => c.EventTypeName).HasMaxLength(500);
            entity.Property(c => c.AggregateTypeName).HasMaxLength(500);
            entity.Property(c => c.TraceId).HasMaxLength(100);
            entity.Property(c => c.SpanId).HasMaxLength(100);

            entity.ToTable("OutBoxEventItems","Framework");

        });
    }

}
