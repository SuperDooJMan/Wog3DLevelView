using System.Xml;

namespace WogView.World.Scene;

public class Rectangle : Geometry
{
    float Width, Height;
    protected override void ParseUnknownAttrib(XmlAttribute attribute)
    {
        switch (attribute.Name)
        {
            case "width":
                Width = float.Parse(attribute.InnerText);
                break;
            case "height":
                Width = float.Parse(attribute.InnerText);
                break;
            default:
                break;
        }
        
    }
}
