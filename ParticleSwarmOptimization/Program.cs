using System;
using System.IO;

namespace musicaevolutiva
{
    class Program
    {
        static void Main(string[] args)
        {
            PSO pso = new PSO(10,50,0.9,0.9);

            Particles[] population = new Particles[Particles.PopulationSize];
            population = Particles.CreatePopulation(population);
            
            pso.Start(population);

        }
    }

}
