using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;

namespace WogView;
public partial class ViewWindow
{
    protected override void OnResize(ResizeEventArgs e) {
        base.OnResize(e);
        int w = e.Width, h = e.Height;
        _camera.AspectRatio = w / h;
        GL.Viewport(0, 0, w, h);
    }
}