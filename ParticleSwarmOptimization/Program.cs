using System;
using System.IO;

namespace musicaevolutiva
{
    class Program
    {
        static void Main(string[] args)
        {

            Particles.PopulationSize = 4;

            Particles[] population = new Particles[Particles.PopulationSize];


            for (int i = 0; i < population.Length; i++)
            {
                population[i] = new Particles();
                population[i].Initiate();
                population[i] = Particles.FitnessCalculate(population[i]);
                population[i].PFitness = population[i].Fitness;
                population[i].PTimes = population[i].Times;
                population[i].PNotes = population[i].Notes;
            }


            /*            for (int i = 0; i < 6; i++)
                       {
                           Console.WriteLine(population[i].showConfiguration());

                       }
            */

            PSO pso = new PSO(2);
            pso.Start(population);


            //     try
            //     {
            //          pso.Start(population);

            //     }
            //    catch( Exception e)
            //    {
            //        Console.WriteLine(e.StackTrace);
            //    }

        }
    }

}
