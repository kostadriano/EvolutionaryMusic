using System;


public class Particles
{
    public const int Size = 20;
    public static string NoteNames = "abcdefg";
    public int[] Times { get; set; }
    public char[] Notes { get; set; }
    public float[] SpeedNotes { get; set; }
    public float[] SpeedTimes { get; set; }
    public char[] PNotes { get; set; }
    public int[] PTimes { get; set; }
    public static int PopulationSize;
    private int TotalTime;
    public float Fitness { get; set; }
    public float PFitness { get; set; }
    public float[] y1 { get; set; }
    public float[] y2 { get; set; }

    public Particles()
    {
        Notes = new char[Size];
        Times = new int[Size];
        PTimes = new int[Size];
        PNotes = new char[Size];
        SpeedTimes = new float[Size];
        SpeedNotes = new float[Size];
        y1 = new float[Size];
        y2 = new float[Size];
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
        // inicializando memória local

    }

    public static int timeGenerate()
    {
        Random rd = new Random();
        return (int)Math.Pow(2, rd.Next(0, 5));
    }

    public static Particles FitnessCalculate(Particles particle)
    {
        particle.TotalTime=0;
        float temp = 0;
        for (int i = 0; i < Size; i++)
        {
            particle.TotalTime += (int)(Math.Log(particle.Times[i],2));
            temp += (float)(Math.Abs(Math.Log(particle.Times[i],2) - Math.Log(Reference.Time[i],2)) + Math.Abs(particle.Notes[i] - Reference.Note[i]));
        }
        temp += Math.Abs(Reference.totalTime - particle.TotalTime);
        particle.Fitness = temp;
        
        return particle;
    }
    public static string showConfiguration(int[] array, char[] array2)
    {
        string ret = string.Empty;
        for (int i = 0; i < Size; i++)
        {
            ret += array2[i] + "'" + array[i] + " ";
        }
        return ret;
    }
}
