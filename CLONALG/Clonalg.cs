using System;

public class Clonalg
{
    public static int PopulationSize { get; set; }
    public static double Beta { get; set; }
    public static int NumSelected { get; set; }
    public static int Generations { get; set; }

    public Clonalg(int generations,int populationsize, double beta, int numselected)
    {
        Generations = generations;
        PopulationSize = populationsize;
        Beta = beta;
        NumSelected = numselected;
    }

    public static void Start(Cell[] Cells){
        for(int i=0;i<Generations;i++){

        }
    }

    public static Cell[] Selection(Cell[] Cells)
    {
        Cell[] Selected = new Cell[NumSelected];
        Array.Copy(Cells,0,Selected,0,NumSelected);
        return null;
    }

    public static Cell[] Clone()
    {
        return null;
    }
    public static Cell Hypermutation()
    {

        return null;
    }
    public static Cell[] OrderBy(Cell[] pop)
    {

        Cell aux;
        Cell[] array = pop;
        int k = Cell.PopulationSize - 1;

        for (int i = 0; i < Cell.PopulationSize; i++)
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
        pop = array;
        return pop;
    }
}