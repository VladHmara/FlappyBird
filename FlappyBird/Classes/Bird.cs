using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Interop;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using NeuralNetworkClasses.Classes;

namespace FlappyBird
{
    public partial class Bird : IGenetic
    {
        static public List<Bird> items = new List<Bird>();
        static int aliveBird;
        static Bitmap[] birdImgs = new Bitmap[20];

        public double TopPosition { get; set; }
        public int Counter { get; set; }
        public NeuralNetwork NeuralNetworkItem { get; set; }
        public long Fintess { get; set; }


        Bitmap[] birdImg = new Bitmap[2];
        public PictureBox pictureBox;
        private Thread thread;
        public bool isAlive;

        public enum Color
        {
            Yellow,
            Red,
            Blue,
            Green,
            White,
            Black,
            Brown,
            Purple,
            Rose,
            Orange
        }


        public Bird(Color c)
        {
            birdImg[0] = birdImgs[(int)c * 2];
            birdImg[1] = birdImgs[(int)c * 2 + 1];
            TopPosition = 100;
            pictureBox = new PictureBox
            {
                Location = new System.Drawing.Point(200, (int)TopPosition),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Image = birdImg[0]
            };
            //Max distance
            Fintess = pictureBox.Location.X;
            NeuralNetworkItem = new NeuralNetwork(2,6,1);
            isAlive = true;
            Game.mainForm.Controls.Add(pictureBox);
            Counter = 0;
            items.Add(this);
        }

        static Bird()
        {
            Bitmap bmp = new Bitmap(@"Textures\img_bird.png");
            birdImgs = ImgWorker.SplitImage(bmp, 2, 10).ToArray();
            aliveBird = 0;
        }

        public void WagStart()
        {
            if (isAlive)
            {
                WagStop();
                thread = new Thread(new ThreadStart(() =>
                {
                    int num = 1;
                    while (true)
                    {
                        num = num == 0 ? 1 : 0;
                        pictureBox.BeginInvoke((MethodInvoker)(() =>
                        {
                            pictureBox.Image = birdImg[num];
                        }));
                        Thread.Sleep(200);
                    }
                }));
                thread.Start();
            }
        }

        public void WagStop()
        {
            if (thread != null && thread.IsAlive)
                thread.Abort();
        }


        //Fly
        public void Fly()
        {
            if (isAlive)
            {
                List<double> data = new List<double>() { 1/(Tree.targetOfBird.pbTreeTop.Left - 128), 1/(Tree.targetOfBird.pbTreeTop.Top + 500 - TopPosition) };
                double output = NeuralNetworkItem.Handle(data)[0];
                if (output > 0.5)
                    Jump();

                if (Counter == 15)
                    Counter *= -1;
                if (Counter <= 0)
                {
                    //fly down;
                    TopPosition += (Math.Pow(Counter, 2) * 0.0008) * 2;

                }
                else
                {
                    //fly up
                    TopPosition -= (Math.Pow(Counter, 2) * 0.0008) * 1.5;
                }

                Counter--;

                pictureBox.BeginInvoke((MethodInvoker)(() =>
                {
                    pictureBox.Top = (int)TopPosition;
                }));
                Fintess++;
            }
        }

        public void Jump()
        {
            if (isAlive)
                Counter = 60;
        }

        public void Dead()
        {
            if (isAlive)
            {
                pictureBox.BeginInvoke((MethodInvoker)(() => pictureBox.Visible = false));
                isAlive = false;
                WagStop();
                aliveBird--;
                if (aliveBird <= 0)
                    Game.mainForm.StopGame();
            }
        }

        public void Born()
        {
            aliveBird++;
            Counter = 0;
            TopPosition = 100;
            Fintess = 200;
            pictureBox.Location = new System.Drawing.Point(200, (int)TopPosition);
            pictureBox.BeginInvoke((MethodInvoker)(() => pictureBox.Visible = true));
            isAlive = true;
            WagStart();
        }
    }
}