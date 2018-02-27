using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkClasses.Classes;

namespace NeuralNetworkTest.Tests
{
    [TestClass]
    public class NeuralNetworkTest
    {
        [TestMethod]
        public void NeuralNetworkCreateTest()
        {
            NeuralNetwork nn = new NeuralNetwork();
        }
    }
}
