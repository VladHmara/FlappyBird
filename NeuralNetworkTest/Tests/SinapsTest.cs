using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkClasses.Classes;

namespace NeuralNetworkTest.Tests
{
    [TestClass]
    public class SinapsTest
    {
        [TestMethod]
        public void Sinaps_GetData_ExpectedCorrect()
        {
            // arrange 
            Neuron neuron = new Neuron()
            {
                Data = 0.156
            };
            Sinaps sinaps = new Sinaps(neuron);
            double expected = sinaps.Weight * 0.156;

            // act
            double actual = sinaps.GetData();

            // assert  
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sinaps_GetData2_ExpectedCorrect()
        {
            // arrange 
            Neuron neuron = new Neuron()
            {
                Data = 0.156
            };
            Sinaps sinaps = new Sinaps(neuron, 0.754);
            double expected = 0.754 * 0.156;

            // act
            double actual = sinaps.GetData();

            // assert  
            Assert.AreEqual(expected, actual);
        }
    }
}
