using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Retribution.Entities.Components
{
    class Camera : Component
    {
        Matrix m_matrix;
        float m_viewPortWidth, m_viewPortHeight;
        float m_zoom;
        public Camera(Entity entity, float viewPortWidth, float viewPortHeight) : base(entity, ComponentType.Camera)
        {
            m_viewPortWidth = viewPortWidth;
            m_viewPortHeight = viewPortHeight;
            m_zoom = 1.0f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
           
        }

        public override void Init()
        {

        }

        public override void Msg(string msg, params object[] args)
        {

        }

        public override void PostUpdate(float dt)
        {
        }

        public Matrix GetMat()
        {
            float x = -m_entity.GetXPos();
            float y = -m_entity.GetYPos();
            if (m_entity.GetXPos() - m_viewPortWidth * 0.5f <= 0)
                x = -m_viewPortWidth * 0.5f;
            if (m_entity.GetYPos() - m_viewPortHeight * 0.5f <= 0)
                y = -m_viewPortHeight * 0.5f; 
              m_matrix =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(x, y, 0)) *
                                         Matrix.CreateRotationZ(m_entity.GetRot()) *
                                         Matrix.CreateScale(new Vector3(m_zoom, m_zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(m_viewPortWidth * 0.5f, m_viewPortHeight * 0.5f, 0));
            
            return m_matrix;
        }

        public override void PreUpdate(float dt)
        {

        }

        public override void Update(float dt)
        {

        }
    }
}
