using System;
using System.IO;

namespace musicaevolutiva
{
    class Program
    {
        static void Main(string[] args)
        {
            Clonalg Clonalg = new Clonalg(10,50, 0.4, 10);

            Cell[] Cells = new Cell[Cell.PopulationSize];
            for (int i = 0; i < Clonalg.PopulationSize; i++)
            {
                Cells[i] = new Cell();
                Cells[i].Initiate();
                Cells[i].FitnessCalculate();
            }
            Cells = Clonalg.OrderBy(Cells);

            Clonalg.Start(Cells);

        }
    }

}
