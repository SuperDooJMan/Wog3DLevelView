
using System.Xml;
using OpenTK.Mathematics;
using WogView.Graphics;
using WogView.Resources;

namespace WogView.World.Scene;



public class Scene {
    public const string LEVELS_FOLDER = "game/res/levels/";
    public List<SceneLayer> Layers = new List<SceneLayer>();
    public ResourceManager Resources = new ResourceManager();
    public static Scene LoadByName(string name){
        Scene result = new Scene();
        XmlDocument doc = new XmlDocument();
        ResourceManager rm = new ResourceManager();
        
        string path = LEVELS_FOLDER + $"{name}/{name}.scene";
        doc.Load(path);

        var layers = doc.GetElementsByTagName("SceneLayer");
        rm.LoadLocalResources(LEVELS_FOLDER + $"{name}/{name}.resrc");

        foreach (XmlNode layer in layers) {
            if (layer == null || layer?.Attributes == null)
                continue;

            result.Layers.Add(new SceneLayer(rm,layer.Attributes));
        }

        result.Resources.LoadLocalResources(LEVELS_FOLDER + $"{name}/{name}.resrc");
        
        result.Layers = result.Layers.OrderBy(l=>l.Position.Z).ToList();

        return result;
    }

    public Scene(){

    }
}
