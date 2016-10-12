using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Retribution.Entities.Components;


namespace Retribution.Entities
{
    class CollisionManager
    {
        //update this befor static collisions are calculated;
        private List<AABBCollider> m_colliders;
        private Map m_map;

        public CollisionManager(Map map)
        {
            m_map = map;
            m_colliders = new List<AABBCollider>();
        }

        public void AddCollider(AABBCollider collider)
        {
            m_colliders.Add(collider);
        }

        public void Update()
        {
            for (int i = 0; i < m_colliders.Count; i++)
            {
                if (m_colliders[i].GetEntity().Deleted)
                    m_colliders.RemoveAt(i);
            }
                for (int i = 0; i < m_colliders.Count; i++)
            {

                var collis = m_map.GetCollision(m_colliders[i].Rect);
                m_colliders[i].GetEntity().Move(collis.Item1.X, collis.Item1.Y);
                m_colliders[i].GetEntity().Msg("Collision", collis.Item2, collis.Item3);

                for (int x = 0; x < m_colliders.Count; x++)
                {
                    Entity[] ignore = m_colliders[i].IgnoreEntities();
                    bool DoIgnore = false;
                    if(ignore != null)
                        foreach (Entity e in ignore)
                            if (e == m_colliders[x].GetEntity())
                                DoIgnore = true;

                    ignore = m_colliders[x].IgnoreEntities();
                    if (ignore != null)
                        foreach (Entity e in ignore)
                            if (e == m_colliders[i].GetEntity())
                                DoIgnore = true;

                    if (DoIgnore)
                        continue;

                    if (i == x)
                        continue;

                    var collision = HelperFunctions.RectIntersect(m_colliders[i].Rect, m_colliders[x].Rect);

                    if (collision.Item2 != Direction.None && m_colliders[i].CollisionResolved == false)
                    {
                        m_colliders[i].GetEntity().Msg("Hit", m_colliders[x].GetEntity(), collision.Item2);
                        m_colliders[x].GetEntity().Msg("Hit", m_colliders[i].GetEntity(), HelperFunctions.OppositeDirection(collision.Item2));

                        m_colliders[i].CollisionResolved = true;
                        m_colliders[x].CollisionResolved = true;

                        var testRect = m_colliders[i].Rect;
                        testRect.X += (int)collision.Item1.X;
                        testRect.Y += (int)collision.Item1.Y;

                        collis = m_map.GetCollision(testRect);

                        if (m_colliders[x].CanMove)
                        {
                            if (collision.Item1.X != 0)
                            {
                                if (collis.Item2 == Direction.Left || collis.Item2 == Direction.Right)
                                {
                                    //so you can get closer to the wall move them based on teh tile map
                                    //m_colliders[i].GetEntity().Move(collision.Item1.X + collis.Item1.X,0);
                                    //m_colliders[x].GetEntity().Move(-collision.Item1.X + collis.Item1.X - m_colliders[i].Rect.Width,0);
                                    m_colliders[x].GetEntity().Move(-collision.Item1);
                                }
                                else
                                    m_colliders[i].GetEntity().Move(collision.Item1);
                            }

                            if (collision.Item1.Y != 0)
                            {
                                if (collis.Item2 == Direction.Up || collis.Item2 == Direction.Down)
                                    //do it here to
                                    m_colliders[x].GetEntity().Move(-collision.Item1);
                                else
                                    m_colliders[i].GetEntity().Move(collision.Item1);
                            }
                        }
                        else
                        {
                            m_colliders[i].GetEntity().Move(collision.Item1);
                        }
                    }

                }


            }


        }
    }
}
