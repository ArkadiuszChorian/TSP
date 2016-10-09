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
        /// <summary>
        /// Creates bitmap based on all points and path taken by algorithm. 
        /// Method saves bitmap to file in ".../TSP/bin/Debug/<param name="filename"/>       
        /// </summary>  
        public void DrawChart( String filename, List<Node> fullNodes, List<Node> path )
        {
            Bitmap cpy;
            try
            {
                // 5px margin
                Bitmap bmp = new Bitmap(805, 405);
                cpy = (Bitmap)bmp.Clone();
                bmp.Dispose();

                using ( Graphics g = Graphics.FromImage(cpy) )
                {
                    g.Clear(Color.White);
                    //g.DrawLine(Pens.Red, 50, 20, 50, 40);
                    for ( int i = 0; i < fullNodes.Count; i++ )
                    {
                        Coordinate coords = prepareCoordinates(fullNodes[i].X, fullNodes[i].Y);
                        g.FillEllipse(Brushes.Red,
                            new RectangleF(new PointF(coords.X, coords.Y), new SizeF(6.0f, 6.0f)));
                    }

                    Coordinate previous = new Coordinate();
                    bool firstTime = true;

                    for (int i = 0; i < path.Count; i++ )
                    {
                        Coordinate coords = prepareCoordinates(path[i].X, path[i].Y);

                        if ( firstTime )
                        {
                            previous = coords;
                            firstTime = false;
                            continue;
                        }

                        g.DrawLine(Pens.Blue, 
                            new PointF(previous.X, previous.Y), 
                            new PointF(coords.X, coords.Y));

                        previous = coords;
                    }
                }

                if ( System.IO.File.Exists(filename) )
                    System.IO.File.Delete(filename);

                cpy.Save(filename);
                cpy.Dispose();
            }
            catch ( Exception ex )
            {
                Console.WriteLine("Conversion failed: {0}", ex.Message);
            }
            //

        }

        /// <summary>
        /// Scaling coordinates for bitmap with factor = 5
        ///(max X value = 4000, scaled = 800, Y value 2000, scaled = 400)
        /// </summary>
        private Coordinate prepareCoordinates( int x, int y )
        {
            Coordinate c = new Coordinate();
            c.X = (float)Math.Round((double)x / 5.0);
            c.Y = (float)( Math.Round((double)y / 5.0) );
            return c;
        }
    }

    /// <summary>
    /// Class to keep scaled coordinates
    /// </summary>
    class Coordinate
    {
        public float X { get; set; }
        public float Y { get; set; }
    }
}
