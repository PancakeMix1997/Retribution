using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Retribution.Entities.Components
{
    class Projectile : Component
    {
        private int m_dmg;
        private Map m_map;
        private Vector2 m_dir;
        private float m_speed;
        private Entity m_owner;

        public Projectile(Entity entity, Map map, Entity owner,Vector2 dir,float speed,int dmg) : base(entity, ComponentType.Projectile)
        {
            m_deleted = false;
            m_map = map;
            m_dmg = dmg;
            m_dir = dir;
            m_owner = owner;
            m_speed = speed;
        }

        public override void Init()
        {
        }

        public override void PreUpdate(float dt)
        {
        }

        public override void Update(float dt)
        {
            m_entity.Move(m_dir * m_speed * dt);
        }

        public override void PostUpdate(float dt)
        {
            if (m_deleted)
                m_map.RemoveEntity(m_entity);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void Msg(string msg, params object[] args)
        {
            
            if(msg == "Hit")
            {
                var entity = (Entity)args[0];
                if (entity != m_owner)
                {
                    entity.Msg("TakeDmg", m_dmg);
                    m_deleted = true;
                }
            }
            else if (msg == "Collision")
            {
                var direction1 = (Direction)args[0];
                var direction2 = (Direction)args[1];
                if (direction1 != Direction.None || direction2 != Direction.None)
                    m_deleted = true;
            }


        }
    }
}
