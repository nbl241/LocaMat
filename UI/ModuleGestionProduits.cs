using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using LocaMat.Dal;
using LocaMat.Metier;
using LocaMat.UI.Framework;

namespace LocaMat.UI
{
    public class ModuleGestionProduits
    {
        private Menu menu;

        private void InitialiserMenu()
        {
            this.menu = new Menu("Gestion des produits");
            this.menu.AjouterElement(new ElementMenu("1", "Afficher les produits")
            {
                FonctionAExecuter = this.AfficherProduits
            });
            this.menu.AjouterElement(new ElementMenu("2", "Ajouter un produit")
            {
                FonctionAExecuter = this.AjouterProduit
            });
            this.menu.AjouterElement(new ElementMenu("3", "Supprimer un produit")
            {
                FonctionAExecuter = this.SupprimerProduit
            });
            this.menu.AjouterElement(new ElementMenuQuitterMenu("R", "Revenir au menu principal..."));
        }

        public void Demarrer()
        {
            if (this.menu == null)
            {
                this.InitialiserMenu();
            }

            this.menu.Afficher();
        }

        public void AfficherProduits()
        {
            ConsoleHelper.AfficherEntete("Produits");

            var liste = new BaseDonnees().Produits.ToList();
            ConsoleHelper.AfficherListe(liste);
        }

        private void AjouterProduit()
        {
            ConsoleHelper.AfficherEntete("Nouveau produit");

            var dal = new BaseDonnees();
            {
                var produit = new Produit();

                produit.Nom = ConsoleSaisie.SaisirChaine("Nom du produit : ", false);
                produit.Description = ConsoleSaisie.SaisirChaine("Description : ", false);
                produit.IdCategorie = ConsoleSaisie.SaisirEntierObligatoire("IdCategorie : ");
                produit.PrixJourHT = ConsoleSaisie.SaisirDecimalObligatoire("PrixJourHT : ");

                var categorie = dal.CategoriesProduits.SingleOrDefault(x => x.Id == produit.IdCategorie);
                if (categorie == null)
                {
                    ConsoleHelper.AfficherMessageErreur("Categorie invalide. Retour au menu");
                    return;
                }

                dal.Produits.Add(produit);
                dal.SaveChanges();
            }
        }

        private void SupprimerProduit()
        {
            ConsoleHelper.AfficherEntete("SupprimerProduit");

            var liste = new BaseDonnees().Produits.ToList();
            ConsoleHelper.AfficherListe(liste);

            var id = ConsoleSaisie.SaisirEntierObligatoire("Numero Id : ");

            using (var sup = new BaseDonnees())
            {
                var produit = sup.Produits.Single(x => x.Id == id);
                sup.Produits.Remove(produit);
                sup.SaveChanges();
            }
        }
    }
}
