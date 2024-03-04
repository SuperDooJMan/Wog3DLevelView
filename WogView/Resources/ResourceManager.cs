using System.Collections;
using System.Net.Mime;
using System.Xml;
using OpenTK.Windowing.Common.Input;

namespace WogView.Resources;

public static partial class ResourceManager {

    public static void LoadGlobalResources(string g_resrc_path){
        
    }

    public static void LoadLocalResources(string l_resrc_path){
        XmlDocument xml = new XmlDocument();
        xml.Load(l_resrc_path);

        var images = xml.GetElementsByTagName("Image");

        foreach (XmlNode item in images) {
            if (item.Attributes == null)
                continue;

            LocalImages.Add(item.Attributes.GetNamedItem("id")?.InnerText,Graphics.Image.LoadFromFile("game/"+item.Attributes.GetNamedItem("path")?.InnerText));
        }

    }

}
