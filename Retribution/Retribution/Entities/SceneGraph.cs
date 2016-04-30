using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Retribution.Entities
{
    class SceneGraph
    {
        private List<Layer> m_layers;
        private List<Layer> m_guiLayers;

        public SceneGraph()
        {
            m_layers = new List<Layer>();
        }

        public void ClearMap()
        {
            m_layers.Clear();
        }

        public void ClearGui()
        {
            m_guiLayers.Clear();
        }
        
        public void NewLayer()
        {
            m_layers.Add(new Layer());
        }

        public Layer GetLayer(int depth)
        {
            return m_layers[depth];
        }

        public void Update(float dt)
        {
            foreach(Layer i in m_layers)
            {
                i.Update(dt);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Layer i in m_layers)
            {
                i.Draw(spriteBatch);
            }
        }
    }
}
