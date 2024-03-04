using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace WogView.Graphics;

public class Image : IDisposable
{
    public readonly int Handle;

    public readonly int Width, Height;

    public static readonly Image Missing = LoadFromFile("missing");

    public static Image LoadFromFile(string path)
    {
        int handle = GL.GenTexture();
        int width, height = 0;
        // Bind the handle
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, handle);

        StbImage.stbi_set_flip_vertically_on_load(1);
        path = $"{path}.png";
        using (Stream stream = File.OpenRead(path))
        {
            ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
            width = image.Width;
            height = image.Height;
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
        }

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToBorder);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToBorder);


        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        

        return new Image(handle, width, height);
    }

    public Image(int glHandle, int w, int h)
    {
        Handle = glHandle;
        Width = w;
        Height = h;
    }


    public void Use(TextureUnit unit)
    {
        GL.ActiveTexture(unit);
        GL.BindTexture(TextureTarget.Texture2D, Handle);
    }

    public void Dispose()
    {
        GL.BindTexture(TextureTarget.Texture2D, 0);
        GL.DeleteTexture(Handle);
    }

}
