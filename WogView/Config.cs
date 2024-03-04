using System.Xml;

namespace WogView;
public static class Config
{
    public const float WORLD_SCALE = 0.01f; // Makes everything smaller
    public static string LevelName { get; private set; } = "GoingUp";
    public static int Width = 800, Height = 600;
    // TODO: Serialization
    public static void Load(){
        if (File.Exists("cfg.xml")){
            XmlDocument doc = new XmlDocument();
            try {
                doc.Load("cfg.xml");
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return;
            }

            string? name = doc.GetElementsByTagName("level")[0]?.Attributes?.GetNamedItem("name")?.InnerText;
            if (name != null)
                LevelName = name;
            
            XmlAttributeCollection? collection = doc.GetElementsByTagName("WindowSize")[0]?.Attributes;
            if (collection != null){
                var w = collection.GetNamedItem("width");
                var h = collection.GetNamedItem("height");
                if (w != null){
                    Width = int.Parse(w.InnerText);
                }
                if (h != null){
                    Height = int.Parse(h.InnerText);
                }
            }
        }
    }

}

