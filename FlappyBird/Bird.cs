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

namespace FlappyBird
{
    class Bird
    {
        static public List<Bird> items = new List<Bird>();

        public double TopPosition { get; set; }
        public int Counter { get; set; }

        static Bitmap[] birdImgs = new Bitmap[20];

        Bitmap[] birdImg = new Bitmap[2];
        PictureBox pictureBox;
        private Thread thread;

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


        public Bird(Form form, Color c)
        {
            birdImg[0] = birdImgs[(int)c * 2];
            birdImg[1] = birdImgs[(int)c * 2 + 1];
            TopPosition = 100;
            pictureBox = new PictureBox
            {
                Location = new System.Drawing.Point(100, (int)TopPosition),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Image = birdImg[0]
            };
            form.Controls.Add(pictureBox);
            Counter = 0;
            items.Add(this);

        }

        static Bird()
        {
            birdImgs = ImgWorker.SplitImage(new Bitmap(@"Textures\img_bird.png"), 2, 10).ToArray();
        }

        public void WagStart()
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

        public void WagStop()
        {
            if (thread != null && thread.IsAlive)
                thread.Abort();
        }


        //Fly
        public void Fly()
        {
            if (Counter == 15)
                Counter *=-1;
            if (Counter <= 0)
            {
                //fly down;
                TopPosition += (Math.Pow(Counter, 2) * 0.0008)*2;

            }
            else
            {
                //fly up
                TopPosition -= (Math.Pow(Counter, 2) * 0.0008)*1.5;
            }

            Counter --;

            pictureBox.Top = (int)TopPosition;
        }

        public void Jump()
        {
            Counter = 65;
        }

        //Destructor
        ~Bird()
        {
            WagStop();
        }

    }
}
