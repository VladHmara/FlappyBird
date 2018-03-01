using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworkClasses.Classes
{
    public static class GeneticAlgorithm
    {
        public static void Evolution(ref List<IGenetic> items)
        {
            Selection(ref items);
            items[items.Count / 3 + 1].NeuralNetworkItem = Crossing(items[0].NeuralNetworkItem, items[1].NeuralNetworkItem);

        }

        private static void Selection(ref List<IGenetic> items)
        {
            //Something
            items.Sort((a, b) => b.Fintess.CompareTo(a.Fintess));
        }

        private static NeuralNetwork Crossing(NeuralNetwork parent1, NeuralNetwork parent2)
        {
            //mainGenome - parent1Genome
            List<double> parent1Genome = parent1.GetGenome();
            List<double> parent2Genome = parent2.GetGenome();
            int size = parent1Genome.Count;
            int CurrentIndex = 0;

            for (int i = 8; i < size; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Math.Abs(parent2Genome[CurrentIndex] - parent1Genome[j] - 0.001) <= 0.02)
                    {
                        if (++CurrentIndex >= size)
                            break;
                        j = -1;
                    }
                }
                if (CurrentIndex >= size)
                    break;
                parent1Genome[i] = parent2Genome[CurrentIndex++];
            }

            parent1.SetGenome(parent1Genome);

            return parent1;
        }

        private static void Mutation()
        {

        }
    }
}
