using LoginAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginAPI.Repository
{
    public partial class ContextBD : DbContext
    {
        public ContextBD(DbContextOptions<ContextBD> options) : base(options) { }
        public virtual DbSet<Usuario>? Usuarios { get; set; }
    }
}
