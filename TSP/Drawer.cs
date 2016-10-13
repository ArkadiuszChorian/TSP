using System;
using System.Collections.Generic;
using System.Drawing;
using TSP.Models;

namespace TSP
{
    class Drawer
    {
        public int BitmapWidth { get; set; }
        public int BitmapHeight { get; set; }
        public Drawer()
        {
            BitmapWidth = 0;
            BitmapHeight = 0;
            FindMinimalBitmapSize(DAL.Instance.Nodes);
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
        public void DrawChart(string filename, IList<Node> resultNodes)
        {
            try
            {
                var bitmap = new Bitmap(BitmapWidth, BitmapHeight);

                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.White);
                    foreach (var node in DAL.Instance.Nodes)
                    {
                        graphics.FillEllipse(new SolidBrush(Color.Red), new RectangleF(new PointF(node.X, node.Y), new SizeF(17.0f, 15.0f)));
                    }
                    for (int i = 0; i < resultNodes.Count-1; i++)
                    {
                        var point1 = new PointF(resultNodes[i].X, resultNodes[i].Y);
                        var point2 = new PointF(resultNodes[i+1].X, resultNodes[i+1].Y);
                        graphics.FillEllipse(new SolidBrush(Color.Black), new RectangleF(point1, new SizeF(10.0f, 10.0f)));
                        graphics.FillEllipse(new SolidBrush(Color.Black), new RectangleF(point2, new SizeF(10.0f, 10.0f)));
                        graphics.DrawLine(Pens.Blue, point1, point2);
                    }

                    graphics.DrawString("1", new Font("Tahoma", 48), Brushes.Black, resultNodes[0].X + 20, resultNodes[0].Y );
                    var firstPoint = new PointF(resultNodes[0].X, resultNodes[0].Y);
                    var lastPoint = new PointF(resultNodes[resultNodes.Count-1].X, resultNodes[resultNodes.Count-1].Y);
                    graphics.DrawLine(Pens.Blue, firstPoint, lastPoint); 

                    foreach (var node in resultNodes)
                    {
                        var lastNode = new PointF(node.X, node.Y);
                        graphics.FillEllipse(new SolidBrush(Color.Black), new RectangleF(new PointF(node.X, node.Y), new SizeF(10.0f, 10.0f)));
                    }
                }

                if ( System.IO.File.Exists(filename) )
                    System.IO.File.Delete(filename);

                bitmap.Save(filename);
                bitmap.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Conversion failed: {0}", ex.Message);
            }
        }     
    }
}
