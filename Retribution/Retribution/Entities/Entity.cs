using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retribution.Entities.Components;


namespace Retribution.Entities
{

    class Entity
    {

        private List<Component> m_components;
        private Vector2 m_pos;
        private Vector2 m_center;
        private Vector2 m_rot;
        private Vector2 m_dims;
        public string Name { get; set; }
        public string Tag { get; set; }
        public bool Deleted { get; set; }

        public Entity()
        {
            Name = "Default";
            Tag = "Default";
            m_components = new List<Component>();
        }

        public Entity(string name, string tag)
        {
            Name = name;
            Tag = tag;
            m_components = new List<Component>();
        }

        public void SetDims(Vector2 Dims)
        {
            m_dims = Dims;
        }

        public Vector2 GetDims()
        {
            return m_dims;
        }

        public Vector2 GetPos()
        {
            return m_pos;
        }

        public Vector2 GetCenter()
        {
            return m_center;
        }

        public void Move(float dirx, float diry)
        {
            m_pos.X += dirx;
            m_pos.Y += diry;
            m_center = m_pos + m_dims / 2;
        }

        public void Move(Vector2 dir)
        {
           m_pos += dir;
           m_center = m_pos + m_dims / 2;
        }

        public void SetPos(Vector2 position)
        {
            m_pos = position;
            m_center = m_pos + m_dims / 2;
        }

        public void SetCenter(Vector2 center)
        {
            m_center = center;
            m_pos = center - m_dims / 2;
        }

        public void AddComponent(Component component)
        {
            m_components.Add(component);
        }

        public void Msg(string msg, params object[] args)
        {
            foreach (Component i in m_components)
            {
                i.Msg(msg, args);
            }
        }

        public T GetComponent<T>(ComponentType type)
            where T : Component
        {
            foreach (Component i in m_components)
            {
                if (i.GetComponentType() == type)
                    return (T)i;
            }
            return default(T);
        }

        public void PreUpdate(float dt)
        {
            foreach (Component i in m_components)
            {
                i.PreUpdate(dt);
            }
        }

        public void Update(float dt)
        {
            foreach(Component i in m_components)
            {
                i.Update(dt);
            }
        }

        public void PostUpdate(float dt)
        {
            for(int i = 0; i < m_components.Count; i++)
            {
                m_components[i].PostUpdate(dt);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Component i in m_components)
            {
                i.Draw(spriteBatch);
            }
        }

        public void Init()
        {
            foreach (Component i in m_components)
            {
                i.Init();
            }
        }
    }
}
