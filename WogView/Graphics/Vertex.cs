

using OpenTK.Mathematics;

namespace WogView.Graphics;

struct Vertex{
    public Vector3 position;

    public Vector2 texCoord;
    public Vertex(float x, float y, float z, float u, float v){
        position = new Vector3(x, y, z);
        texCoord = new Vector2(u, v);
    }
}
