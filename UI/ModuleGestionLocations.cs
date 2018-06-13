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


            var location = new Location();

            Application.ModuleGestionProduits.AfficherProduits();
            location.IdProduit = ConsoleSaisie.SaisirEntierObligatoire("IdProduit : ");
            bool estSaisieValide = false;
            do
            {
                var saisie = Console.ReadLine();
                string IdProduit = null;
                if (saisie != IdProduit)
                {
                    ConsoleHelper.AfficherMessageErreur(MessagePourValeurInvalide);
                }
            }while (!estSaisieValide);

            Application.ModuleGestionAgences.AfficherAgences();
            location.IdAgence = ConsoleSaisie.SaisirEntierObligatoire("IdAgence : ");

            Application.ModuleGestionClients.AfficherClients();
            location.IdClient = ConsoleSaisie.SaisirEntierObligatoire("IdClient : ");

            location.DateDebut = ConsoleSaisie.SaisirDateObligatoire("DateDebut : ");

            location.DateFin = ConsoleSaisie.SaisirDateObligatoire("DateFin : ");

            location.Quantite = ConsoleSaisie.SaisirEntierObligatoire("Quantite : ");

            var bd = new BaseDonnees();
            {
                bd.Locations.Add(location);
                bd.SaveChanges();
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
