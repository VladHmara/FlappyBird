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
    public partial class Game : Form
    {
        List<Bird> birds = new List<Bird>();
        public Game()
        {
            InitializeComponent();

            for (int i = 0; i < 1; i++)
            {
                birds.Add(new Bird(this, Bird.Color.White));
            }

        }

        private void Button_Click(object sender, EventArgs e)
        {
            foreach (Bird b in birds)
            {
                b.Jump();
            }
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Bird bird in birds)
                bird.WagStop();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            foreach (Bird b in birds)
            {
                b.Fly();
            }
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
                foreach (Bird b in birds)
                {
                    b.Jump();
                }
        }

        private void Game_Load(object sender, EventArgs e)
        {
            foreach (Bird b in birds)
                b.WagStart();
            gameTimer.Start();
        }
    }
}
