using System;
using System.Collections.Generic;

#nullable disable

namespace ReglasDeNegocio.Models.DB
{
    public partial class Respuesta
    {
        public int IdRespuesta { get; set; }
        public string DescRespuesta { get; set; }
        public bool RespCorrecta { get; set; }
        public int IdPregunta { get; set; }

        public virtual Pregunta IdPreguntaNavigation { get; set; }
    }
}
