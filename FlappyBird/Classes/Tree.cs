using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public class Tree
    {
        public static List<Tree> items = new List<Tree>();
        static Bitmap[] treeImgs = new Bitmap[2];
        static public Tree targetOfBird;
        static Random random = new Random();

        public PictureBox pbTreeTop;
        public PictureBox pbTreeBottom;
        int number;

        public Tree()
        {
            pbTreeTop = new PictureBox
            {
                Location = new System.Drawing.Point(Convert.ToInt32((Game.mainForm.Width + 90) * (1 + items.Count / 4.0)), 0),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Image = treeImgs[0],
            };

            pbTreeBottom = new PictureBox
            {
                Location = new System.Drawing.Point(Convert.ToInt32((Game.mainForm.Width + 90) * (1 + items.Count / 4.0)), 0),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Image = treeImgs[1],
            };
            GeneretePosition();

            Game.mainForm.Controls.Add(pbTreeTop);
            Game.mainForm.Controls.Add(pbTreeBottom);

            if (items.Count == 0)
                targetOfBird = this;
            number = items.Count;

            items.Add(this);
        }
        static Tree()
        {
            treeImgs = ImgWorker.SplitImage(new Bitmap(@"Textures\img_tree.png"), 2, 1).ToArray();
        }

        public void GeneretePosition()
        {
            pbTreeTop.Top = random.Next(-300, 0);
            pbTreeBottom.Top = pbTreeTop.Top + 600;
        }

        public void startPosition()
        {
            pbTreeTop.Location = new System.Drawing.Point(Convert.ToInt32((Game.mainForm.Width + 90) * (1 + number / 4.0)), 0);
            pbTreeBottom.Location = new System.Drawing.Point(Convert.ToInt32((Game.mainForm.Width + 90) * (1 + number / 4.0)), 0);
            GeneretePosition();
            targetOfBird = items[0];
        }

        public void Move()
        {
            if (pbTreeTop.Left <= -90)
            {
                pbTreeTop.Left = pbTreeTop.Parent.Width;
                GeneretePosition();
            }
            else
                for (int i = 0; i < 3; i++)
                    pbTreeTop.Left--;
            pbTreeBottom.Left = pbTreeTop.Left;

        }

    }
}