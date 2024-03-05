using System.Xml;

namespace WogView.World.Scene;

public class CompositeGeometry : Geometry
{
    public List<Geometry> Geometries = new List<Geometry>();

    public void AddChilds(XmlNodeList Nodes){
        foreach (XmlNode item in Nodes) {
            switch (item.Name)
            {
                case "rectangle":
                    var rect = new Rectangle();
                    rect.LoadFromAttributes(item.Attributes);
                    Geometries.Add(rect);
                    break;
                case "circle":
                    var circle = new Circle();
                    circle.LoadFromAttributes(item.Attributes);
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
