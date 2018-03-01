using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkClasses.Classes
{
    public interface IGenetic
    {
        NeuralNetwork NeuralNetworkItem { get; set; }
        long Fintess { get; set; }
    }
}
