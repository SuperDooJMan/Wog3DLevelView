using System.Runtime.InteropServices;
using OpenTK.Mathematics;

namespace WogView.Graphics;

[StructLayout(LayoutKind.Explicit)]
struct Vertex{
    [FieldOffset(0)]
    public Vector3 position;
    [FieldOffset(0)]
    public Vector2 texCoord;
}
