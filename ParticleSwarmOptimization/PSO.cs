using System;

namespace musicaevolutiva
{
    public class PSO
    {

        Particles[] newPopulation = new Particles[Particles.PopulationSize];
         int iteration = 20;
        Global gParticle = new Global();
        public void Start() // alterar parametro
        {
            int count = 0;
            while (count < iteration) // estabelecer critério de parada
            {
                for (int i = 0; i < Particles.PopulationSize; i++)
                {
                    newPopulation[i].FitnessCalculate();

                    if (newPopulation[i].Fitness < newPopulation[i].PFitness)
                    {
                        UpdateLocalMemory(newPopulation[i]);
                    }

                    if (newPopulation[i].Fitness < gParticle.GFitness)
                    {
                        UpdateGlobalMemory(newPopulation[i], gParticle);
                    }

                    SpeedCalculator(newPopulation[i]);

                    UpdateIndividual(newPopulation[i]);


                }

                count++;
            }
        }

        public void UpdateIndividual(Particles individual)
        {
            //Definir como converter resultados para valores discretos
                    for (int j = 0; j < Particles.Size; j++)
                    {
                        individual.Times[j] += individual.PTimes[j];
                        individual.Notes[j] += individual.PNotes[j];
                    }
        }

        public void UpdateLocalMemory(Particles individual)
        {
            individual.PNotes = individual.Notes;
            individual.PTimes = individual.Times;
            individual.PFitness = individual.Fitness;
        }

        public void UpdateGlobalMemory(Particles individual, Global globalIndividual)
        {
            globalIndividual.GNotes = individual.Notes;
            globalIndividual.GTimes = individual.Times;
            globalIndividual.GFitness = individual.Fitness;
        }
        public void SpeedCalculator(Particles individual)
        {
            Random rd = new Random();
            float w, y1, y2;
            w = rd.Next(0, 100) / 100;
            y1 = rd.Next(0, 100) / 100;
            y2 = rd.Next(0, 100) / 100;

            // Calculo das diferenças
            var auxiliar1 = Difference(individual);
            var auxiliar2 = Difference(individual, gParticle);

            for (int i = 0; i < Particles.Size; i++)
            {
                individual.SpeedTimes[i] += w * individual.SpeedTimes[i];
                individual.SpeedNotes[i] += w * individual.SpeedNotes[i];

                individual.SpeedTimes[i] += y1 * auxiliar1.differenceTimes[i];
                individual.SpeedNotes[i] += y1 * auxiliar1.differenceNotes[i];

                individual.SpeedTimes[i] += y2 * auxiliar2.differenceTimes[i];
                individual.SpeedNotes[i] += y2 * auxiliar2.differenceNotes[i];
            }
        }


        public (float[] differenceTimes, int[] differenceNotes) Difference(Particles individual)
        {
            float[] differenceTimes = new float[Particles.Size];
            int[] differenceNotes = new int[Particles.Size]; // depois converter pra letras

            for (int i = 0; i < Particles.Size; i++)
            {
                differenceTimes[i] = individual.PTimes[i] - individual.Times[i];
                differenceNotes[i] = individual.PNotes[i] - individual.Notes[i];
            }

            return (differenceTimes, differenceNotes);
        }

        public (float[] differenceTimes, int[] differenceNotes) Difference(Particles individual, Global globalMemory)
        {

            float[] differenceTimes = new float[Particles.Size];
            int[] differenceNotes = new int[Particles.Size]; // depois converter pra letras


            for (int i = 0; i < Particles.Size; i++)
            {
                differenceTimes[i] = individual.PTimes[i] - globalMemory.GTimes[i];
                differenceNotes[i] = individual.PNotes[i] - globalMemory.GNotes[i];
            }

            return (differenceTimes, differenceNotes);
        }

    }
}