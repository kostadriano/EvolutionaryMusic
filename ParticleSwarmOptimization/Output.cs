using System;
using System.IO;

namespace musicaevolutiva
{
    public class Output
    {
        public static void FileWriter(int count, Global gParticle, int maxIteration)
        {
            if (count == 0)
            {
                using (StreamWriter writer = new StreamWriter("gBesttemp.ods", true))
                {
                    writer.WriteLine("Population Size," + Particles.PopulationSize + ",Generations," + maxIteration+"\n");
                }
            }

            using (StreamWriter writer = new StreamWriter("gBesttemp.ods", true))
            {
                writer.WriteLine("Generation," + count + ",Global Memory," + gParticle.GFitness);

                if (count == maxIteration - 1)
                    writer.WriteLine("\n");
            }
        }
    }
}