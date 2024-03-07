using OpenTK.Mathematics;
using System.Xml;

namespace WogView.World;

public enum ForceType{
    force = 0,
    gravity = 1,
}

public class LinearForceField : SceneObject {
    public string Id = "";
    public Vector3 Size = Vector3.One;
    public ForceType Type;
    public Vector2 Force = Vector2.Zero;
    public float DampeningFactor = 0f;
    public float RotDampeningfac = 0f;
    public bool Antigrav;
    public Vector4 Color = Vector4.One;
    public bool Enabled;
    public bool IsWater;
    public bool GeomOnly;
    public override void LoadFromXMLAttributes(XmlAttributeCollection attributes) {
        foreach (XmlAttribute attrib in attributes)
        {
            switch (attrib.Name)
            {   
                case "id":
                    Id = attrib.InnerText;
                    break;
                case "center":
                    string[] xy = attrib.InnerText.Split(",");
                    Position.X = float.Parse(xy[0]);
                    Position.Y = float.Parse(xy[1]);
                    break;
                case "depth":
                    Position.Z = float.Parse(attrib.InnerText);
                    break;
                case "width":
                    Size.X = float.Parse(attrib.InnerText);
                    break;
                case "height":
                    Size.Y = float.Parse(attrib.InnerText);
                    break;
                case "force":
                    string[] force = attrib.InnerText.Split(",");
                    Force.X = float.Parse(force[0]);
                    Force.Y = float.Parse(force[1]);
                    break;
                case "type":
                    SetForce(attrib.InnerText);
                    break;
                case "dampeningfactor":
                    DampeningFactor = float.Parse(attrib.InnerText);
                    break;
                case "antigrav":
                    Antigrav = bool.Parse(attrib.InnerText);
                    break;
                case "geomonly":
                    GeomOnly = bool.Parse(attrib.InnerText);
                    break;
                case "enabled":
                    Enabled = bool.Parse(attrib.InnerText);
                    break;
                case "water":
                    IsWater = bool.Parse(attrib.InnerText);
                    
                    break;
                case "color":
                    string[] rgba = attrib.InnerText.Split(",");
                    Color.X = float.Parse(rgba[3]) / 255f; 
                    Color.Y = float.Parse(rgba[2]) / 255f; 
                    Color.Z = float.Parse(rgba[1]) / 255f; 
                    Color.W = float.Parse(rgba[0]) / 255f; 
                    break;
                default:
                    break;
            }

        }
        Position *= Config.WORLD_SCALE;
    }

    public void SetForce(string type){
        switch (type)
        {
            case "gravity":
                Type = ForceType.gravity;
                break;
            case "force":
                Type = ForceType.force;
                break;
            default:
                break;
        }
        
    }
}
