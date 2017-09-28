using System;
using System.IO;

namespace musicaevolutiva
{
    class Program
    {
        static void Main(string[] args)
        {
            Individuals.PopulationSize = 50;
            int NGeracoes = 20;
            Individuals[] population = new Individuals[Individuals.PopulationSize];

            for (int i = 0; i < population.Length; i++)
            {
                population[i] = new Individuals();
                population[i].Initiate();
                population[i].FitnessCalculate();
            }
            population = AG.OrderBy(population);
            Output.scoreGenerator(population, "populacaooriginal.ly");

            AG ag = new AG();
            int g = 0;

            using (StreamWriter writer = new StreamWriter("melhores.ods", true))
            {
                writer.WriteLine("Geracoes:," + NGeracoes + ",Tamanho da Populacao," + Individuals.PopulationSize);
                writer.WriteLine("Selecionados," + Individuals.PopulationSize * 0.3 + ",Mutação, 0.10");
          
            }

            do
            {
                population = ag.Iniciate(population);

                using (StreamWriter writer = new StreamWriter("melhores.ods", true))
                {
                    writer.WriteLine("Geracao:," + g + ",Fitness," + population[0].getFitness());
                }
                g++;

            } while ((g < NGeracoes) && (population[0].getFitness() > 0));
            Output.scoreGenerator(population, "novapopulacao.ly");
        }
    }

}
