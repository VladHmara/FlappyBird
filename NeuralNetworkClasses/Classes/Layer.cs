﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkClasses.Classes
{
    public class Layer
    {
        public List<Neuron> Neurons { get; set; }

        public Layer()
        {
            Neurons = new List<Neuron>();
        }

        public Layer(int numNeurons) : this()
        {
            for (int i = 0; i < numNeurons; i++)
                Neurons.Add(new Neuron());
        }

        public Layer(Layer previousLayer, int numNeurons) : this()
        {
            for (int i = 0; i < numNeurons; i++)
                Neurons.Add(new Neuron(previousLayer));
        }

        public Layer(Layer previousLayer, List<double> genome) : this()
        {
            int num = previousLayer.Neurons.Count;
            for (int i = 0; i < genome.Count / num; i++)
                Neurons.Add(new Neuron(previousLayer, genome.GetRange(i * num, num)));
        }

        public List<double> GetData()
        {
            List<double> data = new List<double>();
            foreach (Neuron neuron in Neurons)
                data.Add(neuron.Data);
            return data;
        }

        public void SetData(List<double> data)
        {
            for (int i = 0; i < Neurons.Count; i++)
                Neurons[i].Data = data[i];
        }

        public List<double> GetGenome()
        {
            List<double> genome = new List<double>();
            foreach (Neuron neuron in Neurons)
                genome.AddRange(neuron.GetGenome());
            return genome;
        }

        public void SetGenome(List<double> genome)
        {
            int sum = 0;
            foreach (Neuron neuron in Neurons)
            {
                int num = neuron.Sinapses.Count;
                neuron.SetGenome(genome.GetRange(sum,num));
                sum += num;
            }
        }

        public void Handle()
        {
            foreach (Neuron neuron in Neurons)
                neuron.Activation();
        }
    }
}
