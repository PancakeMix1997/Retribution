using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Retribution.Entities;
using Retribution.Entities.Components;

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

           
            //not loading children
            XmlNode dynamicNode = root.SelectSingleNode("Dynamic");
            foreach (XmlNode node in dynamicNode.ChildNodes)
            {
                if (node.Name == "Layer")
                {
                    var layer = scenegraph.GetLayer(int.Parse(node.Attributes["Depth"].Value));
                    foreach (XmlNode layerNode in node.ChildNodes)
                    {
                        if (layerNode.Name == "Entity")
                        {
                            var temp = new Entity();
                            foreach (XmlNode entityNode in layerNode.ChildNodes)
                            {
                                switch (entityNode.Name)
                                {
                                    case "pos":
                                        temp.SetPos(new Vector2(int.Parse(entityNode.Attributes["X"].Value), int.Parse(entityNode.Attributes["Y"].Value)));
                                        break;
                                    case "tag":
                                        temp.Tag = entityNode.InnerText;
                                        break;
                                    case "name":
                                        temp.Name = entityNode.InnerText;
                                        break;
                                    case "SpriteRenderer":
                                        temp.AddComponent(new SpriteRenderer(entityNode.ChildNodes, temp));
                                        break;
                                    case "AABBCollider":
                                        temp.AddComponent(new AABBCollider(entityNode.ChildNodes, scenegraph, temp));
                                        break;
                                    case "PlayerController":
                                        temp.AddComponent(new PlayerController(temp));
                                        break;
                                    case "ArmedEnemy":
                                        temp.AddComponent(new ArmedEnemy(entityNode.ChildNodes, temp, scenegraph));
                                        break;

                                }
                            }
                            layer.addEntity(temp);
                        }
                    }
                }
                
            }

        }
        //for file save look up xml serilizer for dynamic objects
    }
}
