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
            var pregunta = 0;
            var respuesta = false;
            var premio = 100000;
            var acum_premio = 0;
            Console.WriteLine("Por favor digite su nombre:");
            string nombre;
            nombre = Console.ReadLine();
            Console.WriteLine($"Nombre del participante: {nombre}");
            pregunta += 1;
            var randomNumero = new Random().Next(1, 6);
            
            Console.WriteLine($"Nuemro aleatorio pregunta{pregunta} es: {randomNumero}");
            
            List<Pregunta> listPreguntas = new List<Pregunta>();
            using (var db = new Prueba_SofkaContext())
            {
                listPreguntas = db.Preguntas.Where(p => p.IdCategoria == 1).ToList();
                int id = listPreguntas.FindIndex(x => x.IdPregunta == randomNumero);
                var Preg = listPreguntas[id].DescPregunta;
                Console.WriteLine($"Por un premio de ${premio} pesos.\n{Preg}:");

                List<Respuesta> listRespuestas = new List<Respuesta>();
                listRespuestas = db.Respuestas.Where(p => p.IdPregunta == randomNumero).ToList();
                int idOpcion = listRespuestas.FindIndex(x => x.IdPregunta == randomNumero);
                var resp = listRespuestas[idOpcion].DescRespuesta;
                Console.WriteLine($"Elija una de las siguientes opciones:");
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine($"{resp}");
                }
                
                string opcion;
                opcion = Console.ReadLine();
                Console.WriteLine($"Opción seleccionada: {opcion}");
               
                




            }
           

            if (respuesta == true)
            {
                acum_premio *= 2;
                Console.WriteLine($"Por ${acum_premio} pesos.\n ");
            }

            //Console.WriteLine("Hello World!");
        }
    }
}
