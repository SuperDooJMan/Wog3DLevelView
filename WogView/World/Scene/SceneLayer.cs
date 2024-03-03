using System.Net;
using System.Xml;
using OpenTK.Graphics.ES20;
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
        string? raw_text = attribs.GetNamedItem("name")?.InnerText;
        if (raw_text != null)
            Name = raw_text;
        
        Vector3 pos = Vector3.Zero; // Final pos

        raw_text = attribs.GetNamedItem("x")?.InnerText;
        if (raw_text != null)
            pos.X = float.Parse(raw_text);
        
        raw_text = attribs.GetNamedItem("y")?.InnerText;
        if (raw_text != null)
            pos.Y = float.Parse(raw_text);

        raw_text = attribs.GetNamedItem("depth")?.InnerText;
        if (raw_text != null)
            pos.Z = float.Parse(raw_text);

        Position = pos; // When we done with position we can assign it to instance

        raw_text = attribs.GetNamedItem("rotation")?.InnerText; // Rotation
        if (raw_text != null)
            Rotation = float.Parse(raw_text);
        
        Vector3 scale = Vector3.One; // Getting scale

        raw_text = attribs.GetNamedItem("scalex")?.InnerText;
        if (raw_text != null)
            scale.X = float.Parse(raw_text);
        
        raw_text = attribs.GetNamedItem("scaley")?.InnerText;
        if (raw_text != null)
            scale.Y = float.Parse(raw_text);
        
        Scale = scale;

        Vector4 color = Vector4.One; // Getting color

        raw_text = attribs.GetNamedItem("colorize")?.InnerText;

        if (raw_text != null){
            string[] rgb = raw_text.Split(",");
            
            color.X = float.Parse(rgb[0]) / 255;
            color.Y = float.Parse(rgb[1]) / 255;
            color.Z = float.Parse(rgb[2]) / 255;
        }

        raw_text = attribs.GetNamedItem("alpha")?.InnerText;
        if (raw_text != null)
            color.W = float.Parse(raw_text);
        
        Color = color;
        Image? i = null;
        raw_text = attribs.GetNamedItem("image")?.InnerText;
        
        if (raw_text != null)
            i = r_manager.GetLocalImage(raw_text);
            if (i != null)
                Image = i;
            else
                Image = Image.Missing;
        
    }
}