using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Retribution.Entities
{
    enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    class Map
    {
        private Texture2D m_backroundImage;
        private List<Layer> m_layers;
        private List<Layer> m_guiLayers;
        private Texture2D m_tileSheet;
        private int[,] m_tileMap;
        private int m_tileWidth, m_tileHeight;
        private int m_tileMapWidth, m_tileMapHeight;

        public Map()
        {
            m_layers = new List<Layer>();
        }


        public void SetBackroundImage(Texture2D tex)
        {
            m_backroundImage = tex;
        }

        public void SetTileMap(string text, int width, int height)
        {
            string [] tiles = text.Split(' ',',','\r','\n');
            m_tileMapWidth = width;
            m_tileMapHeight = height;
            m_tileMap = new int[width , height];
            int index = 0;
            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    while (tiles[index] == "")
                        index++;
                    m_tileMap[x, y] = Int32.Parse(tiles[index]);
                    index++;
                }
                                
            }

        }

        public Entity[] GetEntities(int layer,string tag)
        {
            return m_layers[layer].GetEntities(tag);
        }

        public Entity GetEntity(int layer,string name)
        {
            return m_layers[layer].GetEntity(name);
        }

        public void RemoveEntity(Entity entity)
        {
            foreach(Layer i in m_layers)
                i.RemoveEntity(entity);
        }

        public void RemoveEntity(string name)
        {
            foreach (Layer i in m_layers)
                i.RemoveEntity(name);
        }


        public void SetTileSheet(Texture2D tex, int tileWidth, int tileHeight)
        {
            m_tileSheet = tex;
            m_tileWidth = tileWidth;
            m_tileHeight = tileHeight;
        }

        internal void InitEntities()
        {
            foreach (Layer i in m_layers)
                i.InitEntities();
        }

        public bool LineCast(Vector2 a, Vector2 b)
        {
            //dist 
            int scanAmt = (int)(Vector2.Distance(a, b) / (float)Math.Sqrt(m_tileWidth ^ 2 * m_tileHeight ^ 2) * 50);

            float xSlope = (b.X - a.X) / scanAmt;
            float ySlope = (b.Y - a.Y) / scanAmt;


            var tileRect = new Rectangle();
            tileRect.Width = m_tileWidth;
            tileRect.Height = m_tileHeight;


            Vector2 curPos = new Vector2(a.X, a.Y);
            for (float i = 0; i < scanAmt; i++)
            {
                Point topLeft, topRight, botLeft, botRight;
                topLeft = new Point((int)curPos.X / m_tileWidth, (int)curPos.Y / m_tileHeight);
                botLeft = new Point((int)curPos.X / m_tileWidth, (int)Math.Ceiling(curPos.Y / m_tileHeight));
                topRight = new Point((int)Math.Ceiling(curPos.X / m_tileWidth), (int)curPos.Y / m_tileHeight);
                botRight = new Point((int)Math.Ceiling(curPos.X / m_tileWidth), (int)Math.Ceiling(curPos.Y / m_tileHeight));

                if (m_tileMap[topLeft.X, topLeft.Y] != 0)
                {
                    tileRect.X = topLeft.X * m_tileWidth;
                    tileRect.Y = topLeft.Y * m_tileHeight;
                    if (HelperFunctions.PointIntersect(curPos.ToPoint(), tileRect))
                        return true;
                }
                if (m_tileMap[botLeft.X, botLeft.Y] != 0)
                {
                    tileRect.X = botLeft.X * m_tileWidth;
                    tileRect.Y = botLeft.Y * m_tileHeight;
                    if (HelperFunctions.PointIntersect(curPos.ToPoint(), tileRect))
                        return true;
                }
                if (m_tileMap[topRight.X, topRight.Y] != 0)
                {
                    tileRect.X = topRight.X * m_tileWidth;
                    tileRect.Y = topRight.Y * m_tileHeight;
                    if (HelperFunctions.PointIntersect(curPos.ToPoint(), tileRect))
                        return true;
                }
                if (m_tileMap[botRight.X, botRight.Y] != 0)
                {
                    tileRect.X = botRight.X * m_tileWidth;
                    tileRect.Y = botRight.Y * m_tileHeight;
                    if (HelperFunctions.PointIntersect(curPos.ToPoint(), tileRect))
                        return true;
                }
                curPos.X += xSlope;
                curPos.Y += ySlope;
            }

            return false;
        }

        public Tuple<Vector2,Direction,Direction> GetCollision(Rectangle bounds)
        {
            Vector2 collisRect = new Vector2();
            Direction direction1= Direction.None, direction2 = Direction.None;


            Point topLeft, topRight, botLeft, botRight;

            topLeft = new Point(bounds.X/m_tileWidth, bounds.Y/m_tileHeight);
            botLeft = new Point(bounds.X/m_tileWidth, (bounds.Y+bounds.Height)/m_tileHeight);
            topRight = new Point((bounds.X+bounds.Width)/m_tileWidth, bounds.Y/m_tileHeight);
            botRight = new Point((bounds.X+bounds.Width)/m_tileWidth, (bounds.Y+bounds.Height)/m_tileHeight);

            List<Vector2> closestTiles = new List<Vector2>();

            //reason why you clip if your at the end of a map
            if (topLeft.X >= 0 && topLeft.Y >= 0 && botRight.X < m_tileMapWidth && botRight.Y < m_tileMapHeight)
            {
                if (m_tileMap[topLeft.X, topLeft.Y] != 0)
                    closestTiles.Add(new Vector2(topLeft.X * m_tileWidth + m_tileWidth / 2, topLeft.Y * m_tileHeight + m_tileWidth / 2));
                if (m_tileMap[botLeft.X, botLeft.Y] != 0 && botLeft != topLeft)
                    closestTiles.Add(new Vector2(botLeft.X * m_tileWidth + m_tileWidth / 2, botLeft.Y * m_tileHeight + m_tileWidth / 2));
                if (m_tileMap[topRight.X, topRight.Y] != 0 && topRight != topLeft && topRight != botLeft)
                    closestTiles.Add(new Vector2(topRight.X * m_tileWidth + m_tileWidth / 2, topRight.Y * m_tileHeight + m_tileWidth / 2));
                if (m_tileMap[botRight.X, botRight.Y] != 0 && botRight != topLeft && botRight != botLeft && botRight != topRight)
                    closestTiles.Add(new Vector2(botRight.X * m_tileWidth + m_tileWidth / 2, botRight.Y * m_tileHeight + m_tileWidth / 2));
            }

            if (closestTiles.Count == 0)
                return new Tuple<Vector2, Direction, Direction>(new Vector2(0,0),Direction.None,Direction.None);


            Vector2[] closestTilesArray = closestTiles.ToArray();
            //order collision resolutions to happen starting at the tile closest to you

            Vector2 boundsVec = new Vector2(bounds.Center.X, bounds.Center.Y);
            for(int i = 0; i < closestTilesArray.Length; i++)
            {
                float idist = Vector2.Distance(boundsVec, closestTilesArray[i]);

                for(int x = 0; x < closestTilesArray.Length; x++)
                {
                    if(idist < Vector2.Distance(boundsVec, closestTilesArray[x]))
                    {
                        Vector2 temp = closestTilesArray[x];
                        closestTilesArray[x] = closestTilesArray[i];
                        closestTilesArray[i] = temp;
                    }
                }
            }

            var tileRect = new Rectangle();
            tileRect.Width = m_tileWidth;
            tileRect.Height = m_tileHeight;

            for (int i = 0; i < closestTilesArray.Length; i++)
            {
                closestTilesArray[i].X -= (float)m_tileWidth / 2;
                closestTilesArray[i].Y -= (float)m_tileHeight / 2;

                tileRect.Location = closestTilesArray[i].ToPoint();     

                var intersection = HelperFunctions.RectIntersect(bounds, tileRect);
                collisRect += intersection.Item1;
                bounds.X += (int)intersection.Item1.X;
                bounds.Y += (int)intersection.Item1.Y;

                if (intersection.Item2 != Direction.None)
                {
                    if (direction1 == Direction.None)
                        direction1 = intersection.Item2;
                    else
                        direction2 = intersection.Item2;
                }
            }
            //might wanna loop this code until its not colliding with anything
             
            return new Tuple<Vector2, Direction, Direction>(collisRect,direction1,direction2);
        }

        

        public void ClearMap()
        {
            m_layers.Clear();
        }

        public void ClearGui()
        {
            m_guiLayers.Clear();
        }

        public Layer GetLayer(int depth)
        {
            while(m_layers.Count <= depth)
            {
                m_layers.Add(new Layer(this));
            }
            return m_layers[depth];
        }

        public void Update(float dt)
        {
            foreach(Layer i in m_layers)
            {
                i.Update(dt);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // add dest rect that scales to the total size of map so backround covers it
            spriteBatch.Draw(m_backroundImage,new Rectangle(0,0,m_tileMapWidth*m_tileWidth,m_tileMapHeight*m_tileHeight), Color.White);
            for (int y = 0; y < m_tileMapHeight; y++)
            {
                for (int x = 0; x < m_tileMapWidth; x++)
                {
                    if (m_tileMap[x, y] == 0)
                        continue;
                    spriteBatch.Draw(m_tileSheet, new Rectangle(x * m_tileWidth, y * m_tileWidth, m_tileWidth, m_tileHeight), 
                        new Rectangle((m_tileMap[x, y]-1)*m_tileWidth,0,m_tileWidth,m_tileHeight), Color.White);
                }
            }
            foreach(Layer i in m_layers)
            {
                i.Draw(spriteBatch);
            }
        }
    }
}
