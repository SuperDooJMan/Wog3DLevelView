
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using WogView.Graphics;
using WogView.World.Scene;
using OpenTK.Windowing.Common;

namespace WogView;
public partial class ViewWindow
{
    private Camera _camera;
    protected override void OnLoad()
    {
        base.OnLoad();

        // OpenGl SetUp
        GL.DebugMessageCallback(DebugMessageDelegate, IntPtr.Zero);
        GL.Enable(EnableCap.DebugOutput);

        //GL.Enable(EnableCap.DepthTest);
        // Blending
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        GL.BlendFuncSeparate(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha, BlendingFactorSrc.One, BlendingFactorDest.Zero);
        GL.Enable(EnableCap.Blend);

        Shader.Default.SetInt("tex", 0);
        Shader.Default.Use();
        _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);
        CursorState = CursorState.Grabbed;

        scene = Scene.LoadByName("UpperShaft");
        sceneRenderer = new SceneRenderer();
        sceneRenderer.SceneToDraw = scene;
    }
}
