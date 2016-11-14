using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;

namespace Retribution.Entities.Components
{
    class ArmedEnemy : Component
    {

        private int m_health;
        private Map m_map;
        private Direction m_faceing;
        private float m_hitTimer;
        private Entity m_target;
        private float m_shootFrequency;
        private float m_bulletSpeed;

        public ArmedEnemy(XmlNodeList nodelist, Entities.Entity entity, Map map) : base(entity, ComponentType.ArmedEnemy)
        {
            foreach (XmlNode node in nodelist)
            {
                switch (node.Name)
                {
                    case "health":
                        m_health = int.Parse(node.InnerText);
                        break;
                }
            }
            m_hitTimer = 0;
            m_faceing = Direction.Left;
            m_map = map;
            
            m_shootFrequency = 1.0f;
            m_bulletSpeed = 600.0f;
        }

        public ArmedEnemy(Entities.Entity entity, Map map,int health) : base(entity, ComponentType.ArmedEnemy)
        {
            m_hitTimer = 0;
            m_faceing = Direction.Left;
            m_map = map;
            m_health = health;
            m_shootFrequency = 1.0f;
            m_bulletSpeed = 600.0f;
        }

        public ArmedEnemy(Entities.Entity entity, Map map, int health, float shootFrequency, float bulletSpeed) : base(entity, ComponentType.ArmedEnemy)
        {
            m_hitTimer = 0;
            m_faceing = Direction.Left;
            m_map = map;
            m_health = health;
            m_bulletSpeed = bulletSpeed;
            m_shootFrequency = shootFrequency;
        }

        public override void Init()
        {
            m_target = m_map.GetEntity(0, "player");
        }

        public override void Msg(string msg, params object[] args)
        {
            if (msg == "Hit")
            {
                var entity = (Entity)args[0];
                var direction = (Direction)args[1];

                if (entity.Tag == "Good")
                {

                    if (direction == m_faceing && m_hitTimer >= m_shootFrequency)
                    {
                        m_hitTimer = 0;
                        entity.Msg("TakeDmg", 1);
                    }
                }
            }
            else if (msg == "TakeDmg")
            {
                m_health -= (int)args[0];
            }

        }

        public override void PreUpdate(float dt)
        {
        }

        public override void Update(float dt)
        {
            if(m_hitTimer + dt < float.MaxValue)
                m_hitTimer += dt;

            if (m_target != null)
            {
                Vector2 m_aiming = m_target.GetPos() - m_entity.GetPos();
                m_aiming.Normalize();

                if (m_hitTimer >= m_shootFrequency)
                {
                    if (!m_map.LineCast(m_entity.GetCenter(), m_target.GetCenter()))
                    {
                        var layer = m_map.GetLayer(0);
                        Entity temp = new Entity("bullet", "Projectile");
                        temp.AddComponent(new Projectile(temp, m_map, m_entity, m_aiming, m_bulletSpeed, 1));
                        temp.AddComponent(new SpriteRenderer(Management.ResourceManager.GetTexture("bullet1"), true, temp));
                        temp.AddComponent(new AABBCollider(temp, m_map, true, m_entity));
                        temp.SetCenter(m_entity.GetCenter());
                        layer.addEntity(temp);
                        m_hitTimer = 0;
                    }
                }
            }

            m_entity.Move(0, 1);

        }

        public override void PostUpdate(float dt)
        {
            if (m_health <= 0)
            {
                m_map.RemoveEntity(m_entity);
            }
           
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }


    }
}
