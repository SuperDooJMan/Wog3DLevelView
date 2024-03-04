using System.Xml;
using OpenTK.Mathematics;
using WogView.Graphics;
using WogView.Resources;

namespace WogView.World.Scene;
public class SceneChild {
    public string Name = "";
    public string Id = "";
    public float X;
    public float Y;
    public float Depth;
    public float ScaleX;
    public float ScaleY;
    public float Rotation;
    public float Alpha;
    public byte R;
    public byte G;
    public byte B;
    public string Image = "";
    public Image DrawableImage = Graphics.Image.Missing;
    public SceneChild(){}

    public Vector4 Get4Color(){
        return new Vector4(R / 255f, G / 255f, B / 255f, Alpha);
    }

    public Vector3 Get3DScale(){
        return new Vector3(ScaleX,ScaleY,1.0f);
    }

    public Vector3 Get3DPosition(){
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
                    R = byte.Parse(rgb[0]);
                    G = byte.Parse(rgb[1]);
                    B = byte.Parse(rgb[2]);
                    break;
                case "image":
                    Image = value;
                    var result = ResourceManager.GetImage(value);
                    
                    if (result != null)
                        DrawableImage = result;
                    
                    break;
                default:
                    Console.WriteLine($"Unknown Value:{value}");
                    break;
            }
        }
        Console.WriteLine("Resource Loaded!");
    }


}