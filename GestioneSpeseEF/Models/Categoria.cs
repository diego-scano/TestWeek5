using System.Collections.Generic;

namespace GestioneSpeseEF.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IList<Spesa> Spese { get; set; }
    }
}
