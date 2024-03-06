
using System.Xml;
using OpenTK.Mathematics;
using WogView.Graphics;
using WogView.Resources;

namespace WogView.World;

public sealed class SceneLayer : SceneObject {
    public string Name = "";
    public string Id = "";
    public Vector3 Scale = Vector3.One;
    public Vector4 Color = Vector4.One;
    public float Rotation;
    public ImageResource LoadedImage;

    public override void LoadFromXMLAttributes(XmlAttributeCollection attributes){
        foreach(XmlNode item in attributes){
            if (item.InnerText == null){
                Console.WriteLine($"Empty: {item.Name}");
                continue;
            }
            string value = item.InnerText;
            
            switch (item.Name) {
                case "name":
                    Name = value;
                    break;
                case "id":
                    Id = value;
                    break;
                case "x":
                    Position.X = float.Parse(value);
                    break;
                case "y":
                    Position.Y = float.Parse(value);
                    break;
                case "depth":
                    Position.Z = float.Parse(value); 
                    break;
                case "scalex":
                    Scale.X = float.Parse(value);
                    break;
                case "scaley":
                    Scale.Y = float.Parse(value);
                    break;
                case "rotation":
                    Rotation = float.Parse(value);
                    break;
                case "alpha":
                    Color.W = float.Parse(value);
                    break;
                case "colorize":
                    string[] rgb = value.Split(",");
                    Color.X = float.Parse(rgb[0]) / 255f;
                    Color.Y = float.Parse(rgb[1]) / 255f;
                    Color.Z = float.Parse(rgb[2]) / 255f;
                    break;
                case "image":
                    var result = (ImageResource)ResourceManager.GetResource(value);
                    
                    if (result != null)
                        LoadedImage = result;
                    
                    break;
                default:
                    Console.WriteLine($"Unknown Value:{item.Name}");
                    break;
            }
        }
        Position *= Config.WORLD_SCALE;
    }
}