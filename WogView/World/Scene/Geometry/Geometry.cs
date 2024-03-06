
using System.Xml;
using OpenTK.Mathematics;
using WogView.Resources;

namespace WogView.World;
public abstract class Geometry : SceneObject{
    public string Id = "";
    public bool Static;
    public string[] Tags = {};
    public string Material = "";
    public float Rotation;
    public ImageResource? Image;
    public Vector3 ImagePos;
    public Vector3 ImageScale;
    public float ImageRotation;

    public override void LoadFromXMLAttributes(XmlAttributeCollection attributes){
        foreach (XmlAttribute attrib in attributes)
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
                    Position.X = float.Parse(attrib.InnerText);
                    break;
                case "y":
                    Position.Y = float.Parse(attrib.InnerText);
                    break;
                case "rotation":
                    Rotation = float.Parse(attrib.InnerText);
                    break;
                case "image":
                    Image = (ImageResource)ResourceManager.GetResource(attrib.InnerText);
                    break;
                case "imagepos":
                    string[] pos = attrib.InnerText.Split(",");
                    ImagePos = new Vector3(float.Parse(pos[0]),float.Parse(pos[1]), 0f);
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
        Position *= Config.WORLD_SCALE;
        ImagePos *= Config.WORLD_SCALE;
        Position.Z = 0.1f;
    }

    protected abstract void ParseUnknownAttrib(XmlAttribute attribute);
}
