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

        public PictureBox pbTreeTop;
        public PictureBox pbTreeBottom;
        public Tree(Form form)
        {
            pbTreeTop = new PictureBox
            {
                Location = new System.Drawing.Point(Convert.ToInt32((form.Width + 90) * (1 + items.Count / 4.0)), 0),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Image = treeImgs[0],
            };

            pbTreeBottom = new PictureBox
            {
                Location = new System.Drawing.Point(Convert.ToInt32((form.Width + 90) * (1 + items.Count / 4.0)), 0),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Image = treeImgs[1],
            };
            GeneretePosition();


            form.Controls.Add(pbTreeTop);
            form.Controls.Add(pbTreeBottom);

            if (items.Count == 0)
                targetOfBird = this;

            items.Add(this);
        }
        static Tree()
        {
            treeImgs = ImgWorker.SplitImage(new Bitmap(@"Textures\img_tree.png"), 2, 1).ToArray();
        }

        public void GeneretePosition()
        {
            Random r = new Random();
            pbTreeTop.Top = r.Next(-300, 0);
            pbTreeBottom.Top = pbTreeTop.Top + 600;
        }

        public void Move()
        {
            if (pbTreeTop.Left <= -90)
            {
                pbTreeTop.Left = pbTreeTop.Parent.Width;
                GeneretePosition();
            }
            else
                pbTreeTop.Left = pbTreeTop.Left - 3;
            pbTreeBottom.Left = pbTreeTop.Left;

        }

    }
}