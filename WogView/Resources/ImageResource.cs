using WogView.Graphics;

namespace WogView.Resources;
public class ImageResource : Resource
{
    public Image Image { get; protected set; } = Image.Missing;

    public ImageResource(string id, string path) : base(id,path){

    }

    public override void Load() {
        if (Path == null){
            Console.WriteLine($"Resource have no path: {Id}");
            return;
        }

        Image = Image.LoadFromFile(Path);
        Loaded = true;
    }

    public override void Dispose() {
        // Do not dispose missing placeholder
        if (Image != Image.Missing)
            Image.Dispose();
    }
}
