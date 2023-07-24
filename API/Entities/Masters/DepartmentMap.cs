using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Entities.Masters
{
    public class DepartmentMap : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .HasOne(x => x.DepartmentHead)
                .WithMany()
                .HasForeignKey(x => x.DepartmentHeadId)
                .OnDelete(DeleteBehavior.NoAction
                );
        }
    }
}
