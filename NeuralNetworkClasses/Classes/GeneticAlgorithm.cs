using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworkClasses.Classes
{
    public static class GeneticAlgorithm
    {
        static private Random r = new Random();
        public static void Evolution(ref List<IGenetic> items)
        {
            Selection(ref items);

            for (int i = items.Count / 3 + 1; i < items.Count; i++)
            {
                if (r.Next(0, 1) == 0)
                    items[i].NeuralNetworkItem = Crossing(items[0].NeuralNetworkItem, items[1].NeuralNetworkItem);
                else
                    items[i].NeuralNetworkItem = Crossing(items[2].NeuralNetworkItem, items[3].NeuralNetworkItem);

            }

            Mutation(ref items);


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

            int randomIndex = r.Next(0, parent1Genome.Count - 1);
            int currentIndex = 0;
            for (int i = randomIndex; i < size; i++)
            {
                for (int j = 0; j < randomIndex; j++)
                {
                    if (Math.Abs(parent2Genome[currentIndex] - parent1Genome[j] - 0.001) <= 0.02)
                    {
                        if (++currentIndex >= size)
                            break;
                        j = -1;
                    }
                }
                if (currentIndex >= size)
                    break;
                parent1Genome[i] = parent2Genome[currentIndex++];
            }

            parent1.SetGenome(parent1Genome);

            return parent1;
        }

        private static void Mutation(ref List<IGenetic> items)
        {

            //Коефициент мутирующих особей
            double kMutation = 0.1;
            //Коефициент приращивания
            double kAdd = r.NextDouble() * 0.1;

            //Всего  мутированных особей
            for (int i = 0; i < items.Count * kMutation; i++)
            {
                int current = r.Next(0, items.Count - 1);

                List<double> genome = items[current].NeuralNetworkItem.GetGenome();
                int curerntGenome = r.Next(0, genome.Count - 1);

                if (r.Next(0, 1) == 0)
                    genome[curerntGenome] += genome[curerntGenome] + kAdd <= 1 ? kAdd : -1 * kAdd;
                else
                    genome[curerntGenome] -= genome[curerntGenome] - kAdd >= -1 ? kAdd : -1 * kAdd;

                items[current].NeuralNetworkItem.SetGenome(genome);

            }

        }
    }
}
