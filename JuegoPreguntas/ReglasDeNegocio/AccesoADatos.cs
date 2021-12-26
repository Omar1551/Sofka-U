using ReglasDeNegocio.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReglasDeNegocio
{
    public class AccesoADatos: IDisposable
    {
        Prueba_SofkaContext db;
        public AccesoADatos()
        {
            db = new Prueba_SofkaContext();
        }

        public Categorium ObtenerCategoriaSegunNivel(int nivel)
        {
            Categorium result;
           
            result = db.Categoria.Where(c => c.Nivel == nivel).FirstOrDefault();
            
            return result;
        }

        public void CrearPregunta(Pregunta infoNuevaPregunta)
        {
            db.Preguntas.Add(infoNuevaPregunta);
            db.SaveChanges();

        }

        public void crearRespuesta(Respuesta descNuevaRespuesta)
        {
            db.Respuestas.Add(descNuevaRespuesta);
            db.SaveChanges();
        }

        public List<Pregunta> consultarPregunta(int nivelActual)
        {
            List<Pregunta> result;

            result = db.Preguntas.Where(p => p.IdCategoria == nivelActual).ToList();

            return result;
        } 

        public List<Respuesta> ConsultarRespuesta(int IdPregunta)
        {
            List<Respuesta> result;

            result = db.Respuestas.Where(r => r.IdPregunta == IdPregunta).ToList();

            return result;
        }

        public void GuardarHistorialParticipante(Participante infoParticipante)
        {
            db.Participantes.Add(infoParticipante);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
