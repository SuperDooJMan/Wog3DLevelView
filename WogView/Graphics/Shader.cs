using System.ComponentModel;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace WogView.Graphics;


public class Shader
{
    const string FRAGMENT = @"
        #version 330

        out vec4 outColor;

        in vec2 texCoord;
        
        uniform vec4 color;
        uniform sampler2D tex;

        void main()
        {
            vec4 col = texture(tex, texCoord);
            col *= color;
            if(col.a < (1.0 / 255.0))
                discard;
            outColor = col;
        }";
    
    const string VERTEX = @"
        #version 330 core

        layout(location = 0) in vec3 aPosition;
        layout(location = 1) in vec2 aTexCoord;

        out vec2 texCoord;

        uniform mat4 model;
        uniform mat4 view;
        uniform mat4 projection;

        void main(void)
        {
            texCoord = aTexCoord;
            gl_Position = vec4(aPosition, 1.0) * model * view * projection;
        }";

    public readonly int Handle;
    private readonly Dictionary<string, int> _uniformLocations;
    public readonly bool HasGeometry;
    public static readonly Shader Default = new Shader(VERTEX, FRAGMENT);

    public Shader(string vertSource, string fragSource, string? geomSource = null)
    {
        // Vertex
        var vertexShader = GL.CreateShader(ShaderType.VertexShader);

        GL.ShaderSource(vertexShader, vertSource);

        CompileShader(vertexShader);

        //Fragment
        //shaderSource = File.ReadAllText(fragPath);
        var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

        GL.ShaderSource(fragmentShader, fragSource);

        CompileShader(fragmentShader);

        var geometryShader = 0;
        // Optinal Geometry Shader
        if (geomSource != null)
        {
            HasGeometry = true;
            geometryShader = GL.CreateShader(ShaderType.GeometryShader);

            GL.ShaderSource(geometryShader, geomSource);

            CompileShader(geometryShader);
        }

        Handle = GL.CreateProgram();

        GL.AttachShader(Handle, vertexShader);
        GL.AttachShader(Handle, fragmentShader);

        if (geometryShader != 0)
        {
            GL.AttachShader(Handle, geometryShader);
        }

        LinkProgram(Handle);

        GL.DetachShader(Handle, vertexShader);
        GL.DetachShader(Handle, fragmentShader);

        if (geometryShader != 0)
        {
            GL.DetachShader(Handle, geometryShader);
            GL.DeleteShader(geometryShader);
        }

        GL.DeleteShader(fragmentShader);
        GL.DeleteShader(vertexShader);


        GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

        _uniformLocations = new Dictionary<string, int>();

        for (var i = 0; i < numberOfUniforms; i++)
        {
            var key = GL.GetActiveUniform(Handle, i, out _, out _);

            var location = GL.GetUniformLocation(Handle, key);

            _uniformLocations.Add(key, location);
        }
    }

    public static Shader FromFile(string vertPath, string fragPath, string? geomPath = null){
        string vertSource = File.ReadAllText(vertPath);
        string fragSource = File.ReadAllText(fragPath);

        string? geomSource = null;
        if (geomPath != null)
            geomSource = File.ReadAllText(geomPath);
        
        return new Shader(vertSource,fragSource, geomSource);
    }

    private static void CompileShader(int shader)
    {
        GL.CompileShader(shader);

        GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
        if (code != (int)All.True)
        {
            var infoLog = GL.GetShaderInfoLog(shader);
            throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
        }
    }

    private static void LinkProgram(int program)
    {
        GL.LinkProgram(program);

        GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
        if (code != (int)All.True)
        {
            throw new Exception($"Error occurred whilst linking Program({program})");
        }
    }

    public void Use()
    {
        GL.UseProgram(Handle);
    }

    public int GetAttribLocation(string attribName)
    {
        return GL.GetAttribLocation(Handle, attribName);
    }


    /// <summary>
    /// Set a uniform int on this shader.
    /// </summary>
    /// <param name="name">The name of the uniform</param>
    /// <param name="data">The data to set</param>
    public void SetInt(string name, int data)
    {
        GL.UseProgram(Handle);
        GL.Uniform1(_uniformLocations[name], data);
    }

    /// <summary>
    /// Set a uniform float on this shader.
    /// </summary>
    /// <param name="name">The name of the uniform</param>
    /// <param name="data">The data to set</param>
    public void SetFloat(string name, float data)
    {
        GL.UseProgram(Handle);
        GL.Uniform1(_uniformLocations[name], data);
    }

    /// <summary>
    /// Set a uniform Matrix4 on this shader
    /// </summary>
    /// <param name="name">The name of the uniform</param>
    /// <param name="data">The data to set</param>
    /// <remarks>
    ///   <para>
    ///   The matrix is transposed before being sent to the shader.
    ///   </para>
    /// </remarks>
    public void SetMatrix4(string name, Matrix4 data)
    {
        GL.UseProgram(Handle);
        GL.UniformMatrix4(_uniformLocations[name], true, ref data);
    }

    /// <summary>
    /// Set a uniform Vector3 on this shader.
    /// </summary>
    /// <param name="name">The name of the uniform</param>
    /// <param name="data">The data to set</param>
    public void SetVector3(string name, Vector3 data)
    {
        GL.UseProgram(Handle);
        GL.Uniform3(_uniformLocations[name], data);
    }
    /// <summary>
    /// Set a uniform Vector4 on this shader.
    /// </summary>
    /// <param name="name">The name of the uniform</param>
    /// <param name="data">The data to set</param>
    public void SetVector4(string name, Vector4 data)
    {
        GL.UseProgram(Handle);
        GL.Uniform4(_uniformLocations[name], data);
    }
}