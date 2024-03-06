using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace WogView.Graphics;

public class Graphics : IDisposable
{
    private readonly Mesh _imageQuad = Mesh.QuadMesh;
    private readonly ViewWindow _window;
    public Camera Camera { get; private set; }
    public Shader Shader { get; set; }
    private float _aspectRatio;
    public float AspectRatio {
        get{ return _aspectRatio; }
        set{
            _aspectRatio = value;
            Camera.AspectRatio = value;
            }
    }

    public Graphics(ViewWindow window){
        _window = window;
        Camera = new Camera(Vector3.UnitZ, window.ClientSize.X / window.ClientSize.Y);
        Shader = Shader.Default;
    }
    public void BeginDraw(){
        var model = Matrix4.Identity;
        Shader.SetMatrix4("model", model);
        Shader.SetMatrix4("view", Camera.GetViewMatrix());
        Shader.SetMatrix4("projection", Camera.GetProjectionMatrix());
    }
    public void DrawImage(Image image, Vector3 position, Vector3 scale, Vector4 color, float rotation = 0f){
        Vector3 size = new Vector3(image.Width * scale.X,image.Height * scale.Y, 1.0f);
        Matrix4 model = Matrix4.CreateScale(size * Config.WORLD_SCALE) *
                        Matrix4.CreateRotationZ(rotation) *
                        Matrix4.CreateTranslation(position);
        
        Shader.SetMatrix4("model", model);
        Shader.SetVector4("color", color);
        image.Use(TextureUnit.Texture0);

        _imageQuad.Draw();
    }

    public void Dispose() {
        
    }

}
