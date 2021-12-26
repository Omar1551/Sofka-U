using System;
using System.Collections.Generic;

#nullable disable

namespace ReglasDeNegocio.Models.DB
{
    public partial class Categorium
    {
        public Categorium()
        {
            Pregunta = new HashSet<Pregunta>();
        }

        public int IdCategoria { get; set; }
        public int Nivel { get; set; }
        public decimal ValPremioNivel { get; set; }

        public virtual ICollection<Pregunta> Pregunta { get; set; }
    }
}
