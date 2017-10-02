using System;

namespace musicaevolutiva
{
    public class PSO
    {

        public int maxIteration;
        public static double C1;
        public static double C2;
        Global globalMemory = new Global();

        public PSO(int iteration, int sizePopulation, double c1, double c2)
        {
            C1 = c1;
            C2 = c2;
            Particles.PopulationSize = sizePopulation;
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
            int gBest = GlobalBest(population);
            globalMemory.GFitness = population[gBest].Fitness;
            globalMemory.GTimes = population[gBest].Times;
            globalMemory.GNotes = population[gBest].Notes;


            while (iteration < maxIteration)
            {
                for (int i = 0; i < Particles.PopulationSize; i++)
                {
                    if (population[i].Fitness < population[i].PFitness)
                    {
                        UpdateLocalMemory(population[i]);
                    }
                    SpeedCalculate(population[i]);
                }
                population = UpdateParticles(population);

                gBest = (int)population[GlobalBest(population)].Fitness;
                globalMemory.GFitness = globalMemory.GFitness > gBest ? gBest : globalMemory.GFitness;

                Output.FileWriter(iteration, globalMemory, maxIteration);
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

        public int Direction(int a, int b)
        {
            return a - b == 0 ? 0 : a - b > 0 ? -1 : 1;
        }

        public Particles[] UpdateParticles(Particles[] particles)
        {
            Random rd = new Random();
            int direction = 0;

            for (int j = 0; j < Particles.PopulationSize; j++)
            {
                for (int i = 0; i < Particles.Size; i++)
                {
                    direction = Direction(particles[j].Times[i], Reference.Time[i]);

                    if (particles[j].Times[i] >= 1 && particles[j].Times[i] <= 32 && rd.NextDouble() > 0.9)
                        particles[j].Times[i] = (int)Math.Pow(2, ((int)Math.Log(particles[j].Times[i], 2) + direction));

                    direction = Direction(particles[j].Notes[i], Reference.Note[i]);

                    if (particles[j].Notes[i] <= 'g' && particles[j].Notes[i] >= 'a' && rd.NextDouble() > 0.9)
                        particles[j].Notes[i] = Particles.NoteNames[(Particles.NoteNames.IndexOf(particles[j].Notes[i]) + direction)];

                }
                particles[j] = Particles.FitnessCalculate(particles[j]);
            }
            return particles;
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
                individual.SpeedTimes[i] += ((float)C1 * (float)random.NextDouble() * Difference(individual).differenceTimes[i]) + ((float)C2 * (float)random.NextDouble() * Difference(individual, globalMemory).differenceTimes[i]);
                individual.SpeedNotes[i] += ((float)C1 * (float)random.NextDouble() * Difference(individual).differenceNotes[i]) + ((float)C2 * (float)random.NextDouble() * Difference(individual, globalMemory).differenceNotes[i]);
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
