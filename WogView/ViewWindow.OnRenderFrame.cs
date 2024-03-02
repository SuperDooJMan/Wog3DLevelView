using OpenTK.Graphics.ES11;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using WogView.Graphics;
using WogView.World.Scene;

namespace WogView;

public partial class ViewWindow {
    private SceneRenderer sceneRenderer;
    private Scene scene;
    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        //Draw
        var model = Matrix4.Identity;
        Shader.Default.SetMatrix4("model", model);
        Shader.Default.SetMatrix4("view", _camera.GetViewMatrix());
        Shader.Default.SetMatrix4("projection", _camera.GetProjectionMatrix());

        sceneRenderer.Draw();

        SwapBuffers();
    }
}