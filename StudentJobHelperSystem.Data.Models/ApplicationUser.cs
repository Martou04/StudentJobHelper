namespace StudentJobHelperSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.WorkPlaces = new HashSet<JobAds>();
        }

        public virtual ICollection<JobAds> WorkPlaces { get; set; }
    }
}
