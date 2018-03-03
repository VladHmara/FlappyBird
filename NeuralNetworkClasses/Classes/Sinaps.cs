using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkClasses.Classes
{
    public class Sinaps
    {
        public double Weight { get; set; }
        public Neuron PreviousNeuron { get; set; }
        static private Random r = new Random();

        public Sinaps(Neuron previousNeuron)
        {
            PreviousNeuron = previousNeuron;
            Weight = r.NextDouble() * 2 - 1;
        }
        public Sinaps(Neuron previousNeuron, double weight)
        {
            PreviousNeuron = previousNeuron;
            Weight = weight;
        }

        public double GetData()
        {
            return PreviousNeuron.Data * Weight;
        }
    }
}
