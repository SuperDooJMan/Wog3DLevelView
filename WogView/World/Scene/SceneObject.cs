
using System.Xml;
using OpenTK.Mathematics;

namespace WogView.World;
public abstract class SceneObject
{
    public Vector3 Position = Vector3.Zero;
    public abstract void LoadFromXMLAttributes(XmlAttributeCollection attributes);
}
