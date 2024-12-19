using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Api.Data
{
    public class EcommerceWebAuthDbContext : IdentityDbContext
    {
        public EcommerceWebAuthDbContext(DbContextOptions <EcommerceWebAuthDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "95a7cfa5 - 76f0 - 4cb3 - 9448 - 49a6148e114f";
            var writerRoleId = "626c0965-0399-40c0-b70a-5927a2f34874";

            var roles = new List<IdentityRole> { new IdentityRole

               {
                Id =readerRoleId,
                ConcurrencyStamp=readerRoleId,
                Name="Reader",
                NormalizedName="READER"

                },
                new IdentityRole
                {
                Id =writerRoleId,
                ConcurrencyStamp=writerRoleId,
                Name="Writer",
                NormalizedName="WRITER"

                }
           };

            builder.Entity<IdentityRole>().HasData(roles);




        }



    }
}
