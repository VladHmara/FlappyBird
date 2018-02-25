using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird.Neural_Network
{
    class Sinaps
    {
        public double Weight { get; set; }
        public Neuron PreviousNeuron { get; set; }
        static Random Random { get; set; }

        static Sinaps()
        {
            Random = new Random();
        }

        public Sinaps(Neuron previousNeuron)
        {
            PreviousNeuron = previousNeuron;
            Random random = new Random();
            Weight = Random.NextDouble() * 2 - 1;
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
