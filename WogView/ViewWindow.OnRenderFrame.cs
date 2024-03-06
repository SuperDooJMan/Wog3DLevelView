using OpenTK.Graphics.ES11;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using WogView.Graphics;
using WogView.World;
namespace WogView;

public partial class ViewWindow {
    private Scene Scene;
    private Graphics.Graphics Graphics;
    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        //Draw
        Graphics.BeginDraw();
        Scene.Draw(Graphics);

        SwapBuffers();
    }
}