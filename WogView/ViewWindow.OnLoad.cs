
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using WogView.Resources;
using WogView.World;

namespace WogView;
public partial class ViewWindow
{
    protected override void OnLoad()
    {
        base.OnLoad();
        
        Config.Load();

        Graphics = new Graphics.Graphics(this);
        ClientSize = new Vector2i(Config.Width,Config.Height);

        GL.ClearColor(0.05f,0.2f,0.05f,1f);
        // OpenGl SetUp
        GL.DebugMessageCallback(DebugMessageDelegate, IntPtr.Zero);
        GL.Enable(EnableCap.DebugOutput);
        
        //GL.Enable(EnableCap.DepthTest);
        // Blending
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        GL.BlendFuncSeparate(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha, BlendingFactorSrc.One, BlendingFactorDest.Zero);
        GL.Enable(EnableCap.Blend);

        CursorState = CursorState.Grabbed;

        ResourceManager.LoadResources("game/properties/resources.xml");
        Scene = new Scene();
        Scene.Load(Config.LevelName);
    }
}
