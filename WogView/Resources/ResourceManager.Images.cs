using WogView.Graphics;

namespace WogView.Resources;

public partial class ResourceManager {

    private static Dictionary<string, Image> GlobalImages = new Dictionary<string, Image>();
    private Dictionary<string, Image> LocalImages = new Dictionary<string, Image>();

    public static Image? GetGlobalImaeg(string name){
        Image? result = null;
        GlobalImages.TryGetValue(name, out result);

        return result;
    }

    public Image? GetLocalImage(string name){
        Image? result = null;
        
        if (!LocalImages.TryGetValue(name, out result))
            GlobalImages.TryGetValue(name, out result);

        return result;
    }

}
