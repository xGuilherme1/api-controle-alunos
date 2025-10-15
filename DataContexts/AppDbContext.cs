using Microsoft.EntityFrameworkCore;

namespace ApiControleAlunos.DataContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

    }
}
