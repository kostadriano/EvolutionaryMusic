using System;
public class Individuals
{
    public const int Size = 20;
    public static string NoteNames = "abcdefg";
    private int[] Times;
    private char[] Notes;
    public static int PopulationSize;
    private int TotalTime;
    private float Fitness;

    public Individuals()
    {
        Notes = new char[Individuals.Size];
        Times = new int[Individuals.Size];

    }
    public void Initiate()
    {
        TotalTime = 0;
        Fitness = 0;
        for (int i = 0; i < Size; i++)
        {
            Random rd = new Random();
            Times[i] = timeGenerate();
            Notes[i] = NoteNames[rd.Next(0, 7)];
        }
    }

    public static int timeGenerate()
    {
        Random rd = new Random();
        return (int)Math.Pow(2, rd.Next(0, 5));
    }
    public void FitnessCalculate()
    {
        float temp = 0;
        for (int i = 0; i < Size; i++)
        {
            TotalTime += (int)Times[i];
            temp += (Math.Abs(Times[i] - Reference.Time[i]) + Math.Abs(Notes[i] - Reference.Note[i]));
        }
        temp += Math.Abs(Reference.totalTime - TotalTime);
        Fitness = temp;
    }
    public string showConfiguration()
    {
        string ret = string.Empty;
        for (int i = 0; i < Size; i++)
        {
            ret += Notes[i] + "'" + Times[i] + " ";
        }

        return ret;
    }


    public int[] getTimes()
    {
        return Times;
    }

    public char[] getNotes()
    {
        return Notes;
    }

    public int getTimesAt(int i)
    {
        return Times[i];
    }

    public char getNotesAt(int i)
    {
        return Notes[i];
    }

    public void setNotes(char[] NewNotes)
    {
        Notes = NewNotes;
    }

    public void setTimes(int[] NewTimes)
    {
        Times = NewTimes;
    }

    public void setNotesAt(char NewNote, int i)
    {
        Notes[i] = NewNote;
    }

    public void setTimesAt(int NewTime, int i)
    {
        Times[i] = NewTime;
    }
    public float getFitness()
    {
        return Fitness;
    }
}