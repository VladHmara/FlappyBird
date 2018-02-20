using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    static public class ImgWorker
    {
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
