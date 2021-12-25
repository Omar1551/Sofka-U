using System;
using System.Collections.Generic;
using System.Text;

namespace JuegoPreguntas.Models.DB
{
    class Participantes
    {
        public int IdPartcipante { get; set; }
        public string Nombre_Participante { get; set; }
        public int? Pregunra_1 { get; set; }
        public int? Respuesta_1 { get; set; }
        public int? Pregunra_2 { get; set; }
        public int? Respuesta_2 { get; set; }
        public int? Pregunra_3 { get; set; }
        public int? Respuesta_3 { get; set; }
        public int? Pregunra_4 { get; set; }
        public int? Respuesta_4 { get; set; }
        public int? Pregunra_5 { get; set; }
        public int? Respuesta_5 { get; set; }
        public decimal? Total_Premio { get; set; }

    }
}
