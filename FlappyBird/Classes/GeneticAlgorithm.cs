using NeuralNetworkClasses.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird
{
    public static class GeneticAlgorithm
    {
        static private Random r = new Random();
        public static void Evolution(ref List<Bird> items)
        {
            Selection(ref items);

            //for (int i = items.Count / 3 + 1; i < items.Count; i++)
            //{
            //    luckyWay - way how birds will be Crossing
            //    int luckyWay = r.Next(1, 7);

            //    switch (luckyWay)
            //    {
            //        case 1: { items[i].NeuralNetworkItem = Crossing(items[0].NeuralNetworkItem, items[1].NeuralNetworkItem); break; }
            //        case 2: { items[i].NeuralNetworkItem = Crossing(items[0].NeuralNetworkItem, items[2].NeuralNetworkItem); break; }
            //        case 3: { items[i].NeuralNetworkItem = Crossing(items[0].NeuralNetworkItem, items[3].NeuralNetworkItem); break; }
            //        case 4: { items[i].NeuralNetworkItem = Crossing(items[1].NeuralNetworkItem, items[2].NeuralNetworkItem); break; }
            //        case 5: { items[i].NeuralNetworkItem = Crossing(items[1].NeuralNetworkItem, items[3].NeuralNetworkItem); break; }
            //        case 6: { items[i].NeuralNetworkItem = Crossing(items[2].NeuralNetworkItem, items[3].NeuralNetworkItem); break; }

            //    }
            //}
            items[4].NeuralNetworkItem = Crossing(items[0].NeuralNetworkItem, items[1].NeuralNetworkItem);
            items[5].NeuralNetworkItem = Crossing(items[0].NeuralNetworkItem, items[2].NeuralNetworkItem);
            items[6].NeuralNetworkItem = Crossing(items[0].NeuralNetworkItem, items[3].NeuralNetworkItem);
            items[7].NeuralNetworkItem = Crossing(items[1].NeuralNetworkItem, items[2].NeuralNetworkItem);
            items[8].NeuralNetworkItem = Crossing(items[1].NeuralNetworkItem, items[3].NeuralNetworkItem);
            items[9].NeuralNetworkItem = Crossing(items[2].NeuralNetworkItem, items[3].NeuralNetworkItem);
            Mutation(ref items);


        }

        private static void Selection(ref List<Bird> items)
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

            int randomIndex = r.Next(0, parent1Genome.Count);
            int currentIndex = 0;
            for (int i = randomIndex; i < size; i++)
            {
                for (int j = 0; j < randomIndex; j++)
                {
                    if (currentIndex >= size)
                        break;
                    if (Math.Abs(parent2Genome[currentIndex] - parent1Genome[j] - 0.001) <= 0.02)
                    {
                      currentIndex++;
                        j = -1;
                    }
                }
                if (currentIndex >= size)
                    break;
                parent1Genome[i] = parent2Genome[currentIndex++];
            }
            // 2 выходных, 6 внутренних, 1 выходных
            NeuralNetwork NetworkNew = new NeuralNetwork(2,6,1);
            NetworkNew.SetGenome(parent1Genome);

            return NetworkNew;
        }

        private static void Mutation(ref List<Bird> items)
        {

            //Коефициент мутирующих особей
            double kMutation = 0.1;
            //Коефициент приращивания
            double kAdd = r.NextDouble();

            //Всего  мутированных особей
            for (int j = 4; j < items.Count; j++)
            {
                List<double> genome = items[j].NeuralNetworkItem.GetGenome();
                for (int i = 0; i < genome.Count * kMutation; i++)
                {
                    int curerntGenome = r.Next(0, genome.Count);

                    if (r.Next(0, 2) == 0)
                        genome[curerntGenome] += genome[curerntGenome] + kAdd <= 1 ? kAdd : -1 * kAdd;
                    else
                        genome[curerntGenome] -= genome[curerntGenome] - kAdd >= 0 ? kAdd : -1 * kAdd;
                }
                items[j].NeuralNetworkItem.SetGenome(genome);
            }

        }
    }
}
