using OpenTK.Graphics.ES11;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;


namespace WogView;

public partial class ViewWindow {
    
    protected override void OnUpdateFrame(FrameEventArgs e) {
        base.OnUpdateFrame(e);

            if (!IsFocused) // Check to see if the window is focused
            {
                return;
            }

            var input = KeyboardState;

            if (input.IsKeyPressed(Keys.Escape))
            {
                if(CursorState == CursorState.Grabbed){
                    CursorState = CursorState.Normal;
                }else{
                    CursorState = CursorState.Grabbed;
                }
            }
            
            const float sensitivity = 0.1f;
            if (input.IsKeyDown(Keys.W))
            {
                Graphics.Camera.Position += Graphics.Camera.Front * Config.CAM_SPEED * (float)e.Time; // Forward
            }
            if (input.IsKeyDown(Keys.S))
            {
                Graphics.Camera.Position -= Graphics.Camera.Front * Config.CAM_SPEED * (float)e.Time; // Backwards
            }
            if (input.IsKeyDown(Keys.A))
            {
                Graphics.Camera.Position -= Graphics.Camera.Right * Config.CAM_SPEED * (float)e.Time; // Left
            }
            if (input.IsKeyDown(Keys.D))
            {
                Graphics.Camera.Position += Graphics.Camera.Right * Config.CAM_SPEED * (float)e.Time; // Right
            }
            if (input.IsKeyDown(Keys.Space))
            {
                Graphics.Camera.Position += Graphics.Camera.Up * Config.CAM_SPEED * (float)e.Time; // Up
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                Graphics.Camera.Position -= Graphics.Camera.Up * Config.CAM_SPEED * (float)e.Time; // Down
            }

            if(CursorState != CursorState.Grabbed)
                return;

            // Get the mouse state
            var mouse = MouseState;

            if (_firstMove) // This bool variable is initially set to true.
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                // Calculate the offset of the mouse position
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);

                // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                Graphics.Camera.Yaw += deltaX * sensitivity;
                Graphics.Camera.Pitch -= deltaY * sensitivity; // Reversed since y-coordinates range from bottom to top
            }
    }
}