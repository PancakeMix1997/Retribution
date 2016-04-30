using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Retribution.Entities
{
    class Entity
    {
        private List<Components.Component> m_components;
        private Vector2 pos;
        private Vector2 rot;

        public Entity()
        {
            m_components = new List<Components.Component>();
        }

        public Vector2 GetPos()
        {
            return pos;
        }

        public void Move(float dirx, float diry)
        {
            pos.X += dirx;
            pos.Y += diry;
        }

        public void Move(Vector2 dir)
        {
            pos += dir;
        }

        public void SetPos(Vector2 position)
        {
            pos = position;
        }

        public void AddComponent(Components.Component component)
        {
            m_components.Add(component);
        }

        public void Update(float dt)
        {
            foreach(Components.Component i in m_components)
            {
                i.Update(dt);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Components.Component i in m_components)
            {
                i.Draw(spriteBatch);
            }
        }
    }
}
