using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RestFull_Api.Data
{
    public class NZWalkAuth : IdentityDbContext
    {
        public NZWalkAuth(DbContextOptions<NZWalkAuth> opt ):base(opt)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var ReaderRoleId = "fd63d6a0-3f0e-45d3-a14f-4c95c7acd396";
            var WriterRoleId = "9eb4d524-3a34-4415-86f8-71a42573ed0a";
            
            var Roles=new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = ReaderRoleId,
                    ConcurrencyStamp=ReaderRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id= WriterRoleId,
                    ConcurrencyStamp = WriterRoleId,
                     Name="Writer",
                     NormalizedName="Writer".ToUpper(),
                }

            };

            builder.Entity<IdentityRole>().HasData(Roles);
        }
    }
}
