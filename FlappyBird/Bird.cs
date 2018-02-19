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

            pictureBox = new PictureBox();
            pictureBox.Location = new System.Drawing.Point(0,0);
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            form.Controls.Add(pictureBox);
            pictureBox.Image = birdImg[0];
        }

        static Bird()
        {
            birdImgs = SplitImage(new Bitmap(@"D:\Education 2018\FlappyBird\FlappyBird\Textures\img_bird.png"), 2, 10).ToArray();
        }

        public void WagStart()
        {
            thread = new Thread(new ThreadStart(()=> {
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
            thread.Abort();
        }
        public static List<Bitmap> SplitImage(Bitmap img, int NumX, int NumY)
        {
            List<Bitmap> listBmp = new List<Bitmap>();

            Bitmap bmp = new Bitmap(img);
            int width = img.Width / NumX;
            int height = img.Height / NumY;

            for (int i = 0; i < NumY; i++)
                for (int j = 0; j < NumX; j++)
                {
                    Rectangle rect = new Rectangle(j * width, i * height, width, height);
                    Bitmap region = new Bitmap(rect.Width, rect.Height);
                    using (Graphics g = Graphics.FromImage(region))
                    {
                        g.DrawImage(bmp, 0, 0, rect, GraphicsUnit.Pixel);
                    }
                    listBmp.Add(region);
                }
            return listBmp;
        }
    }
}
