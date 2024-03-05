
using System.Xml;
using OpenTK.Mathematics;
using WogView.Resources;

namespace WogView.World.Scene;
public abstract class Geometry{
    public string Id;
    public bool Static;
    public string[] Tags;
    public string Material;
    public float X;
    public float Y;
    public float Rotation;
    public ImageResource? Image;
    public Vector3 ImagePos;
    public Vector3 ImageScale;
    public float ImageRotation;

    public void LoadFromAttributes(XmlAttributeCollection xmlAttributes){
        foreach (XmlAttribute attrib in xmlAttributes)
        {
            switch (attrib.Name) {
                case "id":
                    Id = attrib.InnerText;
                    break;
                case "static":
                    Static = bool.Parse(attrib.InnerText);
                    break;
                case "tag":
                    Tags = attrib.InnerText.Split(",");
                    break;
                case "material":
                    Material = attrib.InnerText;
                    break;
                case "x":
                    X = float.Parse(attrib.InnerText) * Config.WORLD_SCALE;
                    break;
                case "y":
                    Y = float.Parse(attrib.InnerText) * Config.WORLD_SCALE;
                    break;
                case "rotation":
                    Rotation = float.Parse(attrib.InnerText);
                    break;
                case "image":
                    Image = (ImageResource)ResourceManager.GetResource(attrib.InnerText);
                    break;
                case "imagepos":
                    string[] pos = attrib.InnerText.Split(",");
                    ImagePos = new Vector3(float.Parse(pos[0]),float.Parse(pos[1]), 0f) * Config.WORLD_SCALE;
                    break;
                case "imagescale":
                    string[] scale = attrib.InnerText.Split(",");
                    ImageScale = new Vector3(float.Parse(scale[0]),float.Parse(scale[1]), 1f);
                    break;
                case "imagerot":
                    ImageRotation = float.Parse(attrib.InnerText);
                    break;
                default:
                    ParseUnknownAttrib(attrib);
                    break;
            }
        }
    }
    public Vector3 GetPosition3D(){
        return new Vector3(X,Y,0f);
    }

    protected abstract void ParseUnknownAttrib(XmlAttribute attribute);
}
