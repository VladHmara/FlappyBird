using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkClasses.Classes;

namespace NeuralNetworkTest.Tests
{
    [TestClass]
    public class NeuronTest
    {
        [TestMethod]
        public void Neuron_Activation_CorrectLogic()
        {
            // arrange 
            Neuron neuron = new Neuron();
            Sinaps sinaps1 = new Sinaps(new Neuron() { Data = 0.5 }, 0.2);
            Sinaps sinaps2 = new Sinaps(new Neuron() { Data = 0.3 }, 0.4);
            neuron.Sinapses.Add(sinaps1);
            neuron.Sinapses.Add(sinaps2);
            double expected = 0;
            foreach (Sinaps sinaps in neuron.Sinapses)
                expected += sinaps.GetData();
            expected = (Math.Exp(2 * expected) - 1) / (Math.Exp(2 * expected) + 1);

            // act
            neuron.Activation();

            // assert  
            double actual = neuron.Data;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Neuron_ActivationWithWrongWeight_CorrectRange()
        {
            // arrange 
            Neuron neuron = new Neuron();
            Sinaps sinaps1 = new Sinaps(new Neuron() { Data = 0.5 }, 1000);
            Sinaps sinaps2 = new Sinaps(new Neuron() { Data = 0.3 }, 0.4);
            neuron.Sinapses.Add(sinaps1);
            neuron.Sinapses.Add(sinaps2);

            // act
            neuron.Activation();

            // assert  
            double actual = neuron.Data;
            Assert.IsTrue(actual >=- 1 && actual <= 1);
        }

        [TestMethod]
        public void Neuron_Activation_CorrectRange()
        {
            // arrange 
            Neuron neuron = new Neuron();
            Sinaps sinaps1 = new Sinaps(new Neuron() { Data = 0.5 }, 0.5);
            Sinaps sinaps2 = new Sinaps(new Neuron() { Data = 0.3 }, 0.4);
            neuron.Sinapses.Add(sinaps1);
            neuron.Sinapses.Add(sinaps2);

            // act
            neuron.Activation();

            // assert  
            double actual = neuron.Data;
            Assert.IsTrue(actual >= -1 && actual <= 1);
        }

    }
}
