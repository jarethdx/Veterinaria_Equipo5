using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VET_WebAPI.Identity
{
    // Igual al demo del profesor — solo hereda de IdentityDbContext
    public class VetIdentityDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public VetIdentityDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
