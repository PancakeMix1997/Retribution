﻿using System;
using Microsoft.Xna.Framework;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;

namespace Retribution.Entities.Components
{
    class SpriteRenderer : Component
    {
        private Texture2D m_tex;
        
        private Rectangle m_destRect;

        public SpriteRenderer(XmlNodeList nodelist, Entity entity) : base(entity, ComponentType.SpriteRenderer)
        {
            foreach (XmlNode node in nodelist)
            {
                switch (node.Name)
                {
                    case "image":
                        m_tex = Management.ResourceManager.GetTexture(node.InnerText);
                        m_destRect.Width = m_tex.Width;
                        m_destRect.Height = m_tex.Height;
                        break;
                    case "ResizeToDims":
                        entity.SetDims(new Vector2(m_tex.Width, m_tex.Height));
                        break;
                }
            }
        }

        public SpriteRenderer(Texture2D tex, bool setDimsToTexDims, Entity entity) : base(entity, ComponentType.SpriteRenderer)
        {
            m_tex = tex;
            if (setDimsToTexDims)
                entity.SetDims(new Vector2(tex.Width, tex.Height));
            m_destRect.Width = tex.Width;
            m_destRect.Height = tex.Height;
        }

        public override void PreUpdate(float dt)
        {
        }

        public override void Update(float dt)
        {
        }

        public override void PostUpdate(float dt)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            m_destRect.X = (int)m_entity.GetPos().X;
            m_destRect.Y = (int)m_entity.GetPos().Y;
            spriteBatch.Draw(m_tex, m_destRect, Color.White);
        }

        public override void Msg(string msg, params object[] args)
        {
            //actually for recieveing msgs
        }

        public override void Init()
        {
        }
    }
}
