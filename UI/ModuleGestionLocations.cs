using LocaMat.Dal;
using LocaMat.Metier;
using LocaMat.UI;
using LocaMat.UI.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocaMat
{
    class ModuleGestionLocations
    {
        private Menu menu;

        public ModuleGestionLocations(Application application)
        {
            Application = application;
        }

        private const string MessagePourValeurInvalide = "Valeur invalide. Veuillez recommencer: ";

        public Application Application { get; }

        private void InitialiserMenu()
        {
            this.menu = new Menu("Gestion des locations");
            this.menu.AjouterElement(new ElementMenu("1", "Afficher les locations")
            {
                FonctionAExecuter = this.AfficherLocations
            });
            this.menu.AjouterElement(new ElementMenu("2", "Ajouter une location")
            {
                FonctionAExecuter = this.AjouterLocation
            });
            this.menu.AjouterElement(new ElementMenu("3", "Supprimer une location")
            {
                FonctionAExecuter = this.SupprimerLocation
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

        private void AfficherLocations()
        {
            ConsoleHelper.AfficherEntete("Locations");

            var liste = new BaseDonnees().Locations.ToList();
            ConsoleHelper.AfficherListe(liste);
        }

        private void AjouterLocation()
        {
            ConsoleHelper.AfficherEntete("Nouvelle location");

            var dal = new BaseDonnees();
            {
                var location = new Location();

                Application.ModuleGestionProduits.AfficherProduits();
                location.IdProduit = ConsoleSaisie.SaisirEntierObligatoire("IdProduit : ");
                var produit = dal.Produits.SingleOrDefault(x => x.Id == location.IdProduit);
                if(produit ==null)
                {
                    ConsoleHelper.AfficherMessageErreur("Produit inexistant. Retour au menu");
                    return;
                }

                Application.ModuleGestionAgences.AfficherAgences();
                location.IdAgence = ConsoleSaisie.SaisirEntierObligatoire("IdAgence : ");
                var agence = dal.Agences.SingleOrDefault(x => x.Id == location.IdAgence);
                if (agence == null)
                {
                    ConsoleHelper.AfficherMessageErreur("Agence inexistante. Retour au menu");
                    return;
                }

                Application.ModuleGestionClients.AfficherClients();
                location.IdClient = ConsoleSaisie.SaisirEntierObligatoire("IdClient : ");
                var client = dal.Clients.SingleOrDefault(x => x.Id == location.IdClient);
                if (client == null)
                {
                    ConsoleHelper.AfficherMessageErreur("Produit inexistant. Retour au menu");
                    return;
                }

                location.DateDebut = ConsoleSaisie.SaisirDateObligatoire("DateDebut : ");
                if (location.DateDebut < DateTime.Today)
                {
                    ConsoleHelper.AfficherMessageErreur("Date invalide. Retour au menu");
                    return;
                }

                location.DateFin = ConsoleSaisie.SaisirDateObligatoire("DateFin : ");
                int result = DateTime.Compare(location.DateDebut, location.DateFin);
                if (location.DateFin < location.DateDebut)
                {
                    ConsoleHelper.AfficherMessageErreur("Date invalide. Retour au menu");
                    return;
                }

                location.Quantite = ConsoleSaisie.SaisirEntierObligatoire("Quantite : ");

                int result = DateTime.Compare(location.DateDebut, location.DateFin);

                dal.Locations.Add(location);
                dal.SaveChanges();
            }
        }

        private void SupprimerLocation()
        {
            ConsoleHelper.AfficherEntete("SupprimerLocation");

            var liste = new BaseDonnees().Locations.ToList();
            ConsoleHelper.AfficherListe(liste);

            var id = ConsoleSaisie.SaisirEntierObligatoire("Numero Id : ");

            using (var sup = new BaseDonnees())
            {
                var location = sup.Locations.Single(x => x.Id == id);
                sup.Locations.Remove(location);
                sup.SaveChanges();
            }

        }
    }
}
