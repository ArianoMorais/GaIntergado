using Microsoft.EntityFrameworkCore;

namespace Intergado.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options) 
        { 
            
        }

        public DbSet<Fazenda> Fazendas { get; set; }
        public DbSet<Animal> Animais { get; set; }

       
    }
}
