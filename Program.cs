using LocaMat.UI;
using System.Configuration;

namespace LocaMat
{
    class Program
    {
        static void Main(string[] args)
        {
            var application = new Application();
            application.Demarrer();
        }
    }

    public static class ExtensionsString
    {
        public static string Tronquer(this string valeur, int nombreCaracteres)
        {
            const string points = "...";
            return string.IsNullOrEmpty(valeur) || valeur.Length <= nombreCaracteres
                ? valeur
                : valeur.Substring(0, nombreCaracteres - points.Length) + points;
        }
    }
}
