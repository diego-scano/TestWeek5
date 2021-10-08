using System;

namespace GestioneSpeseEF.Models
{
    public class Spesa
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int CategoriaId { get; set; }
        public string Descrizione { get; set; }
        public string Utente { get; set; }
        public decimal Importo { get; set; }
        public bool Approvato { get; set; }
        public Categoria Categoria { get; set; }
    }
}
