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
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("---------------------- Bienvenido al juego de preguntas. ----------------------");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("- Presione 1 para configurar el juego (Crear preguntas y respuestas)");
            Console.WriteLine("- Presione 2 para jugar.");

            var opcionMenu = Console.ReadLine();
            if (opcionMenu.Trim() == "1")
            {
                Console.WriteLine("-------------------------------------------------------------------------------");
                Console.WriteLine("--------------------- Creación de preguntas y respuestas. ---------------------");
                Console.WriteLine("-------------------------------------------------------------------------------");

                Console.WriteLine();
                Console.WriteLine("Ingrese el nivel de la pregunta: ");
                var nivelPregunta = Console.ReadLine();
                using (var db = new Prueba_SofkaContext())
                {


                    var nivel = db.Categoria.Where(c => c.Nivel == Convert.ToInt32(nivelPregunta)).FirstOrDefault();
                    Console.WriteLine();
                    Console.WriteLine("Escriba la pregunta que desea crear:");
                    string infoNuevaPregunta;
                    infoNuevaPregunta = Console.ReadLine();
                    var nuevaPregunta = new Pregunta();
                    nuevaPregunta.DescPregunta = infoNuevaPregunta;
                    nuevaPregunta.IdCategoria = nivel.IdCategoria;
                    db.Preguntas.Add(nuevaPregunta);
                    db.SaveChanges();

                    Console.WriteLine("Acontinuación, ingrese las opciones para la pregunta.\n");
                    var respCorrecta = false;
                    for (int z = 0; z < 4; z++)
                    {

                        var nuevaRespuesta = new Respuesta();
                        nuevaRespuesta.IdPregunta = nuevaPregunta.IdPregunta;

                        Console.WriteLine();
                        Console.WriteLine($"Digite desc de la respuesta {z + 1}");
                        var descNuevaRespuesta = Console.ReadLine();
                        nuevaRespuesta.DescRespuesta = descNuevaRespuesta;

                        if (respCorrecta == false)
                        {
                            Console.WriteLine("Si es esta es la respuesta correcta a la pregunta escriba 'S', si no escriba cualquier otro caracter:");
                            var infoNuevaRespuesta = Console.ReadLine();
                            nuevaRespuesta.RespCorrecta = infoNuevaRespuesta.Trim().ToLower() == "s" ? true : false;
                            respCorrecta = nuevaRespuesta.RespCorrecta;
                        }
                        else
                        {
                            nuevaRespuesta.RespCorrecta = false;
                        }

                        db.Respuestas.Add(nuevaRespuesta);
                        db.SaveChanges();
                    }

                    Console.WriteLine("Pregunta registrada con éxito");
                    Console.WriteLine();


                }


            }
            else
            {
                var acum_premio = 0.0;
                Console.WriteLine("Por favor digite su nombre:");
                string nombre;
                nombre = Console.ReadLine();
                Console.WriteLine($"Nombre del participante: {nombre}");

                using (var db = new Prueba_SofkaContext())
                {
                    var infoParticipante = new Participante();
                    infoParticipante.NombrePartcipante = nombre;
                    for (int j = 1; j < 6; j++)
                    {

                        var nivelActual = db.Categoria.Where(c => c.Nivel == j).FirstOrDefault();
                        List<Pregunta> listPreguntas = new List<Pregunta>();

                        listPreguntas = db.Preguntas.Where(p => p.IdCategoria == nivelActual.IdCategoria).ToList();
                        var randomNumero = new Random().Next(0, listPreguntas.Count);
                        if (listPreguntas.Count >= randomNumero)
                        {
                            Pregunta PreguntaSeleccinada = listPreguntas[randomNumero];
                            var descripcionPregunta = PreguntaSeleccinada.DescPregunta;
                            Console.WriteLine();
                            Console.WriteLine($"Nivel #{j}. \nPor un premio de ${nivelActual.ValPremioNivel} pesos.\n{descripcionPregunta}:");

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

                            var opcionSelectedString = Console.ReadLine();

                            if (int.TryParse(opcionSelectedString, out var opcionSelected) == false || opcionSelected > 4 || opcionSelected < 1)
                            {
                                Console.WriteLine("Ingresaste una opción invalida. El juego se da por terminado. ");
                                acum_premio = 0;
                                break;
                            }

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
                            if (j < 5)
                            {
                                Console.WriteLine("¿Deseas continuar con la siguiente pregunta? \nRecuerda que el nivel de dificultad será mayor. \nIngresa 'S' para continuar o cualquier otro para finalizar.");

                                var continuar = Console.ReadLine();
                                if (continuar.Trim().ToLower() != "s")
                                {
                                    Console.WriteLine($"Felicitaciones {nombre}!! Terminaste con un acumulado de ${acum_premio}");
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Felicitaciones {nombre}!! Completaste el juego hasta el final. Terminaste con un acumulado de ${acum_premio}");
                            }

                        }
                        else
                        {
                            Console.WriteLine("La cantidad de preguntas no coincide con lo esperado. \n El juego se da por finalizado, revisa por favor las configuracion de las preguntas.");
                            acum_premio = 0;
                            break;
                        }


                    }

                    infoParticipante.TotalPremio = Convert.ToDecimal(acum_premio);
                    db.Participantes.Add(infoParticipante);
                    db.SaveChanges();
                    Console.WriteLine("El juego ha terminado. Presione enter para cerrar la ventana.");
                    Console.ReadLine();
                }
            }

        }
    }
}
