using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Retribution.Entities
{
    class Layer
    {
        private List<Entity> m_entities;
        private CollisionManager m_collisMan;
        private Map m_map;

        public Layer(Map map)
        {
            m_map = map;
            m_collisMan = new CollisionManager(map);
            m_entities = new List<Entity>();
        }

        public void addEntity(Entity entity)
        {
            Components.AABBCollider collider = entity.GetComponent<Components.AABBCollider>(Components.ComponentType.AABBCollider);

            if (collider != null)
                m_collisMan.AddCollider(collider);

            int count = 0;
            foreach (Entity i in m_entities)
            {
                if(i.Name == entity.Name)
                {
                    count++;
                }
            }

            if(count > 0)
                entity.Name += count.ToString();

            m_entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            entity.Deleted = true;
            m_entities.Remove(entity);
        }

        public void RemoveEntity(string name)
        {
            for(int i = 0; i < m_entities.Count; i++)
            {
                if(m_entities[i].Name == name)
                {
                    m_entities[i].Deleted = true;
                    m_entities.RemoveAt(i);
                }
            }
        }

        public Entity[] GetEntities(string tag)
        {
            List<Entity> entities = new List<Entity>(2);
            foreach(Entity i in m_entities)
            {
                if(i.Tag == tag)
                {
                    entities.Add(i);
                }
            }
            return entities.ToArray();
        }

        public Entity GetEntity(string name)
        {
            foreach (Entity i in m_entities)
            {
                if (i.Name == name)
                {
                    return i;
                }
            }
            return null;
        }

        public void Update(float dt)
        {
            for (int i = 0; i < m_entities.Count; i++ )
            {
                m_entities[i].PreUpdate(dt);
                m_entities[i].Update(dt);
                m_entities[i].PostUpdate(dt);
            }
            m_collisMan.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Entity i in m_entities)
            {
                i.Draw(spriteBatch);
            }
        }

        public void InitEntities()
        {
            foreach (Entity i in m_entities)
            {
                i.Init();
            }
        }
    }
}
