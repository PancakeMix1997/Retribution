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
    class AABBCollider : Component
    {
        private Map m_map;
        public Rectangle Rect { get; set; }
        public bool CanMove { get; }
        public bool CollisionResolved { get; set; }
        private Entity[] m_ignore;


        public AABBCollider(XmlNodeList nodelist, Map map, Entity entity) : base(entity, ComponentType.AABBCollider)
        {
            CanMove = false;
            foreach (XmlNode node in nodelist)
            {
                switch (node.Name)
                {
                    case "canMove":
                        CanMove = true;
                        break;
                }
            }
            m_map = map;
            Rect = new Rectangle((int)m_entity.GetPos().X, (int)m_entity.GetPos().Y, (int)m_entity.GetDims().X, (int)m_entity.GetDims().Y);
        }

        public AABBCollider (Entities.Entity entity, Map map, bool canMove) : base(entity, ComponentType.AABBCollider)
        {
            CanMove = canMove;
            m_map = map;
            Rect = new Rectangle((int)m_entity.GetPos().X, (int)m_entity.GetPos().Y, (int)m_entity.GetDims().X, (int)m_entity.GetDims().Y);
        }

        public AABBCollider(Entities.Entity entity, Map map, bool canMove, Entity[] IgnoreCollisWith) : base(entity, ComponentType.AABBCollider)
        {
            CanMove = canMove;
            m_map = map;
            Rect = new Rectangle((int)m_entity.GetPos().X, (int)m_entity.GetPos().Y, (int)m_entity.GetDims().X, (int)m_entity.GetDims().Y);
            m_ignore = IgnoreCollisWith;
        }

        public AABBCollider(Entities.Entity entity, Map map, bool canMove, Entity IgnoreCollisWith) : base(entity, ComponentType.AABBCollider)
        {
            CanMove = canMove;
            m_map = map;
            Rect = new Rectangle((int)m_entity.GetPos().X, (int)m_entity.GetPos().Y, (int)m_entity.GetDims().X, (int)m_entity.GetDims().Y);
            m_ignore = new Entity[1];
            m_ignore[0] = IgnoreCollisWith;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public Entity[] IgnoreEntities()
        {
            return m_ignore;
        }

        public Entity GetEntity()
        {
            return m_entity;
        }

        public override void Msg(string msg, params object[] args)
        {
        }

        public override void PreUpdate(float dt)
        {
            
        }

        public override void Update(float dt)
        {
            CollisionResolved = false;
        }

        public override void PostUpdate(float dt)
        {
            Rect = new Rectangle((int)m_entity.GetPos().X, (int)m_entity.GetPos().Y, (int)m_entity.GetDims().X, (int)m_entity.GetDims().Y);
        }

        public override void Init()
        {
            
        }
    }
}
