using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    class Drawer
    {
        public void DrawChart(String filename, List<Node> fullNodes, List<Node> path)
        {
            Bitmap cpy;
            try
            {
                Bitmap bmp = new Bitmap(800, 800);
                cpy = (Bitmap)bmp.Clone();
                bmp.Dispose();

                using (Graphics g = Graphics.FromImage(cpy))
                {
                    g.Clear(Color.White);
                    //g.DrawLine(Pens.Red, 50, 20, 50, 40);
                    for (int i = 0; i < fullNodes.Count; i++)
                    {
                        g.DrawEllipse(Pens.Red,
                            new RectangleF(new PointF(fullNodes[i].X, fullNodes[i].Y), new SizeF(5.0f, 5.0f)));
                    }
                }

                cpy.Save(filename);
                cpy.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Conversion failed: {0}", ex.Message);
            }
            //cpy.SetResolution(96, 96);

        }
    }
}
