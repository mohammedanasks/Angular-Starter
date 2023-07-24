using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.SalaryManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Entities.Masters
{
    public class SalaryPaymentMap : IEntityTypeConfiguration<SalaryPayment>
    {
        public void Configure(EntityTypeBuilder<SalaryPayment> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
