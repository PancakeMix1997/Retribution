using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retribution.Management
{
    class MapLoader
    {
        public MapLoader()
        {

        }

        public void Load(Entities.SceneGraph scenegraph, string filePath)
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine("File %s: Not Found!", filePath);
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            scenegraph.ClearMap();

            

        }
    }
}
