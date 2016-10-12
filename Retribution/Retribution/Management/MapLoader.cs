using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Retribution.Management
{
    class MapLoader
    {
        public MapLoader()
        {

        }

        public void Load(Entities.Map scenegraph, string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File %s: Not Found!", filePath);
                return;
            }

            scenegraph.ClearMap();

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode root = doc.SelectSingleNode("Map");

            XmlNode backroundNode = root.SelectSingleNode("Backround");
            scenegraph.SetBackroundImage(Management.ResourceManager.GetTexture(backroundNode.InnerText.Trim()));

            XmlNode tileSheetNode = root.SelectSingleNode("TileSheet");
            scenegraph.SetTileSheet(Management.ResourceManager.GetTexture(tileSheetNode.Attributes["Tileset"].Value), 
                Int32.Parse(tileSheetNode.Attributes["TileWidth"].Value), Int32.Parse(tileSheetNode.Attributes["TileHeight"].Value));

           /* foreach (XmlNode tileNode in tileSheetNode.ChildNodes)
            {
                Rectangle sourceRect = new Rectangle();

                foreach(XmlNode node in tileNode.ChildNodes)
                {
                    switch(node.Name)
                    {
                        case "X":
                            sourceRect.X = Int32.Parse(node.InnerText);
                            break;
                        case "Y":
                            sourceRect.Y = Int32.Parse(node.InnerText);
                            break;
                        case "Width":
                            sourceRect.Width = Int32.Parse(node.InnerText);
                            break;
                        case "Height":
                            sourceRect.Height = Int32.Parse(node.InnerText);
                            break;
                    }
                }
                scenegraph.AddTileToDic(sourceRect);
            }
            */

            XmlNode staticNode = root.SelectSingleNode("Static");
            scenegraph.SetTileMap(staticNode.InnerText, Int32.Parse(staticNode.Attributes["Width"].Value), 
                Int32.Parse(staticNode.Attributes["Height"].Value));

            XmlNode dynameicNode = root.SelectSingleNode("Dynamic");
            //load dynamic obj
            
        }
        //for file save look up xml serilizer for dynamic objects
    }
}
