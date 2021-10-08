using ConsoleTables;
using GestioneSpeseEF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestioneSpeseEF.EF
{
    public static class Client
    {
        public static void AggiuntaCategorie()
        {
            using SpesaContext ctx = new();
            var category = new Categoria()
            {
                Nome = "Alimentari"
            };

            var category2 = new Categoria()
            {
                Nome = "Elettronica"
            };

            var category3 = new Categoria()
            {
                Nome = "Abbigliamento"
            };

            if (!ctx.Categorie.Any(c => c.Nome == "Alimentari"))
            {
                ctx.Categorie.Add(category);
            }

            if (!ctx.Categorie.Any(c => c.Nome == "Elettronica"))
            {
                ctx.Categorie.Add(category2);
            }

            if (!ctx.Categorie.Any(c => c.Nome == "Abbigliamento"))
            {
                ctx.Categorie.Add(category3);
            }

            ctx.SaveChanges();

        }
        public static void InserireSpesa()
        {
            using (SpesaContext ctx = new())
            {
                Console.Clear();
                Console.WriteLine("=== Inserisci nuova spesa ===");
                Console.WriteLine();
                Console.WriteLine("- Inserisci indice Categoria: 1) Alimentari  2) Elettronica  3) Abbigliamento");
                int ClientCategory = int.Parse(Console.ReadLine());
                Console.WriteLine("- Inserisci Descrizione: ");
                string ClientDescription = Console.ReadLine();
                Console.WriteLine("- Inserisci Nome e Cognome dell'Utente: ");
                string ClientUser = Console.ReadLine();
                Console.WriteLine("- Inserisci Importo: ");
                decimal ClientImport = decimal.Parse(Console.ReadLine());

                Spesa NuovaSpesa = new()
                {
                    Data = DateTime.Now,
                    CategoriaId = ClientCategory,
                    Descrizione = ClientDescription,
                    Utente = ClientUser,
                    Importo = ClientImport,
                    Approvato = false
                };

                ctx.Spese.Add(NuovaSpesa);
                ctx.SaveChanges();
            }
        }

        public static void ApprovaSpesa()
        {
            using SpesaContext ctx = new();

            Console.Clear();
            Console.WriteLine("=== Approva una spesa ===");
            Console.WriteLine();

            IEnumerable<Spesa> ListaSpeseDaApprovare;
            ListaSpeseDaApprovare = ctx.Spese.Include(s => s.Categoria).Where(s => s.Approvato == false);

            if(ListaSpeseDaApprovare.Any())
            {
                var table = new ConsoleTable("Id", "Data", "Descrizione", "Utente", "Importo", "Approvato", "Categoria");

                foreach(Spesa s in ListaSpeseDaApprovare)
                {
                    string approvato;
                    if (s.Approvato == true)
                        approvato = "Si";
                    else
                        approvato = "No";

                    table.AddRow($"{s.Id}", $"{s.Data.ToShortDateString()}", $"{s.Descrizione}", $"{s.Utente}", $"{s.Importo}", $"{approvato}", $"{s.Categoria.Nome}");
                }

                table.Write();

                Console.WriteLine();
                Console.WriteLine("Scegli ID della spesa da approvare");
                int IdToApprove = int.Parse(Console.ReadLine());

                var SpesaDaApprovare = ctx.Spese.Find(IdToApprove);

                SpesaDaApprovare.Approvato = true;

                ctx.SaveChanges();
            }
            else
            {
                Console.WriteLine("\nNon è presente alcuna spesa da approvare\n");
            }
        }

        public static void CancellaSpesa()
        {
            using SpesaContext ctx = new();

            Console.Clear();
            Console.WriteLine("=== Cancella una spesa ===");
            Console.WriteLine();

            var table = new ConsoleTable("Id", "Data", "Descrizione", "Utente", "Importo", "Approvato", "Categoria");

            foreach (Spesa s in ctx.Spese.Include(s => s.Categoria))
            {
                string approvato;
                if (s.Approvato == true)
                    approvato = "Si";
                else
                    approvato = "No";

                table.AddRow($"{s.Id}", $"{s.Data.ToShortDateString()}", $"{s.Descrizione}", $"{s.Utente}", $"{s.Importo}", $"{approvato}", $"{s.Categoria.Nome}");
            }
                

            table.Write();

            Console.WriteLine("- Inserisci ID della spesa da cancellare");
            int IdSpesaDaCancellare = int.Parse(Console.ReadLine());

            var SpesaDaCancellare = ctx.Spese.Find(IdSpesaDaCancellare);

            if(SpesaDaCancellare != null)
            {
                ctx.Spese.Remove(SpesaDaCancellare);
            }

            ctx.SaveChanges();
        }

        public static void ListaSpeseApprovate()
        {
            using SpesaContext ctx = new();

            Console.Clear();
            Console.WriteLine("=== Lista spese approvate ===\n");

            var table = new ConsoleTable("Id", "Data", "Descrizione", "Utente", "Importo", "Approvato", "Categoria");

            foreach (Spesa s in ctx.Spese.Include(s => s.Categoria).Where(s => s.Approvato == true))
            {
                string approvato;
                if (s.Approvato == true)
                    approvato = "Si";
                else
                    approvato = "No";

                table.AddRow($"{s.Id}", $"{s.Data.ToShortDateString()}", $"{s.Descrizione}", $"{s.Utente}", $"{s.Importo}", $"{approvato}", $"{s.Categoria.Nome}");
            }

            table.Write();
        }

        public static void ListaSpesePerUtente()
        {
            using SpesaContext ctx = new();

            Console.Clear();
            Console.WriteLine("=== Lista spese per utente ===");
            Console.WriteLine();
            Console.WriteLine("Inserisci Utente di cui vuoi vedere la lista spese");
            string UtentePerLista = Console.ReadLine();

            var table = new ConsoleTable("Id", "Data", "Descrizione", "Utente", "Importo", "Approvato", "Categoria");

            foreach (Spesa s in ctx.Spese.Include(s => s.Categoria).Where(s => s.Utente == UtentePerLista))
            {
                string approvato;
                if (s.Approvato == true)
                    approvato = "Si";
                else
                    approvato = "No";

                table.AddRow($"{s.Id}", $"{s.Data.ToShortDateString()}", $"{s.Descrizione}", $"{s.Utente}", $"{s.Importo}", $"{approvato}", $"{s.Categoria.Nome}");
            }

            table.Write();
        }

        public static void TotaleSpesePerCategoria()
        {
            using SpesaContext ctx = new();

            Console.Clear();
            Console.WriteLine("=== Totale spese per categoria ===");
            Console.WriteLine();

            var TotSpesePerCategoria = ctx.Spese.GroupBy(
                s => new { s.Categoria.Nome },
                (k, g) => new
                {
                    NomeCategoria = k.Nome,
                    TotaleSpesa = g.Sum(s => s.Importo)
                }
            );

            var table = new ConsoleTable("Categoria", "Totale spesa");

            foreach (var s in TotSpesePerCategoria)
                table.AddRow($"{s.NomeCategoria}", $"{s.TotaleSpesa}");

            table.Write();
        }
    }
}
