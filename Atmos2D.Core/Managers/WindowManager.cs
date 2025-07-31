using System.Numerics; 

using Raylib_cs;
using Color = Raylib_cs.Color;
using KeyboardKey = Raylib_cs.KeyboardKey;
using MouseButton = Raylib_cs.MouseButton;

namespace Atmos2D.Core.Managers
{
    /// <summary>
    /// Manages the creation, update, and closing of the game window using Raylib-cs.
    /// </summary>
    public static class WindowManager
    {
        /// <summary>
        /// Initializes the Raylib window with specified dimensions and title.
        /// </summary>
        /// <param name="width">The desired width of the window.</param>
        /// <param name="height">The desired height of the window.</param>
        /// <param name="title">The title text for the window.</param>
        public static void InitializeWindow(int width, int height, string title)
        {
            Raylib.InitWindow(width, height, title);
            Raylib.SetTargetFPS(60); // Optional: Set a target FPS for consistent game speed
            Console.WriteLine($"[WindowManager] Raylib window initialized: {title} ({width}x{height})");
        }

        /// <summary>
        /// Checks if the window should close (e.g., user clicked close button or pressed ESC).
        /// </summary>
        /// <returns>True if the window should close, otherwise false.</returns>
        public static bool WindowShouldClose()
        {
            return Raylib.WindowShouldClose();
        }

        /// <summary>
        /// Closes the Raylib window and unloads OpenGL context.
        /// </summary>
        public static void CloseWindow()
        {
            Raylib.CloseWindow();
            Console.WriteLine("[WindowManager] Raylib window closed.");
        }

        /// <summary>
        /// Begins the drawing phase. Must be called before any drawing commands.
        /// </summary>
        public static void BeginDrawing()
        {
            Raylib.BeginDrawing();
        }

        /// <summary>
        /// Ends the drawing phase and swaps buffers. Must be called after all drawing commands.
        /// </summary>
        public static void EndDrawing()
        {
            Raylib.EndDrawing();
        }

        /// <summary>
        /// Clears the background of the drawing area with a specified color.
        /// </summary>
        /// <param name="color">The color to clear the background with.</param>
        public static void ClearBackground(Color color)
        {
            Raylib.ClearBackground(color);
        }

        /// <summary>
        /// Gets the time elapsed since the last frame update.
        /// </summary>
        /// <returns>Delta time in seconds.</returns>
        public static float GetFrameTime()
        {
            return Raylib.GetFrameTime();
        }

        public static float GetFPS()
        {
            return Raylib.GetFPS();
        }

        /// <summary>
        /// Checks if a specific key is down.
        /// </summary>
        public static bool IsKeyDown(KeyboardKey key)
        {
            return Raylib.IsKeyDown(key);
        }

        /// <summary>
        /// Checks if a specific key was pressed in the current frame.
        /// </summary>
        public static bool IsKeyPressed(KeyboardKey key)
        {
            return Raylib.IsKeyPressed(key);
        }

        /// <summary>
        /// Checks if a specific mouse button is down.
        /// </summary>
        public static bool IsMouseButtonDown(MouseButton button)
        {
            return Raylib.IsMouseButtonDown(button);
        }

        /// <summary>
        /// Checks if a specific mouse button was pressed in the current frame.
        /// </summary>
        public static bool IsMouseButtonPressed(MouseButton button)
        {
            return Raylib.IsMouseButtonPressed(button);
        }

        /// <summary>
        /// Gets the current mouse X-coordinate.
        /// </summary>
        public static int GetMouseX()
        {
            return Raylib.GetMouseX();
        }

        /// <summary>
        /// Gets the current mouse Y-coordinate.
        /// </summary>
        public static int GetMouseY()
        {
            return Raylib.GetMouseY();
        }
    }
}