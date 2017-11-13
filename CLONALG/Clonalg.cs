using System;

public class Clonalg
{
    public static float infinity = 564897987987987;
    public static int PopulationSize { get; set; }
    public static double Beta { get; set; }
    public static int NumSelected { get; set; }
    public static int Generations { get; set; }

    public Clonalg(int generations, int populationsize, double beta, int numselected)
    {
        Generations = generations;
        PopulationSize = populationsize;
        Beta = beta;
        NumSelected = numselected;
    }

    public static void Start(Cell[] Cells)
    {
        for (int i = 0; i < Generations; i++)
        {

            Cells = OrderBy(Cells);
            //Console.WriteLine(Cells[0].getFitness());

            Cell[] Clones = Clone(Selection(Cells));
            Clones = Hypermutation(Clones);
            //Console.WriteLine(Clones[0].getFitness());
            
            Clones = ClonesFitness(Clones);
            //Console.WriteLine(Clones[0].getFitness());

        //Clones = NegativeSelection(Clones);
          //  Console.WriteLine(Clones[0].getFitness());

            Clones = OrderBy(Clones);
            //Console.WriteLine(Clones[0].getFitness());

            Cells = Repopulation(Cells, Clones);
            Cells = OrderBy(Cells);
            
            Console.WriteLine(Cells[0].getFitness());
        }

    }

    public static Cell[] ClonesFitness(Cell[] Clones)
    {
        for (int j = 0; j < Clones.Length; j++)
        {
            Clones[j].FitnessCalculate();
        }
        return Clones;
    }
    public static Cell[] Selection(Cell[] Cells)
    {
        Cell[] Selected = new Cell[NumSelected];
        Array.Copy(Cells, 0, Selected, 0, NumSelected);
        return Selected;
    }

    public static Cell[] Clone(Cell[] Cells)
    {
        Cell[] Clones = new Cell[NumSelected * 4];
        int index = 0;
        int k = 1;
        for (int i = 0; i < Clones.Length; i++)
        {
            Clones[i] = Cells[index];

            if (k % 4 == 0)
                index++;
            k++;
        }
        return Clones;
    }
    public static Cell[] Hypermutation(Cell[] Clones)
    {
        Random Random = new Random();

        for (int i = 0; i < Clones.Length; i++)
        {
            int numAlter = (int)Math.Round(Clones[i].getFitness() * Beta);
            for (int j = 0; j < numAlter; j++)
            {
                int rd = Random.Next(0, 20);
                Clones[i].setTimesAt(Cell.timeGenerate(), rd);
                Clones[i].setNotesAt(Cell.NoteNames[Random.Next(0, 7)], rd);
            }
            Clones[i].FitnessCalculate();
        }
        return Clones;
    }
    public static Cell[] OrderBy(Cell[] array)
    {
        Cell aux;
        int k = PopulationSize - 1;

        for (int i = 0; i < PopulationSize; i++)
        {
            for (int j = 0; j < k; j++)
            {
                if ((array[j].getFitness()) > (array[j + 1].getFitness()))
                {
                    aux = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = aux;
                }
            }
            k--;
        }
        return array;
    }
    public static Cell[] NegativeSelection(Cell[] Clones)
    {
        for (int i = 0; i < Clones.Length - 1; i++)
        {
            for (int j = i + 1; j < Clones.Length; j++)
            {
                if ((Clones[i].getFitness() == Clones[j].getFitness()) && (Clones[i].getFitness() != infinity)&&(Clones[j].getFitness() != infinity))
                {
                    Clones[j].setFitness(infinity);
                }
            }
        }
        return Clones;
    }
    public static Cell[] Repopulation(Cell[] Cells, Cell[] Clones)
    {
        Cell[] temp = new Cell[NumSelected];
        Array.Copy(Clones, 0, temp, 0, NumSelected);
        Array.Copy(temp, 0, Cells, Cells.Length - NumSelected, NumSelected);

        return Cells;
    }
}