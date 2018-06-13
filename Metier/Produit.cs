using LocaMat.UI.Framework;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocaMat.Metier
{
    public class Produit
    {
        [InformationAffichage(Entete = "Id", NombreCaracteres = 3)]
        public int Id { get; set; }

        [InformationAffichage(Entete = "Nom", NombreCaracteres = 20)]
        public string Nom { get; set; }

        [InformationAffichage(Entete = "Description", NombreCaracteres = 50)]
        public string Description { get; set; }

        [InformationAffichage(Entete = "Prix jour (HT)", NombreCaracteres = 15)]
        public decimal PrixJourHT { get; set; }

        [InformationAffichage(Entete = "Catégorie", NombreCaracteres = 20)]
        [ForeignKey("IdCategorie")]
        public virtual CategorieProduit Categorie { get; set; }

        public int IdCategorie { get; set; }

        public override string ToString()
        {
            return $"{this.Nom} ({this.Id})";
        }
    }
}
