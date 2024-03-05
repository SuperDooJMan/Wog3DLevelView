
using System.Xml;
using OpenTK.Mathematics;
using WogView.Graphics;
using WogView.Resources;

namespace WogView.World.Scene;



public class Scene {
    public const string LEVELS_FOLDER = "game/res/levels/";
    public List<SceneLayer> Layers = new List<SceneLayer>();
    public List<Geometry> Geometries = new List<Geometry>();
    public static Scene LoadByName(string name){
        Scene result = new Scene();
        XmlDocument doc = new XmlDocument();
        
        string path = LEVELS_FOLDER + $"{name}/{name}.scene";
        doc.Load(path);

        var layers = doc.GetElementsByTagName("SceneLayer");
        ResourceManager.LoadResources(LEVELS_FOLDER + $"{name}/{name}.resrc");

        foreach (XmlNode item in doc.DocumentElement.ChildNodes)
        {
            switch (item.Name)
            {
                case "SceneLayer":
                    var layer = new SceneLayer();
                    layer.LoadFromAttributes(item.Attributes);
                    result.Layers.Add(layer);
                    break;
                case "compositegeom":
                    var compost = new CompositeGeometry();
                    compost.LoadFromAttributes(item.Attributes);
                    compost.AddChilds(item.ChildNodes);
                    result.Geometries.Add(compost);
                    break;
                case "rectangle":
                    var rect = new Rectangle();
                    rect.LoadFromAttributes(item.Attributes);
                    result.Geometries.Add(rect);
                    break;
                case "circle":
                    var circle = new Circle();
                    circle.LoadFromAttributes(item.Attributes);
                    result.Geometries.Add(circle);
                    break;
                default:
                    break;
            }
            
        }

        result.Layers = result.Layers.OrderBy(l=>l.Depth).ToList();

        return result;
    }

    public Scene(){

    }
}
