using System;


public class Particles
{
    public const int Size = 20;
    public static string NoteNames = "abcdefg";
    public int[] Times { get; set; }
    public char[] Notes { get; set; }
    public float[] SpeedNotes { get; set; }
    public float[] SpeedTimes { get; set; }
    public char[] LocalMemoryNotes { get; set; }
    public int[] LocalMemoryTimes { get; set; }
    public static int PopulationSize;
    private int TotalTime;
    public float Fitness { get; set; }
    public float LocalMemoryFitness { get; set; }

    public Particles()
    {
        Notes = new char[Size];
        Times = new int[Size];
        LocalMemoryTimes = new int[Size];
        LocalMemoryNotes = new char[Size];
        SpeedTimes = new float[Size];
        SpeedNotes = new float[Size];
        TotalTime = 0;
        Fitness = 0;
    }

    public void Initiate()
    {
        Random rd = new Random();
        for (int i = 0; i < Size; i++)
        {
            Times[i] = timeGenerate();
            Notes[i] = NoteNames[rd.Next(0, 7)];
        }
    }

    public static Particles[] CreatePopulation(Particles[] population)
    {
        for (int i = 0; i < population.Length; i++)
        {
            population[i] = new Particles();
            population[i].Initiate();
            population[i] = Particles.FitnessCalculate(population[i]);
            population[i].LocalMemoryFitness = population[i].Fitness;
            population[i].LocalMemoryTimes = population[i].Times;
            population[i].LocalMemoryNotes = population[i].Notes;
        }

        return population;
    }
    public static int timeGenerate()
    {
        Random rd = new Random();
        return (int)Math.Pow(2, rd.Next(0, 5));
    }

    public static Particles FitnessCalculate(Particles particle)
    {
        particle.TotalTime = 0;
        float temp = 0;
        for (int i = 0; i < Size; i++)
        {
            particle.TotalTime += (int)(Math.Log(particle.Times[i], 2));
            temp += (float)(Math.Abs(Math.Log(particle.Times[i], 2) - Math.Log(Reference.Time[i], 2)) + Math.Abs(particle.Notes[i] - Reference.Note[i]));
        }
        temp += Math.Abs(Reference.totalTime - particle.TotalTime);
        particle.Fitness = temp;

        return particle;
    }

}
