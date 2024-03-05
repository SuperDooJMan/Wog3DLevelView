using System.Xml;

namespace WogView.World.Scene;

public class Circle : Geometry
{
    public float Radius;
    protected override void ParseUnknownAttrib(XmlAttribute attribute)
    {
        switch (attribute.Name)
        {
            case "radius":
                Radius = float.Parse(attribute.InnerText);
                break;
            default:
                break;
        }
        
    }
}
