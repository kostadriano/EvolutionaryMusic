using System;

namespace musicaevolutiva
{
    public class PSO
    {

        public static int iteration = 20;
        Global gParticle = new Global();
        public void Start(Particles[] population)
        {
            try
            {
                int count = 0;
                gParticle.w = (float)0.9;

                while (count < iteration)
                {
                    for (int i = 0; i < Particles.PopulationSize; i++)
                    {
                        population[i].FitnessCalculate();

                        if (population[i].Fitness < population[i].PFitness)
                        {
                            population[i] = UpdateLocalMemory(population[i]);
                        }

                        if (population[i].Fitness < gParticle.GFitness)
                        {
                            gParticle = UpdateGlobalMemory(population[i], gParticle);
                        }

                        population[i] = SpeedCalculator(population[i]);

                        population[i] = UpdateIndividual(population[i]);


                    }

                    Output.FileWriter(count, gParticle);

                    ShowParticle(population[0]);

                    count++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        public void ShowParticle(Particles population)
        {
            for (int i = 0; i < Particles.Size; i++)
            {
                Console.Write(population.Notes[i]);
            }

            Console.WriteLine();

            for (int i = 0; i < Particles.Size; i++)
            {
                Console.Write(population.Times[i]);
            }
        }



        public Particles UpdateIndividual(Particles individual)
        {
            //Definir como converter resultados para valores discretos
            for (int j = 0; j < Particles.Size; j++)
            {
                individual.Times[j] += individual.PTimes[j];
                individual.Notes[j] += individual.PNotes[j];

            }

            return individual;
        }

        public Particles UpdateLocalMemory(Particles individual)
        {
            individual.PNotes = individual.Notes;
            individual.PTimes = individual.Times;
            individual.PFitness = individual.Fitness;

            return individual;
        }

        public Global UpdateGlobalMemory(Particles individual, Global gParticle)
        {
            gParticle.GNotes = individual.Notes;
            gParticle.GTimes = individual.Times;
            gParticle.GFitness = individual.Fitness;

            return gParticle;
        }
        public Particles SpeedCalculator(Particles individual)
        {
            // Constantes
            var constant = DefineConstants(gParticle, individual);
            // Calculo das diferenças

            var auxiliar1 = Difference(individual);
            var auxiliar2 = Difference(individual, gParticle);

            for (int i = 0; i < Particles.Size; i++)
            {
                individual.SpeedTimes[i] += constant.w * individual.SpeedTimes[i];
                individual.SpeedNotes[i] += constant.w * individual.SpeedNotes[i];

                for (int j = 0; j < Particles.Size; j++)
                {
                    individual.SpeedTimes[i] += constant.y1[j] * auxiliar1.differenceTimes[i];
                    individual.SpeedNotes[i] += constant.y1[j] * auxiliar1.differenceNotes[i];

                    individual.SpeedTimes[i] += constant.y2[j] * auxiliar2.differenceTimes[i];
                    individual.SpeedNotes[i] += constant.y2[j] * auxiliar2.differenceNotes[i];
                }


            }

            return individual;
        }

        public (float w, float[] y1, float[] y2) DefineConstants(Global gParticle, Particles individual)
        {

            Random rd = new Random();
            float wAuxiliar;
            wAuxiliar = gParticle.w * (float)0.95;

            gParticle.w = wAuxiliar < 0.001 ? (float)0.001 : wAuxiliar;

            for (int i = 0; i < Particles.Size; i++)
            {
                individual.y1[i] = (float)rd.Next(0, 100) / 100;
                individual.y2[i] = (float)rd.Next(0, 100) / 100;
            }


            return (gParticle.w, individual.y1, individual.y2);
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
