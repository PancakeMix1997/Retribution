using Microsoft.Xna.Framework.Graphics;

namespace Retribution.Entities.Components
{
    enum ComponentType
    {
        AABBCollider,
        PlayerController,
        SpriteRenderer,
        ArmedEnemy,
        Projectile,
        Camera
    }

    abstract class Component
    {
        protected Entity m_entity;
        protected ComponentType m_type;
        protected bool m_deleted;

        public Component(Entity entity, ComponentType type)
        {
            m_type = type;
            m_entity = entity;
        }

        public ComponentType GetComponentType()
        {
            return m_type;
        }

        abstract public void Init();
        abstract public void PreUpdate(float dt);
        abstract public void Update(float dt);
        abstract public void PostUpdate(float dt);
        abstract public void Draw(SpriteBatch spriteBatch);
        abstract public void Msg(string msg, params object[] args);
    }
}
