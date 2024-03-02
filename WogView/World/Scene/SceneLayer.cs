using System.Xml;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using WogView.Graphics;
using WogView.Resources;

namespace WogView.World.Scene;

public class SceneLayer {
    public string Name      { get; set; } = "";
    public Vector3 Position { get; set; } = Vector3.Zero;
    public Vector3 Scale    { get; set; } = Vector3.One;
    public Vector4 Color    { get; set; } = Vector4.Zero;
    public float Rotation   { get; set; } = 0f;
    public Image Image      { get; set; }

    public SceneLayer(Image image) {
        Image = image;
    }

    public SceneLayer(ResourceManager r_manager, XmlAttributeCollection attribs){
        string? name = attribs.GetNamedItem("name")?.InnerText;
        float x = float.Parse(attribs.GetNamedItem("x")?.InnerText);
        float y = float.Parse(attribs.GetNamedItem("y")?.InnerText);
        float d = float.Parse(attribs.GetNamedItem("depth")?.InnerText);
        float rot = float.Parse(attribs.GetNamedItem("rotation")?.InnerText);
        float s_x = float.Parse(attribs.GetNamedItem("scalex")?.InnerText);
        float s_y = float.Parse(attribs.GetNamedItem("scaley")?.InnerText);

        
        string? colorize = attribs.GetNamedItem("colorize")?.InnerText;
        float r = 1, g = 1, b = 1;
        if (colorize != null){
            string[] rgb = colorize.Split(",");
            
            r = float.Parse(rgb[0]) / 255;
            g = float.Parse(rgb[1]) / 255;
            b = float.Parse(rgb[2]) / 255;
        }

        float a = float.Parse(attribs.GetNamedItem("alpha")?.InnerText);

        Name = name != null ? name : "";
        Position = new Vector3(x,y,d);
        Rotation = rot;
        Scale = new Vector3(s_x,s_y,1);
        Color = new Vector4(r,g,b,a);
        Image = r_manager.GetLocalImage(attribs.GetNamedItem("image")?.InnerText);
    }
}