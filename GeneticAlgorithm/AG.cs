using System;

public class AG
{
    public Individuals[] Iniciate(Individuals[] population)
    {
        Individuals[] newPopulation = new Individuals[Individuals.PopulationSize];
        int tempRd;
        int k = 0; // controle de qual individuo esta cruzando

        for (int i = 0; i < Individuals.PopulationSize; i++)
        {
            char[] newNotes = new char[Individuals.Size];
            int[] newTimes = new int[Individuals.Size];

            newPopulation[i] = new Individuals();
            if ((k <= 47) && ((i % 2) == 0) && (i != 0))
            {
                k = k + 2;
            }

            for (int iNotes = 0; iNotes < Individuals.Size; iNotes++)
            {
                Random random = new Random();
                tempRd = random.Next(0, 2);

                if (tempRd == 0)
                {
                    newNotes[iNotes] = population[k].getNotesAt(iNotes);
                    newTimes[iNotes] = population[k].getTimesAt(iNotes);

                }
                else
                {
                    newNotes[iNotes] = population[k + 1].getNotesAt(iNotes);
                    newTimes[iNotes] = population[k + 1].getTimesAt(iNotes);

                }
            }
            newPopulation[i].setNotes(newNotes);
            newPopulation[i].setTimes(newTimes);
        }

        return Selection(population, Mutation(newPopulation));

    }

    public Individuals[] Mutation(Individuals[] newPopulation)
    {
        int[] sorteados = new int[Individuals.PopulationSize];
        int i = 0, temp;
        Random rd = new Random();
        do
        {
            temp = rd.Next(0, Individuals.PopulationSize);
            if (Array.IndexOf(sorteados, temp) == -1)
            {

                for (int j = 0; j < Individuals.Size; j++)
                {
                    if (rd.Next(0, 11) == 0)
                    {
                        newPopulation[temp].setTimesAt(Individuals.timeGenerate(), i);
                        newPopulation[temp].setNotesAt(Individuals.NoteNames[rd.Next(0, 7)], i);
                    }
                }
                sorteados[i] = temp;
                i++;
            };
        } while (i < (Individuals.PopulationSize * 0.1));

        for (int k = 0; k < newPopulation.Length; k++)
        {
            newPopulation[k].FitnessCalculate();
        }
        return OrderBy(newPopulation);
    }
    public Individuals[] Selection(Individuals[] population, Individuals[] newPopulation)
    {
        for (int i = 0; i < Individuals.PopulationSize * 0.3; i++)
            newPopulation[Individuals.PopulationSize - 1 - i] = population[i];

        return OrderBy(newPopulation);
    }
    public static Individuals[] OrderBy(Individuals[] pop)
    {

        Individuals aux;
        Individuals[] array = pop;
        int k = Individuals.PopulationSize - 1;

        for (int i = 0; i < Individuals.PopulationSize; i++)
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