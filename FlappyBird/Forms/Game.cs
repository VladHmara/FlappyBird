using NeuralNetworkClasses.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Game : Form
    {
        public static Game mainForm;
        public Game()
        {
            InitializeComponent();
            mainForm = this;
            for (int i = 0; i < 10; i++)
            {
                new Bird(Bird.Color.White);
            }

            for (int i = 0; i < 4; i++)
            {
                new Tree();
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            foreach (Bird b in Bird.items)
            {
                b.Jump();
            }
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Bird bird in Bird.items)
            {
                bird.WagStop();

            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            //coolizies
            //BirdDie        
            foreach (Bird b in Bird.items)
            {
                listBox1.Items.Add(b.Fintess.ToString());

                foreach (Tree tree in Tree.items)
                {

                    if (b.pictureBox.Bounds.IntersectsWith(tree.pbTreeBottom.Bounds) || b.pictureBox.Bounds.IntersectsWith(tree.pbTreeTop.Bounds))
                        b.Dead();
                }
            }

            if (Tree.targetOfBird.pbTreeTop.Left <= 110)
            {
                List<Tree> trees = Tree.items.FindAll(p => !p.Equals(Tree.targetOfBird));
                Tree.targetOfBird = trees[0];
                foreach (Tree t in trees)
                    if (t.pbTreeTop.Left < Tree.targetOfBird.pbTreeTop.Left)
                        Tree.targetOfBird = t;
            }

            foreach (Bird b in Bird.items)
            {
                b.Fly();
            }

            foreach (Tree t in Tree.items)
            {
                t.Move();
            }

            label1.Text = Tree.targetOfBird.pbTreeTop.Left.ToString();
            label1.Left = Tree.targetOfBird.pbTreeTop.Left;
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
                foreach (Bird b in Bird.items)
                {
                    b.Jump();
                }
        }

        private void Game_Load(object sender, EventArgs e)
        {
            StartGame();
        }

        private void StartGame()
        {
            foreach (Bird b in Bird.items)
                b.Born();
            foreach (Tree t in Tree.items)
                t.startPosition();
            gameTimer.Start();
        }

        public void StopGame()
        {
            gameTimer.Stop();
            //Playing in God
            GeneticAlgorithm.Evolution(ref ((IEnumerable<IGenetic>)Bird.items));


            StartGame();
        }
    }
}