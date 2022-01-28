
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcess
{
    public    class Rh_estrategiaContext : DbContext
    {
        public Rh_estrategiaContext(DbContextOptions<Rh_estrategiaContext> options ):base(options){   }
        public DbSet<Pessoa> Pessoas{ get; set; }


    }
}
