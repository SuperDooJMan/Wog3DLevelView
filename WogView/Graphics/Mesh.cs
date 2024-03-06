using OpenTK.Graphics.OpenGL4;

namespace WogView.Graphics;
public class Mesh : IDisposable{

    private static readonly float[] _quad_vertices =
    {
        // Position         Texture coordinates
         0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
         0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
        -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
        -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
    };

    private static readonly uint[] _quad_indices =
    {
        0, 1, 3,
        1, 2, 3
    };
    public static readonly Mesh QuadMesh = new Mesh(_quad_vertices, _quad_indices);
    private int _EBO;
    private int _VBO;
    private int _VAO;
    private int _indices_count;

    public Mesh(float[] vertices, uint [] indices){
        _VAO = GL.GenVertexArray();
        GL.BindVertexArray(_VAO);

        _VBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        _EBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        
        // Vertex Position
        var location = Shader.Default.GetAttribLocation("aPosition");
        GL.EnableVertexAttribArray(location);
        GL.VertexAttribPointer(location, 3,VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

        // Vertex Texture Coord
        location = Shader.Default.GetAttribLocation("aTexCoord");
        GL.EnableVertexAttribArray(location);
        GL.VertexAttribPointer(location, 2,VertexAttribPointerType.Float, true, 5 * sizeof(float), 3 * sizeof(float));
        
        _indices_count = indices.Length;

    }

    public void Draw(){
        Bind();
        GL.DrawElements(PrimitiveType.Triangles, _indices_count, DrawElementsType.UnsignedInt, 0);
        //Unbind();
    }

    public void Bind(){
        GL.BindVertexArray(_VAO);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _VBO);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _EBO);
    }

    public static void Unbind(){
        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_EBO);
        GL.DeleteBuffer(_VBO);
        GL.DeleteVertexArray(_VAO);
    }

}