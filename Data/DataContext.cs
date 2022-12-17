using Microsoft.EntityFrameworkCore;
using SuperSuperSimpleHelpDesk.Entities;

namespace SuperSuperSimpleHelpDesk.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Ticket { get; set; } = default!;
    }
}
