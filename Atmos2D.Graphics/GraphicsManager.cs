using System;
using System.Numerics; 

using Raylib_cs;
using Color = Raylib_cs.Color;

namespace Atmos2D.Graphics
{
    public class GraphicsManager
    {
        public GraphicsManager()
        {
            // Raylib initialization moved to WindowManager
        }

        // Add other drawing methods as needed (e.g., shapes, text)
        public void DrawRectangle(int x, int y, int width, int height, Color color)
        {
            Raylib.DrawRectangle(x, y, width, height, color);
        }
        public void DrawRectangle(float x, float y, float width, float height, Color color)
        {
            Raylib.DrawRectangle((int)x, (int)y, (int)width, (int)height, color);
        }

        public void DrawWireRectangle(int x, int y, int width, int height, Color color)
        {
            Raylib.DrawRectangleLines(x, y, width, height, color);
        }
        
        public void DrawWireRectangle(float x, float y, float width, float height, Color color)
        {
            Raylib.DrawRectangleLines((int)x, (int)y, (int)width, (int)height, color);
        }

        public void DrawText(string text, int x, int y, int fontSize, Color color)
        {
            Raylib.DrawText(text, x, y, fontSize, color);
        }
    }
}