using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using LocaMat.Dal;
using LocaMat.Metier;
using LocaMat.UI.Framework;

namespace LocaMat.UI
{
    public class ModuleGestionClients
    {
        private Menu menu;

        private void InitialiserMenu()
        {
            this.menu = new Menu("Gestion des clients");
            this.menu.AjouterElement(new ElementMenu("1", "Afficher les clients")
            {
                FonctionAExecuter = this.AfficherClients
            });
            this.menu.AjouterElement(new ElementMenu("2", "Ajouter un client")
            {
                FonctionAExecuter = this.AjouterClient
            });
            this.menu.AjouterElement(new ElementMenu("3", "Supprimer un client")
            {
                FonctionAExecuter = this.SupprimerClient
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

        public void AfficherClients()
        {
            {
                ConsoleHelper.AfficherEntete("Produits");

                var liste = new BaseDonnees().Clients.ToList();
                ConsoleHelper.AfficherListe(liste);
            }
        }

        private void AjouterClient()
        {
            ConsoleHelper.AfficherEntete("Nouveau client");

            var client = new Client
            {
                Nom = ConsoleSaisie.SaisirChaine("Nom : ", false),
                Prenom = ConsoleSaisie.SaisirChaine("Prenom : ", false),
                Adresse = ConsoleSaisie.SaisirChaine("Adresse : ", false),
            };

            var bd = new BaseDonnees();
            {
                bd.Clients.Add(client);
                bd.SaveChanges();
            }
        }

        private void SupprimerClient()
        {
            ConsoleHelper.AfficherEntete("Supprimer un Client");

            var liste = new BaseDonnees().Clients.ToList();
            ConsoleHelper.AfficherListe(liste);

            var id = ConsoleSaisie.SaisirEntierObligatoire("Numero Id : ");

            using (var sup = new BaseDonnees())
            {
                var client = sup.Clients.Single(x => x.Id == id);
                sup.Clients.Remove(client);
                sup.SaveChanges();
            }
        }
    }
}
