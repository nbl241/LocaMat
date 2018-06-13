using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocaMat.Metier;

namespace LocaMat.Dal
{
    class BaseDonnees : DbContext
    {
        public BaseDonnees(string connectionString = "Connexion")
            : base(connectionString)
        {

        }

        public DbSet<Agence> Agences { get; set; }

        public DbSet<CategorieProduit> CategoriesProduits { get; set; }

        public DbSet<Produit> Produits { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<OffreProduit> OffresProduits { get; set; }

        public DbSet<Client> Clients { get; set; }
    }
}
