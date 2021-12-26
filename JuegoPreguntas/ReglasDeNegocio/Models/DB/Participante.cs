using System;
using System.Collections.Generic;

#nullable disable

namespace ReglasDeNegocio.Models.DB
{
    public partial class Participante
    {
        public int IdParticipante { get; set; }
        public string NombrePartcipante { get; set; }
        public int? Pregunta1 { get; set; }
        public int? Respuesta1 { get; set; }
        public int? Pregunta2 { get; set; }
        public int? Respuesta2 { get; set; }
        public int? Pregunta3 { get; set; }
        public int? Respuesta3 { get; set; }
        public int? Pregunta4 { get; set; }
        public int? Respuesta4 { get; set; }
        public int? Pregunta5 { get; set; }
        public int? Respuesta5 { get; set; }
        public decimal? TotalPremio { get; set; }
    }
}
