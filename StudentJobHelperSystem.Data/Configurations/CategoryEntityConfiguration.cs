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
                Id = 1001,
                Name = "Engineers and Technicians"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 1002,
                Name = "Healthcare and Pharmacy"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 1003,
                Name = "Real-estate"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 1004,
                Name = "Legal"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 1005,
                Name = "Media, Publishing"
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
