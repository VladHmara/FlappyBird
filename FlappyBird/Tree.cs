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

        PictureBox pbTreeTop;
        PictureBox pbTreeBottom;
        public Tree(Form form)
        {
            pbTreeTop = new PictureBox
            {
                Location = new System.Drawing.Point(form.Width - 300, 0),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Image = treeImgs[0],
                
            };
            
            pbTreeBottom = new PictureBox
            {
                Location = new System.Drawing.Point(form.Width - 300, 0),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Image = treeImgs[1],
            };
            GeneretePosition();

            items.Add(this);

            form.Controls.Add(pbTreeTop);
            form.Controls.Add(pbTreeBottom);
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

    }
}
