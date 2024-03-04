namespace WogView.Resources;

public abstract class Resource: IDisposable
{
    public string Id { get; protected set; }
    public string Path { get; protected set; }
    public bool Loaded { get; protected set; }
    public abstract void Load();
    public abstract void Dispose();
}
