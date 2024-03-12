using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairService.Infrastructure.Data.Configuration
{
    public class RepairStatusConfiguration : IEntityTypeConfiguration<RepairStatus>
    {
        public void Configure(EntityTypeBuilder<RepairStatus> builder)
        {
            builder.HasData(RepairStatusUpdate());
        }

        private List<RepairStatus> RepairStatusUpdate()
        {
            var repairStatus = new List<RepairStatus>();
            //pending
            var status = new RepairStatus()
            {
                Id = 1,
                Name = "Pending"
            };

            repairStatus.Add(status);
            //approved
            status = new RepairStatus()
            {
                Id = 2,
                Name = "Approved"
            };

            repairStatus.Add(status);
            //declined
            status = new RepairStatus()
            {
                Id = 3,
                Name = "Declined"
            };

            repairStatus.Add(status);
            //deleted
            status = new RepairStatus()
            {
                Id = 4,
                Name = "Deleted"
            };

            repairStatus.Add(status);
            //completed
            status = new RepairStatus()
            {
                Id = 5,
                Name = "Completed"
            };

            repairStatus.Add(status);
            return repairStatus;
        }
    }
}
