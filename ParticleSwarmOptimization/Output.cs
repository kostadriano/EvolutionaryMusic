using System;
using System.IO;

public class Output
{
    public static void scoreGenerator(Particles[] population, String path)
    {
        Stream arquivo = File.Open("entrada.txt", FileMode.Open);
        StreamReader leitor = new StreamReader(arquivo);
        string entrada = leitor.ReadToEnd();
        leitor.Close();
        for (int i = 0; i < 10; i++)
        {
            string tagName = String.Format("<name{0}>", i);
            string tagFit = String.Format("<fit{0}>", i);
            string tagNotes = String.Format("<notes{0}>", i);
            string stgNotes = population[i].showConfiguration();

            entrada = entrada.Replace(tagName, i.ToString()).Replace(tagFit, population[i].getFitness().ToString()).Replace(tagNotes, stgNotes);
        }

        Stream saida = File.Open(path, FileMode.Create);
        StreamWriter escritor = new StreamWriter(saida);
        escritor.WriteLine(entrada);
        escritor.Close();
        saida.Close();
    }

    public static void FileWriter(int count, Global gParticle)
    {


        if (count == 0)
        {
            using (StreamWriter writer = new StreamWriter("melhores.ods", true))
            {
                writer.WriteLine("Geracoes:," + count + ",Tamanho da Populacao," + Particles.PopulationSize);
                // writer.WriteLine("Selecionados," + Particles.PopulationSize * 0.3 + ",Mutação, 0.10");

            }
        }

        using (StreamWriter writer = new StreamWriter("melhores.ods", true))
        {
            writer.WriteLine("Geracao:," + count + ",Fitness," + gParticle.GFitness);
        }

    }


}