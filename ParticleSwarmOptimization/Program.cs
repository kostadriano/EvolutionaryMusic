using System;
using System.IO;

namespace musicaevolutiva
{
    class Program
    {
        static void Main(string[] args) 
        {
            
            Particles.PopulationSize = 50;
           
            Particles[] population = new Particles[Particles.PopulationSize];
           

            for (int i = 0; i < population.Length; i++)
            {
                population[i] = new Particles();
                population[i].Initiate();
                population[i].FitnessCalculate();
            }      
            
            PSO pso = new PSO();
           
            try
            {
                 pso.Start(population);

            }
           catch( Exception e)
           {
               Console.WriteLine(e.StackTrace);
           }
            
        }
    }

}
