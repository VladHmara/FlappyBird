using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkClasses.Classes
{
    public class NeuralNetwork
    {
        public List<Layer> Layers { get; set; }

        public NeuralNetwork()
        {
            Layers = new List<Layer>();
        }

        public NeuralNetwork(params int[] numNeuronsInLayers) : this()
        {
            foreach (int num in numNeuronsInLayers)
                if (Layers.Count == 0)
                    Layers.Add(new Layer(num));
                else
                    Layers.Add(new Layer(Layers[Layers.Count - 1], num));
        }

        public NeuralNetwork(List<double> genome, params int[] numNeuronsInLayers) : this()
        {
            int sum = 0;
            foreach (int num in numNeuronsInLayers)
            {
                if (Layers.Count == 0)
                    Layers.Add(new Layer(num));
                else
                    Layers.Add(new Layer(Layers[Layers.Count - 1], genome.GetRange(sum, num)));
                sum += num;
            }
        }

        public List<double> GetGenome()
        {
            List<double> genome = new List<double>();
            foreach (Layer layer in Layers)
                genome.AddRange(layer.GetGenome());
            return genome;
        }

        public void SetGenome(List<double> genome)
        {
            int sum = 0;
            foreach (Layer layer in Layers)
            {
                int num = layer.Neurons.Count;
                layer.SetGenome(genome.GetRange(sum, num));
                sum += num;
            }
        }

        public List<double> Handle(List<double> input)
        {
                Layers[0].SetData(input);
                foreach (Layer layer in Layers)
                    layer.Handle();
                return Layers[Layers.Count - 1].GetData();
        }
    }
}
