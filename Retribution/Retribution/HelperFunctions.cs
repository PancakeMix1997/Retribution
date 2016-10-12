using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Retribution.Entities;

namespace Retribution
{
    static class HelperFunctions
    {
        public static Tuple<Vector2, Direction> RectIntersect(Rectangle a, Rectangle b)
        {
            Rectangle test = Rectangle.Intersect(a, b);

            if (test.Width == 0 || test.Height == 0)
                return new Tuple<Vector2, Direction>(new Vector2(0, 0), Direction.None);

            Direction xdir = Direction.None, ydir = Direction.None;

            Vector2 rect = new Vector2();

            float minx, maxx, miny, maxy;

            minx = Math.Min(b.Right, a.Right);
            maxx = Math.Max(b.X, a.X);

            miny = Math.Min(b.Bottom, a.Bottom);
            maxy = Math.Max(b.Y, a.Y);

            if (a.Right == (int)minx)
            {
                xdir = Direction.Right;
                rect.X = maxx - minx;
            }
            else
            {
                xdir = Direction.Left;
                rect.X = minx - maxx;
            }
            if (a.Y == (int)maxy)
            {
                ydir = Direction.Up;
                rect.Y = miny - maxy;
            }
            else
            {
                ydir = Direction.Down;
                rect.Y = maxy - miny;
            }

            if (Math.Abs(rect.X) < Math.Abs(rect.Y))
            {
                rect.Y = 0;
                return new Tuple<Vector2, Direction>(rect, xdir);
            }
            else
            {
                rect.X = 0;
                return new Tuple<Vector2, Direction>(rect, ydir);
            }

            // rect.X = Math.Min(b.X + b.Width, a.X + a.Width) - Math.Max(b.X, a.X);
            //  rect.Y = Math.Min(b.Y + b.Height, a.Y + a.Height) - Math.Max(b.Y, a.Y);
        }

        public static Direction OppositeDirection(Direction dir)
        {
            if (dir == Direction.Right)
                return Direction.Left;
            else if (dir == Direction.Left)
                return Direction.Right;
            else if (dir == Direction.Up)
                return Direction.Down;
            else if (dir == Direction.Down)
                return Direction.Up;
            return Direction.None;
        }

    }
}
