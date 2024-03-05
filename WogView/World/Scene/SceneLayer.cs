
using System.Xml;
using OpenTK.Mathematics;
using WogView.Graphics;
using WogView.Resources;

namespace WogView.World.Scene;

public sealed class SceneLayer {
    public string Name = "";
    public string Id = "";
    public float X;
    public float Y;
    public float Depth;
    public float ScaleX;
    public float ScaleY;
    public float Rotation;
    public float Alpha = 1f;
    public ByteColor Colorize;
    public ImageResource LoadedImage;

    public Vector4 GetColor4(){
        return new Vector4(Colorize.R / 255f, Colorize.G / 255f, Colorize.B / 255f, Alpha);
    }

    public Vector3 GetScale3D(){
        return new Vector3(ScaleX,ScaleY,1.0f);
    }

    public Vector3 GetPosition3D(){
        return new Vector3(X,Y,Depth);
    }

    public void LoadFromAttributes(XmlAttributeCollection xmlAttributes){
        foreach(XmlNode item in xmlAttributes){
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
                    X = float.Parse(value) * Config.WORLD_SCALE;
                    break;
                case "y":
                    Y = float.Parse(value) * Config.WORLD_SCALE;
                    break;
                case "depth":
                    Depth = float.Parse(value) * Config.WORLD_SCALE; 
                    break;
                case "scalex":
                    ScaleX = float.Parse(value);
                    break;
                case "scaley":
                    ScaleY = float.Parse(value);
                    break;
                case "rotation":
                    Rotation = float.Parse(value);
                    break;
                case "alpha":
                    Alpha = float.Parse(value);
                    break;
                case "colorize":
                    string[] rgb = value.Split(",");
                    Colorize.R = byte.Parse(rgb[0]);
                    Colorize.G = byte.Parse(rgb[1]);
                    Colorize.B = byte.Parse(rgb[2]);
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
    }
}