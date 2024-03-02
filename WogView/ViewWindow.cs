using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace WogView;
public partial class ViewWindow : GameWindow
{
    private bool _firstMove = true;

    private Vector2 _lastPos;

    private double _time;

    public ViewWindow() : base(GameWindowSettings.Default, new NativeWindowSettings()
    {
        ClientSize = (800, 600),
        MinimumClientSize = (400, 300),
        Title = "World Of Goo 3D level view (Alpha)",
        APIVersion = new Version(4, 5),
        API = OpenTK.Windowing.Common.ContextAPI.OpenGL
    })
    {
        Console.WriteLine($"Window created! OpenGL: {APIVersion}");
        
    }
    private static DebugProc DebugMessageDelegate = OnDebugMessage;
    private static void OnDebugMessage(
    DebugSource source,     // Source of the debugging message.
    DebugType type,         // Type of the debugging message.
    int id,                 // ID associated with the message.
    DebugSeverity severity, // Severity of the message.
    int length,             // Length of the string in pMessage.
    IntPtr pMessage,        // Pointer to message string.
    IntPtr pUserParam)      // The pointer you gave to OpenGL, explained later.
    {
        // In order to access the string pointed to by pMessage, you can use Marshal
        // class to copy its contents to a C# string without unsafe code. You can
        // also use the new function Marshal.PtrToStringUTF8 since .NET Core 1.1.
        string message = Marshal.PtrToStringAnsi(pMessage, length);

        // The rest of the function is up to you to implement, however a debug output
        // is always useful.
        Console.WriteLine("[{0} source={1} type={2} id={3}] {4}", severity, source, type, id, message);

        // Potentially, you may want to throw from the function for certain severity
        // messages.
        if (type == DebugType.DebugTypeError)
        {
            throw new Exception(message);
        }
    }
}
