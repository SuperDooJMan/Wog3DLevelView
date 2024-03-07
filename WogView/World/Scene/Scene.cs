
using System.Xml;
using OpenTK.Mathematics;
using WogView.Graphics;
using WogView.Resources;

namespace WogView.World;



public class Scene {
    private const string SceneLayer = "SceneLayer";
    private const string CompositeGeometry = "compositegeom";
    private const string Rectangle = "rectangle";
    private const string Circle = "circle";
    private const string LinearForceField = "linearforcefield";

    public const string LEVELS_FOLDER = "game/res/levels/";
    public List<SceneObject> SceneObjects = new List<SceneObject>();
    
    public Scene(){

    }

    public void Load(string level_name){
        if(!Directory.Exists(LEVELS_FOLDER + level_name)){
            throw new DirectoryNotFoundException();
        }
        string level_path = LEVELS_FOLDER + $"{level_name}/{level_name}";

        XmlDocument doc = new XmlDocument();
        doc.Load(level_path + ".scene");

        ResourceManager.LoadResources(level_path + ".resrc");
        
        if (doc.DocumentElement == null){
            throw new Exception("Level is empty!");
        }

        foreach (XmlNode node in doc.DocumentElement.ChildNodes) {
            if (node.Attributes == null || node is XmlComment)
                continue;
            switch (node.Name)
            {
                case SceneLayer:
                    var layer = new SceneLayer();
                    layer.LoadFromXMLAttributes(node.Attributes);
                    SceneObjects.Add(layer);
                    break;
                case CompositeGeometry:
                    var compose = new CompositeGeometry();
                    compose.LoadFromXMLAttributes(node.Attributes);
                    compose.AddChilds(node.ChildNodes);
                    SceneObjects.Add(compose);
                    break;
                case Rectangle:
                    var rect = new Rectangle();
                    rect.LoadFromXMLAttributes(node.Attributes);
                    SceneObjects.Add(rect);
                    break;
                case Circle:
                    var circle = new Circle();
                    circle.LoadFromXMLAttributes(node.Attributes);
                    SceneObjects.Add(circle);
                    break;
                case LinearForceField:
                    var lff = new LinearForceField();
                    lff.LoadFromXMLAttributes(node.Attributes);
                    SceneObjects.Add(lff);
                    break;
                default:
                    break;
            }
        }
        SceneObjects = SceneObjects.OrderBy(o=>o.Position.Z).ToList();
    }

    public void Draw(Graphics.Graphics graphics){
        foreach (SceneObject obj in SceneObjects) {
            Image? image = null;
            Vector3 position = Vector3.Zero;
            Vector3 scale = Vector3.One;
            Vector4 color = Vector4.One;
            float rotation = 0f;

            if (obj is SceneLayer layer){// Re do
                image = layer.LoadedImage.Image;
                if(image == null)
                    continue;
                position = layer.Position;
                scale = layer.Scale;
                color = layer.Color;
                rotation = MathHelper.DegreesToRadians(layer.Rotation); 
            } else if (obj is CompositeGeometry composite){
                image = composite.Image?.Image;
                position = composite.ImagePos;
                scale = composite.ImageScale;
                rotation = composite.ImageRotation;

                foreach (Geometry geom in composite.Geometries) {
                    if (geom.Image == null)
                        continue;
                    graphics.DrawImage(geom.Image.Image, geom.ImagePos,geom.ImageScale,Vector4.One,geom.ImageRotation);
                }
                if(image == null)
                    continue;
            } else if (obj is Geometry geom){
                image = geom.Image?.Image;
                if(image == null)
                    continue;
                position = geom.ImagePos;
                scale = geom.ImageScale;
                rotation = geom.ImageRotation;
            } else if (obj is LinearForceField lff){
                if (lff.IsWater){
                    graphics.DrawColor(lff.Position, lff.Size, lff.Color, 0f);
                    continue;
                }
            }

            if (image != null)
                graphics.DrawImage(image, position, scale, color, rotation);
        }
    }
}
