using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;

namespace WogView;
public partial class ViewWindow
{
    protected override void OnResize(ResizeEventArgs e) {
        base.OnResize(e);
        
        Console.WriteLine($"Resized from:{ClientSize}, to:{e.Size}");
        int w = e.Width, h = e.Height;
        if (w > h)
            _camera.AspectRatio = w / (float)h;
        else
            _camera.AspectRatio = h / (float)w;
        GL.Viewport(0, 0, w, h);
    }
}