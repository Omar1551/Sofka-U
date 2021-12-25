using System;
using System.Collections.Generic;
using JuegoPreguntas.Models;
using JuegoPreguntas.Models.DB;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections;


namespace JuegoPreguntas
{
    class JuegoPreguntas
    {
        static void Main(string[] args)
        {
            var acum_premio = 0.0;
            Console.WriteLine("Por favor digite su nombre:");
            string nombre;
            nombre = Console.ReadLine();
            Console.WriteLine($"Nombre del participante: {nombre}");
            var randomNumero = new Random().Next(0, 5);
            using (var db = new Prueba_SofkaContext())
            {
                var infoParticipante = new Participante();
                infoParticipante.NombrePartcipante = nombre;
                for (int j = 1; j < 6; j++)
                {

                    var nivelActual = db.Categoria.Where(c => c.Nivel == j).FirstOrDefault();
                    List<Pregunta> listPreguntas = new List<Pregunta>();

                    listPreguntas = db.Preguntas.Where(p => p.IdCategoria == nivelActual.IdCategoria).ToList();
                    if (listPreguntas.Count >= randomNumero)
                    {
                        Pregunta PreguntaSeleccinada = listPreguntas[randomNumero];
                        var descripcionPregunta = PreguntaSeleccinada.DescPregunta;
                        Console.WriteLine($"Nivel: #{j}. Por un premio de ${nivelActual.ValPremioNivel} pesos.\n{descripcionPregunta}:");

                        List<Respuesta> listRespuestas = new List<Respuesta>();
                        listRespuestas = db.Respuestas.Where(p => p.IdPregunta == PreguntaSeleccinada.IdPregunta).ToList();

                        var indexRespCorrecta = -1;
                        for (int i = 0; i < listRespuestas.Count; i++)
                        {
                            var resp = listRespuestas[i].DescRespuesta;
                            Console.WriteLine($"{i + 1}){resp}");
                            if (listRespuestas[i].RespCorrecta == true)
                            {
                                indexRespCorrecta = i + 1;
                            }
                        }
                        Console.WriteLine($"Ingrese el número de la opción seleccionada: ");

                        var opcionSelected = Convert.ToInt32(Console.ReadLine());

                        switch (nivelActual.Nivel)
                        {
                            case 1:
                                infoParticipante.Pregunta1 = PreguntaSeleccinada.IdPregunta;
                                infoParticipante.Respuesta1 = listRespuestas[opcionSelected - 1].IdRespuesta;
                                break;
                            case 2:
                                infoParticipante.Pregunta2 = PreguntaSeleccinada.IdPregunta;
                                infoParticipante.Respuesta2 = listRespuestas[opcionSelected - 1].IdRespuesta;
                                break;
                            case 3:
                                infoParticipante.Pregunta3 = PreguntaSeleccinada.IdPregunta;
                                infoParticipante.Respuesta3 = listRespuestas[opcionSelected - 1].IdRespuesta;
                                break;
                            case 4:
                                infoParticipante.Pregunta4 = PreguntaSeleccinada.IdPregunta;
                                infoParticipante.Respuesta4 = listRespuestas[opcionSelected - 1].IdRespuesta;
                                break;
                            case 5:
                                infoParticipante.Pregunta5 = PreguntaSeleccinada.IdPregunta;
                                infoParticipante.Respuesta5 = listRespuestas[opcionSelected - 1].IdRespuesta;
                                break;
                        }

                        if (opcionSelected == indexRespCorrecta)
                        {
                            acum_premio += Convert.ToDouble(nivelActual.ValPremioNivel);
                            Console.WriteLine($"LA RESPUESTA ES CORRECTA. Acumulaste: ${acum_premio}");
                        }
                        else
                        {
                            acum_premio = 0;
                            Console.WriteLine($"LA RESPUESTA ES INCORRECTA. Quedaste eliminado.");
                            break;
                        }
                        if (j <= 5)
                        {
                            Console.WriteLine("¿Deseas continuar con la siguiente pregunta? \nRecuerda que el nivel de dificultad será mayor. \nIngrese 'S' para continuar o 'N' para finalizar.");

                            var continuar = Console.ReadLine();
                            if (continuar.Trim().ToLower() != "s")
                            {
                                break;
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine("La cantidad de preguntas no coincide con lo esperado.");
                        acum_premio = 0;
                    }


                }

                Console.WriteLine($"El juego ha finalizado. Terminaste con un acumulado de {acum_premio}");
                infoParticipante.TotalPremio = Convert.ToDecimal(acum_premio);
                db.Participantes.Add(infoParticipante);
                db.SaveChanges();
                Console.WriteLine("Presione enter para finalizar.");
                Console.ReadLine();
            }

        }
    }
}
