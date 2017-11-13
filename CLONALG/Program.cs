using System;
using System.IO;

namespace musicaevolutiva
{
    class Program
    {
        static void Main(string[] args)
        {
            Clonalg Clonalg = new Clonalg(10,10, 0.1, 4);

            Cell[] Cells = new Cell[Clonalg.PopulationSize];
            for (int i = 0; i < Clonalg.PopulationSize; i++)
            {
                Cells[i] = new Cell();
                Cells[i].Initiate();
                Cells[i].FitnessCalculate();

            }

            Clonalg.Start(Cells);

        }
    }

}
