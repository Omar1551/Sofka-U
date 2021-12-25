using System;
using System.Collections.Generic;

#nullable disable

namespace JuegoPreguntas.Models.DB
{
    public partial class Pregunta
    {
        public Pregunta()
        {
            Respuesta = new HashSet<Respuesta>();
        }

        public int IdPregunta { get; set; }
        public string DescPregunta { get; set; }
        public int IdCategoria { get; set; }

        public virtual Categorium IdCategoriaNavigation { get; set; }
        public virtual ICollection<Respuesta> Respuesta { get; set; }
    }
}
