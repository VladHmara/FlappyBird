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
        public Game()
        {
            InitializeComponent();

            for (int i = 0; i < 10; i++)
            {
                new Bird(this, Bird.Color.White);
            }

            for (int i = 0; i < 4; i++)
            {
                new Tree(this);
                Thread.Sleep(500);
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
                bird.WagStop();
        }


        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //coolizies
            //BirdDie        
            //Thread thread1 = new Thread(() => {
            foreach (Bird b in Bird.items)
            {
                foreach (Tree tree in Tree.items)
                {
                    if (b.pictureBox.Bounds.IntersectsWith(tree.pbTreeBottom.Bounds) || b.pictureBox.Bounds.IntersectsWith(tree.pbTreeTop.Bounds))
                        b.Dead();
                }
            }
        //});
        //    thread1.Start(); 

            //Thread thread2 = new Thread(() =>  {
                if (Tree.targetOfBird.pbTreeTop.Left <= 200)
                {
                    List<Tree> trees = Tree.items.FindAll(p => !p.Equals(Tree.targetOfBird));
                    Tree.targetOfBird = trees[0];
                    foreach (Tree t in trees)
                        if (t.pbTreeTop.Left < Tree.targetOfBird.pbTreeTop.Left)
                            Tree.targetOfBird = t;
                }
            //});
            //thread2.Start();

            foreach (Bird b in Bird.items)
            {
                b.Fly();
            }

            foreach (Tree t in Tree.items)
            {
                t.Move();
            }

            //thread2.Join();
            label1.Text = Tree.targetOfBird.pbTreeTop.Left.ToString();
            label1.Left = Tree.targetOfBird.pbTreeTop.Left;
            //thread1.Join();
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
            foreach (Bird b in Bird.items)
                b.WagStart();
            gameTimer.Start();
        }
    }
}