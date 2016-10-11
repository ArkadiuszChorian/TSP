using System;
using System.Collections.Generic;
using System.Drawing;

namespace TSP
{
    class Drawer
    {
        public int BitmapWidth { get; set; }
        public int BitmapHeight { get; set; }
        public Drawer(IList<Node> nodes)
        {
            BitmapWidth = 0;
            BitmapHeight = 0;
            FindMinimalBitmapSize(nodes);
        }

        private void FindMinimalBitmapSize(IList<Node> nodes)
        {
            foreach (var node in nodes)
            {
                if (node.X > BitmapWidth)
                {
                    BitmapWidth = node.X + 50; // Added more because of cuting some points
                }

                if (node.Y > BitmapHeight)
                {
                    BitmapHeight = node.Y + 50;
                }
            }
        }
        public void DrawChart(string filename, IList<Node> allNodes, IList<Node> resultNodes)
        {
            try
            {
                var bmp = new Bitmap(BitmapWidth, BitmapHeight);
                var cpy = (Bitmap)bmp.Clone();
                bmp.Dispose();

                using (var g = Graphics.FromImage(cpy))
                {
                    g.Clear(Color.White);
                    foreach (var node in allNodes)
                    {
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

                    g.DrawString("1", new Font("Tahoma", 48), Brushes.Black, resultNodes[0].X + 20, resultNodes[0].Y );
                    var firstPoint = new PointF(resultNodes[0].X, resultNodes[0].Y);
                    var lastPoint = new PointF(resultNodes[resultNodes.Count-1].X, resultNodes[resultNodes.Count-1].Y);
                    g.DrawLine(Pens.Blue, firstPoint, lastPoint); 

                    foreach (var node in resultNodes)
                    {
                        var lastNode = new PointF(node.X, node.Y);
                        g.FillEllipse(new SolidBrush(Color.Black), new RectangleF(new PointF(node.X, node.Y), new SizeF(10.0f, 10.0f)));
                    }
                }

                if ( System.IO.File.Exists(filename) )
                    System.IO.File.Delete(filename);

                cpy.Save(filename);
                cpy.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Conversion failed: {0}", ex.Message);
            }
        }
    }
}
