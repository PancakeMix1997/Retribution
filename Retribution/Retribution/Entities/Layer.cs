using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Retribution.Entities
{
    class Layer
    {
        private List<Entity> m_entites;

        public Layer()
        {
            m_entites = new List<Entity>();
        }

        public void addEntity(Entity entity)
        {
            m_entites.Add(entity);
        }

        public void Update(float dt)
        {
            foreach(Entity i in m_entites)
            {
                i.Update(dt);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Entity i in m_entites)
            {
                i.Draw(spriteBatch);
            }
        }
    }
}
