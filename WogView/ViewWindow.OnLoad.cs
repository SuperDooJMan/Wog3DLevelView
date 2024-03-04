
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using WogView.Graphics;
using WogView.World.Scene;
using OpenTK.Windowing.Common;
using System.Xml;
using WogView.Resources;

namespace WogView;
public partial class ViewWindow
{
    private Camera _camera;
    protected override void OnLoad()
    {
        base.OnLoad();
        
        Config.Load();


        GL.ClearColor(0.05f,0.2f,0.05f,1f);
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
        _camera = new Camera(Vector3.UnitZ, Size.X / Size.Y);
        CursorState = CursorState.Grabbed;

        ClientSize = new Vector2i(Config.Width,Config.Height);
        ResourceManager.LoadResources("game/properties/resources.xml");
        scene = Scene.LoadByName(Config.LevelName);
        sceneRenderer = new SceneRenderer();
        sceneRenderer.SceneToDraw = scene;
    }
}
