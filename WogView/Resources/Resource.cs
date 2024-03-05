using OpenTK.Graphics.OpenGL;

namespace WogView.Resources;

public abstract class Resource: IDisposable
{
    public string Id { get; protected set; }
    public string Path { get; protected set; }
    public bool Loaded { get; protected set; }

    public Resource(string id, string path){
        Id = id;
        Path = path;
    }

    public abstract void Load();
    public abstract void Dispose();
}
