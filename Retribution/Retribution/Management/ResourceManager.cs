using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Retribution.Management
{
    static class ResourceManager
    {
        private static ContentManager m_content;
        private static Dictionary<string, Texture2D> m_textures;

        public static void Init(ContentManager con)
        {
            m_textures = new Dictionary<string, Texture2D>();
            m_content = con;
        }

        public static Texture2D GetTexture(string name)
        {
            Texture2D temp;
            if (!m_textures.TryGetValue(name, out temp))
            {
                temp = m_content.Load<Texture2D>(name);
                m_textures.Add(name, temp);
            }
            return temp;
        }
    }
}
