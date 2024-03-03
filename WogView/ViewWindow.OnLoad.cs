
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using WogView.Graphics;
using WogView.World.Scene;
using OpenTK.Windowing.Common;
using System.Xml;

namespace WogView;
public partial class ViewWindow
{
    private Camera _camera;
    protected override void OnLoad()
    {
        base.OnLoad();
        
        string levelname = "GoingUp";
        if (File.Exists("cfg.xml")){
            XmlDocument doc = new XmlDocument();
            doc.Load("cfg.xml");
            string? name = doc.GetElementsByTagName("level")[0]?.InnerText;
            if (name != null)
                levelname = name; // TODO: Fix all nullable refs.
        }

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
        _camera = new Camera(Vector3.UnitZ * 100, Size.X / Size.Y);
        CursorState = CursorState.Grabbed;

        scene = Scene.LoadByName(levelname);
        sceneRenderer = new SceneRenderer();
        sceneRenderer.SceneToDraw = scene;
    }
}
