using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retribution.Entities.Components;

namespace Retribution.Entities
{
    class HealthBar
    {
        private PlayerController m_player;
        private Texture2D m_texFull, m_texHalf, m_texLow;

        public HealthBar(Entity player, Texture2D full, Texture2D half, Texture2D low)
        {
            m_texFull = full;
            m_texHalf = half;
            m_texLow = low;
            m_player = player.GetComponent<PlayerController>(ComponentType.PlayerController);
        }


        public void Draw(SpriteBatch spritebatch)
        {
            //spritebatch.Draw(m_texture, new Rectangle(0, 0, m_player.GetHealth(), 20),Color.Blue);
            if (m_player.GetHealth() == 3)
                spritebatch.Draw(m_texFull, new Rectangle(0, 0, 300, 20), Color.White);
            else if (m_player.GetHealth() == 2)
                spritebatch.Draw(m_texHalf, new Rectangle(0, 0, 200, 20), Color.White);
            else if (m_player.GetHealth() == 1)
                spritebatch.Draw(m_texLow, new Rectangle(0, 0, 100, 20), Color.White);


        }


    }
}
