using System.Text.RegularExpressions;
using System.Xml;

namespace WogView.Resources;

// TODO: Make it collection
public static partial class ResourceManager
{
    // First string is group <Resources id="scene_GoingUp">
    private static Dictionary<string, List<Resource>> Resources = new Dictionary<string, List<Resource>>();

    public static void LoadResources(string f_path)
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(f_path);

        if (xml.DocumentElement == null)
        {
            Console.Error.WriteLine($"Resource file with no root: {f_path}");
            return;
        }
        string default_path = "";
        string default_idprefix = "";
        foreach (XmlNode group in xml.DocumentElement.ChildNodes)
        {
            if (group is XmlComment)
                continue;
            string current_group = group.Attributes.GetNamedItem("id").InnerText;
            Resources.Add(current_group, new List<Resource>());
            foreach (XmlNode resource in group.ChildNodes)
            {
                switch (resource.Name)
                {
                    case "SetDefaults":
                        foreach (XmlAttribute attr in resource.Attributes)
                        {
                            if (attr.Name == "path")
                                default_path = attr.InnerText;
                            if (default_path == "./")
                                default_path = "";
                            if (attr.Name == "idprefix")
                                default_idprefix = attr.InnerText;
                        }
                        break;
                    case "Image":
                        var id = default_idprefix + resource.Attributes?.GetNamedItem("id")?.InnerText;
                        var path = "game/" + default_path + resource.Attributes?.GetNamedItem("path")?.InnerText;
                        ImageResource ir = new ImageResource(id, path);
                        ir.Load();
                        Resources[current_group].Add(ir);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public static Resource GetResource(string group, string id){
        Resource result = Resources[group].Find(r => r.Id == id);
        if (!result.Loaded)
            result.Load();
        return result;
    }

    public static void FreeResources(string group) {
        foreach (var item in Resources[group]) {
            item.Dispose();
        }
    }
}
