using WogView.Graphics;

namespace WogView.Resources;

public static partial class ResourceManager {

    private static Dictionary<string, Image> GlobalImages = new Dictionary<string, Image>();
    private static Dictionary<string, Image> LocalImages = new Dictionary<string, Image>();

    public static Image? GetImage(string name){
        Image? result = null;
        
        if (!LocalImages.TryGetValue(name, out result))
            GlobalImages.TryGetValue(name, out result);
        if (result == null)
            Console.Error.WriteLine("Can't find: " + name);

        return result;
    }

    public static void FreeLocalImages(){
        foreach (var elem in LocalImages) {
            elem.Value.Dispose();
            LocalImages.Remove(elem.Key);
        }
    }
}
