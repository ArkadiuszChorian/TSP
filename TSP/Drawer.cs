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
        public int BitmapWidth { get; set; }
        public int BitmapHeight { get; set; }
        public Drawer(List<Node> nodes)
        {
            BitmapWidth = 0;
            BitmapHeight = 0;
            FindMinimalBitmapSize(nodes);
        }

        private void FindMinimalBitmapSize(List<Node> nodes)
        {
            foreach (var node in nodes)
            {
                if (node.X > BitmapWidth)
                {
                    BitmapWidth = node.X + 30;
                }

                if (node.Y > BitmapHeight)
                {
                    BitmapHeight = node.Y + 30;
                }
            }
        }
        public void DrawChart(string filename, List<Node> allNodes, List<Node> resultNodes)
        {
            Bitmap cpy;
            try
            {
                //Bitmap bmp = new Bitmap(800, 800);
                Bitmap bmp = new Bitmap(BitmapWidth, BitmapHeight);
                cpy = (Bitmap)bmp.Clone();
                bmp.Dispose();

                using (Graphics g = Graphics.FromImage(cpy))
                {
                    g.Clear(Color.White);
                    //g.DrawLine(Pens.Red, 50, 20, 50, 40);
                    foreach (var node in allNodes)
                    {
                        //g.DrawEllipse(Pens.Red, new RectangleF(new PointF(node.X, node.Y), new SizeF(15.0f, 15.0f)));
                        g.FillEllipse(new SolidBrush(Color.Red), new RectangleF(new PointF(node.X, node.Y), new SizeF(17.0f, 15.0f)));
                    }
                    for (int i = 0; i < resultNodes.Count-1; i++)
                    {
                        var point1 = new PointF(resultNodes[i].X, resultNodes[i].Y);
                        var point2 = new PointF(resultNodes[i+1].X, resultNodes[i+1].Y);
                        g.FillEllipse(new SolidBrush(Color.Black), new RectangleF(point1, new SizeF(10.0f, 10.0f)));
                        g.FillEllipse(new SolidBrush(Color.Black), new RectangleF(point2, new SizeF(10.0f, 10.0f)));
                        g.DrawLine(Pens.Blue, point1, point2);
                    }
                    foreach (var node in resultNodes)
                    {
                        var lastNode = new PointF(node.X, node.Y);
                        g.FillEllipse(new SolidBrush(Color.Black), new RectangleF(new PointF(node.X, node.Y), new SizeF(10.0f, 10.0f)));
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
