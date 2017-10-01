using System;

namespace musicaevolutiva
{
    public class PSO
    {

        public int maxIteration;
        Global globalMemory = new Global();

        public PSO(int iteration)
        {
            maxIteration = iteration;
        }

        public int GlobalBest(Particles[] population)
        {
            int small = 0;
            for (int i = 1; i < Particles.PopulationSize; i++)
            {
                if (population[i].Fitness < population[small].Fitness)
                    small = i;
            }

            return small;
        }
        public void Start(Particles[] population)
        {

            int iteration = 0;
            globalMemory.w = (float)0.9;
            int gBest = GlobalBest(population);
            globalMemory.GFitness = population[gBest].Fitness;
            globalMemory.GTimes = population[gBest].Times;
            globalMemory.GNotes = population[gBest].Notes;


            while (iteration < maxIteration)
            {
                Console.WriteLine(globalMemory.GFitness);
                for (int i = 0; i < Particles.PopulationSize; i++)
                {
                    if (population[i].Fitness < population[i].PFitness)
                    {
                        population[i] = UpdateLocalMemory(population[i]);
                    }

                    if (population[i].Fitness < globalMemory.GFitness)
                    {
                        globalMemory = UpdateGlobalMemory(population[i], globalMemory);
                    }
                    population[i] = SpeedCalculate(population[i]);
                    for(int j=0;j<Particles.Size;j++)
                    Console.Write(population[i].SpeedNotes[j]+" ");
                    
                    Console.WriteLine();
                    population[i] = UpdateParticle(population[i]);

                    population[i] = Particles.FitnessCalculate(population[i]);

                }

                Output.FileWriter(iteration, globalMemory);

                //ShowParticle(population[0]);

                iteration++;


            }
        }

        public void ShowParticle(Particles particle)
        {
            for (int i = 0; i < Particles.Size; i++)
            {
                Console.Write(particle.Notes[i] + " ");
            }

            Console.WriteLine();

            for (int i = 0; i < Particles.Size; i++)
            {
                Console.Write(particle.Times[i] + " ");
            }
        }


        public Particles UpdateParticle(Particles particle)
        {
            Random rd = new Random();


            for (int i = 0; i < Particles.Size; i++)
            {
                if (particle.SpeedNotes[i] > 3 && particle.Notes[i] < 'g')
                    particle.Notes[i] = Particles.NoteNames[(Particles.NoteNames.IndexOf(particle.Notes[i]) + 1)];
                if (particle.SpeedNotes[i] < -3 && particle.Notes[i] > 'a')
                    particle.Notes[i] = Particles.NoteNames[(Particles.NoteNames.IndexOf(particle.Notes[i]) - 1)];

                if (particle.SpeedTimes[i] > 3 && particle.Times[i] < 32)
                    particle.Times[i] = (int)Math.Pow(2, ((int)Math.Log(particle.Times[i], 2) + 1));
                if (particle.SpeedTimes[i] < -3 && particle.Times[i] > 1)
                    particle.Times[i] = (int)Math.Pow(2, ((int)Math.Log(particle.Times[i], 2) - 1));
            }
            return particle;
        }

        public Particles UpdateLocalMemory(Particles particle)
        {
            particle.PNotes = particle.Notes;
            particle.PTimes = particle.Times;
            particle.PFitness = particle.Fitness;
            return particle;
        }

        public Global UpdateGlobalMemory(Particles particle, Global globalMemory)
        {
            globalMemory.GNotes = particle.Notes;
            globalMemory.GTimes = particle.Times;
            globalMemory.GFitness = particle.Fitness;

            return globalMemory;
        }

        public int Log(int x)
        {
            return (int)Math.Log(x, 2);
        }

        public Particles SpeedCalculate(Particles individual)
        {
            Random random = new Random();

            for (int i = 0; i < Particles.Size; i++)
            {
                individual.SpeedTimes[i] += ((float)0.3 * (float)random.NextDouble() * Difference(individual).differenceTimes[i]) + ((float)0.7 * (float)random.NextDouble() * Difference(individual, globalMemory).differenceTimes[i]);
                individual.SpeedNotes[i] += ((float)0.3 * (float)random.NextDouble() * Difference(individual).differenceNotes[i]) + ((float)0.7 * (float)random.NextDouble() * Difference(individual, globalMemory).differenceNotes[i]);
            }
            return individual;
        }


        public (int[] differenceTimes, int[] differenceNotes) Difference(Particles particle)
        {
            int[] differenceTimes = new int[Particles.Size];
            int[] differenceNotes = new int[Particles.Size];
            for (int i = 0; i < Particles.Size; i++)
            {
                differenceTimes[i] = Log(particle.PTimes[i]) - Log(particle.Times[i]);
                differenceNotes[i] = particle.PNotes[i] - particle.Notes[i];
            }

            return (differenceTimes, differenceNotes);
        }

        public (int[] differenceTimes, int[] differenceNotes) Difference(Particles particle, Global globalMemory)
        {

            int[] differenceTimes = new int[Particles.Size];
            int[] differenceNotes = new int[Particles.Size];


            for (int i = 0; i < Particles.Size; i++)
            {
                differenceTimes[i] = Log(particle.PTimes[i]) - Log(globalMemory.GTimes[i]);
                differenceNotes[i] = (particle.PNotes[i] - globalMemory.GNotes[i]);
            }

            return (differenceTimes, differenceNotes);
        }

    }
}
