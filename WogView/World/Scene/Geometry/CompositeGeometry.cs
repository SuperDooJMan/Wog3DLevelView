using System.Xml;

namespace WogView.World;

public class CompositeGeometry : Geometry
{
    public List<Geometry> Geometries = new List<Geometry>();

    public void AddChilds(XmlNodeList Nodes){
        foreach (XmlNode item in Nodes) {
            if (item.Attributes == null)
                continue;
            switch (item.Name)
            {
                case "rectangle":
                    var rect = new Rectangle();
                    rect.LoadFromXMLAttributes(item.Attributes);
                    Geometries.Add(rect);
                    break;
                case "circle":
                    var circle = new Circle();
                    circle.LoadFromXMLAttributes(item.Attributes);
                    Geometries.Add(circle);
                    break;
                default:
                    break;
            }
        }
    }
    protected override void ParseUnknownAttrib(XmlAttribute attribute) {
        return;
    }
}
