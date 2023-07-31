namespace StudentJobHelperSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasData(this.GenerateCategories());
        }

        private Category[] GenerateCategories() 
        { 
            ICollection<Category> categories = new HashSet<Category>();

            Category category;
            category = new Category()
            {
                Id = 1,
                Name = "Retail,Wholesale"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 2,
                Name = "Manufacturing"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 3,
                Name = "Restaurants, Bars, Hotels, Tourism"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 4,
                Name = "Administrative, Office and Business activities"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 5,
                Name = "Drivers, Couriers"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 6,
                Name = "Engineers and Technicians"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 7,
                Name = "Manual work"
            };
            categories.Add(category);


            category = new Category()
            {
                Id = 8,
                Name = "Architecture, Construction"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 9,
                Name = "Logistics, Spedition"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 10,
                Name = "Installation, Maintenance and Repair"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 11,
                Name = "Healthcare and Pharmacy"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 12,
                Name = "Accouning, Audit, Finance"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 13,
                Name = "Automotive, Auto Service, Gas Stations"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 14,
                Name = "Banking, Lending, Insurance"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 15,
                Name = "Marketing, Advertising, PR"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 16,
                Name = "Telecoms"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 17,
                Name = "Management"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 18,
                Name = "Design, Creative, Arts"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 19,
                Name = "Education, Courses, Translators"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 20,
                Name = "Legal"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 21,
                Name = "Aviation, Airport and Airline"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 22,
                Name = "Marine and Shipping"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 23,
                Name = "Research and development"
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
