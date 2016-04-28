using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Retribution.Entities.Components
{
    class SpriteRenderer : Component
    {
        private Texture2D m_tex;
        private Entity m_entity;
        private Rectangle m_destRect;

        public SpriteRenderer(Texture2D tex, Entity entity)
        {
            m_tex = tex;
            m_entity = entity;
            m_destRect.Width = tex.Width;
            m_destRect.Height = tex.Height;
        }

        public override void Update(float dt)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            m_destRect.X = (int)m_entity.GetPos().X;
            m_destRect.Y = (int)m_entity.GetPos().Y;
            spriteBatch.Draw(m_tex, m_destRect, Color.White);
        }

        public override void SendMsg(string msg)
        {
            //actually for recieveing msgs
        }

    }
}
