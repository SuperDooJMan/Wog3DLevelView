
using System.Xml;
using OpenTK.Mathematics;
using WogView.Graphics;
using WogView.Resources;

namespace WogView.World.Scene;



public class Scene {
    public const string LEVELS_FOLDER = "game/res/levels/";
    public List<SceneChild> Children = new List<SceneChild>();
    public static Scene LoadByName(string name){
        Scene result = new Scene();
        XmlDocument doc = new XmlDocument();
        
        string path = LEVELS_FOLDER + $"{name}/{name}.scene";
        doc.Load(path);

        var layers = doc.GetElementsByTagName("SceneLayer");
        ResourceManager.LoadLocalResources(LEVELS_FOLDER + $"{name}/{name}.resrc");

        foreach (XmlNode layer in layers) {
            if (layer == null || layer?.Attributes == null)
                continue;
            var child = new SceneChild();
            child.LoadFromAttributes(layer.Attributes);
            result.Children.Add(child);
        }

        result.Children = result.Children.OrderBy(l=>l.Depth).ToList();

        return result;
    }

    public Scene(){

    }
}
