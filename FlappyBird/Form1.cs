using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        List<Bird> birds = new List<Bird>();
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 100; i++)
            {
                birds.Add(new Bird(this, Bird.Color.White));
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Bird bird in birds)
                bird.WagStart();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Bird bird in birds)
                bird.WagStop();
        }
    }
}
