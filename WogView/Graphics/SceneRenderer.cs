using System.Text.RegularExpressions;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using WogView.World.Scene;

namespace WogView.Graphics;
public class SceneRenderer
{
    public Scene? SceneToDraw;

    // private static readonly Vertex[] _vertices = { Sucks...
    //     new Vertex(x: .5f, y: .5f, z:.0f, u:1f, v:1f),
    //     new Vertex(x: .5f, y:-.5f, z:.0f, u:1f, v:0f),
    //     new Vertex(x:-.5f, y:-.5f, z:.0f, u:0f, v:0f),
    //     new Vertex(x:-.5f, y: .5f, z:.0f, u:0f, v:1f)
    // };
    private static readonly float[] _vertices =
    {
        // Position         Texture coordinates
         0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
         0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
        -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
        -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
    };
    private static readonly uint[] _indices =
    {
        0, 1, 3,
        1, 2, 3
    };
    private static int _EBO;
    private static int _VBO;

    public static int _VAO;
    public SceneRenderer()
    {
        // Binding Vao Locations
        _VAO = GL.GenVertexArray();
        GL.BindVertexArray(_VAO);

        _VBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

        _EBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);
        
        // Vertex Position
        var location = Shader.Default.GetAttribLocation("aPosition");
        GL.EnableVertexAttribArray(location);
        GL.VertexAttribPointer(location, 3,VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

        // Vertex Texture Coord
        location = Shader.Default.GetAttribLocation("aTexCoord");
        GL.EnableVertexAttribArray(location);
        GL.VertexAttribPointer(location, 2,VertexAttribPointerType.Float, true, 5 * sizeof(float), 3 * sizeof(float));
        
        Shader.Default.SetInt("tex", 0);
    }

    public void Draw()
    {
        if (SceneToDraw == null) return;
        bool geometry_drawn = false;
        foreach (SceneLayer layer in SceneToDraw.Layers) {
            if (layer.Depth >= 0 && !geometry_drawn) { // We Reached a middle, drawing stuff that have to be in the middle
                foreach(Geometry geometry in SceneToDraw.Geometries){
                    if (geometry is CompositeGeometry compose){
                        DrawGeometryImage(geometry);
                        foreach (var item in compose.Geometries)
                            DrawGeometryImage(item);
                    }else
                        DrawGeometryImage(geometry);
                }
                geometry_drawn = true;
            }
            DrawLayer(layer);
        }
    }

    private void DrawLayer(SceneLayer layer){
        Image image = layer.LoadedImage.Image;
        Vector3 size = new Vector3(image.Width * layer.ScaleX * Config.WORLD_SCALE,image.Height * layer.ScaleY * Config.WORLD_SCALE, 1f);
        
        Matrix4 model = Matrix4.CreateScale(size) *
                        Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(layer.Rotation)) *
                        Matrix4.CreateTranslation(layer.GetPosition3D());
        
        Shader.Default.SetMatrix4("model", model);
        Shader.Default.SetVector4("color", layer.GetColor4());
        image.Use(TextureUnit.Texture0);

        GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
    }

    private void DrawGeometryImage(Geometry geometry){
        if(geometry.Image != null){
            Image image = geometry.Image.Image;

            Vector3 size = new Vector3(image.Width * geometry.ImageScale.X * Config.WORLD_SCALE,image.Height * geometry.ImageScale.Y * Config.WORLD_SCALE, 1f);

            Matrix4 model = Matrix4.CreateScale(size) *
                            Matrix4.CreateRotationZ(geometry.ImageRotation) * // Now its radians
                            Matrix4.CreateTranslation(geometry.ImagePos);

            Shader.Default.SetMatrix4("model", model);
            Shader.Default.SetVector4("color", Vector4.One);
            image.Use(TextureUnit.Texture0);

            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }

}
