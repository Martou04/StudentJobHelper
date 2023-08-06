using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentJobHelperSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentJobHelperSystem.Data.Configurations
{
    public class JobAdsEntityConfiguration : IEntityTypeConfiguration<JobAds>
    {
        public void Configure(EntityTypeBuilder<JobAds> builder)
        {
            builder.Property(j => j.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder
                .HasOne(j => j.Category)
                .WithMany(c => c.JobAds)
                .HasForeignKey(j => j.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(j => j.Employer)
                .WithMany(e => e.JobPositions)
                .HasForeignKey(j => j.EmployerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(this.GenerateJobAds());
        }

        private JobAds[] GenerateJobAds()
        {
            ICollection<JobAds> jobs = new HashSet<JobAds>();
            JobAds jobAds;

            jobAds = new JobAds()
            {
                Title = "nz",
                Description = "fasdgsgdfgdfg",
                Salary = 2500.00M,
                CityOfWork = "Sofia",
                TypeOfEmployment = "Permanent",
                OffDaysCount = 15,
                ForeignLanguage = "English",
                HomeOffice = false,
                PhoneNumber = "0876545222",
                Email = "employer@gmail.com",
                LogoUrl = "sdgsdfgdfgdgh",
                CategoryId = 1001,
                EmployerId = Guid.Parse("53F74E02-0C81-45B2-B443-912F2A37D410"),
                WorkerId = Guid.Parse("1461A7DE-14E2-4031-A2D9-5EDC19DF3B8E")
            };
            jobs.Add(jobAds);

            return jobs.ToArray();
        }
    }
}
