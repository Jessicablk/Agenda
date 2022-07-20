using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaTelefonica.Models
{
    public class Contexto : DbContext
    {
        //Atributos para reverenciar as tabelas
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Email> Emails { get; set; }


        //Construtor
        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Endereco>()
                .HasOne<Contato>(e => e.Contato)
                .WithMany(c => c.Enderecos)
                .HasForeignKey(e => e.IdContato)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Email>()
                .HasOne<Contato>(e => e.Contato)
                .WithMany(c => c.Emails)
                .HasForeignKey(e => e.IdContato)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
