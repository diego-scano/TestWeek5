using GestioneSpeseEF.EF;
using System;

namespace GestioneSpeseEF
{
    class Program
    {
        static void Main(string[] args)
        {
            Client.AggiuntaCategorie();

            bool quit = false;

            do
            {
                Console.WriteLine("\n=== MENU PRINCIPALE ===");
                Console.WriteLine();
                Console.WriteLine("[ 1 ] - Inserisci nuova spesa");
                Console.WriteLine("[ 2 ] - Approva una spesa");
                Console.WriteLine("[ 3 ] - Cancella una spesa");
                Console.WriteLine("[ 4 ] - Elenco spese approvate");
                Console.WriteLine("[ 5 ] - Elenco spese per utente");
                Console.WriteLine("[ 6 ] - Totale spese per categoria");
                Console.WriteLine("[ q ] - Esci");
                var command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        Client.InserireSpesa();
                        break;
                    case "2":
                        Client.ApprovaSpesa();
                        break;
                    case "3":
                        Client.CancellaSpesa();
                        break;
                    case "4":
                        Client.ListaSpeseApprovate();
                        break;
                    case "5":
                        Client.ListaSpesePerUtente();
                        break;
                    case "6":
                        Client.TotaleSpesePerCategoria();
                        break;
                    case "q":
                        quit = true;
                        break;
                }
            } while (!quit);
        }
    }
}
