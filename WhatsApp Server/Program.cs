using System;

using Library;
using Library.Infrastructure;

namespace WhatsApp_Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            WhatsAppServer server = new();
            server.ListenAsync();
            var x = new CSVEnumerable("dades.csv");
            foreach (string item in x)
            {
                var camps = item.Split(';');
                var aux = new Registre()
                {
                    Any = int.Parse(camps[0]),
                    Mes = int.Parse(camps[1]),
                    CodiDistricte = int.Parse(camps[2]),
                    NomDistricte = camps[3].Trim('"'),
                    CodiBarri = int.Parse(camps[4]),
                    NomBarri = camps[5].Trim('"'),
                    Sexe = camps[6] == "\"Dones\"" ? Sexe.Dones : Sexe.Homes,
                    DemandaOcupacio = camps[7] == "\"Atur Registrat\"" ? DemandaOcupacio.AturRegistrat : DemandaOcupacio.DemandaNoAturats,
                    Num = int.Parse(camps[8])
                };
                server.Registres.Add((aux.Any, aux.Mes, aux.CodiDistricte, aux.CodiBarri, aux.Sexe, aux.DemandaOcupacio), aux);
            }
            Console.Title = server.ToString();
            while (Console.ReadKey().Key != ConsoleKey.Spacebar)
            {

            }

            server.StopListener();
        }
    }
}
