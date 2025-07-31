using Atmos2D.ECS;
using Atmos2D.Graphics;
using Atmos2D.Core.Managers; 

using static Raylib_cs.Raylib;
using Color = Raylib_cs.Color;
using KeyboardKey = Raylib_cs.KeyboardKey;

namespace Atmos2D.Core
{
    /// <summary>
    /// Main engine class, managing the game loop and initialization of subsystems.
    /// </summary>
    public abstract class Game : IDisposable // Implement IDisposable for proper cleanup
    {
        // Engine properties
        public int WindowWidth { get; protected set; }
        public int WindowHeight { get; protected set; }
        public string WindowTitle { get; protected set; }

        // Central managers
        protected EntityManager EntityManager { get; private set; }
        protected SystemManager SystemManager { get; private set; }
        protected GraphicsManager GraphicsManager { get; private set; } 

        private bool _isRunning;

        /// <summary>
        /// Base constructor for the game.
        /// </summary>
        /// <param name="width">Window width.</param>
        /// <param name="height">Window height.</param>
        /// <param name="title">Window title.</param>
        public Game(int width, int height, string title)
        {
            WindowWidth = width;
            WindowHeight = height;
            WindowTitle = title;

            // Initialize managers
            EntityManager = new EntityManager();
            SystemManager = new SystemManager(EntityManager);
            GraphicsManager = new GraphicsManager(); 
        }

        /// <summary>
        /// Initialization method called once at the start of the game.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Update method called every frame for game logic.
        /// </summary>
        /// <param name="deltaTime">Time elapsed since the last frame.</param>
        protected abstract void Update(float deltaTime);

        /// <summary>
        /// Draw method called every frame for rendering.
        /// </summary>
        protected abstract void Draw();

        /// <summary>
        /// Starts the main game loop.
        /// </summary>
        public void Run()
        {
            // Initialize the window (via WindowManager/Raylib)
            WindowManager.InitializeWindow(WindowWidth, WindowHeight, WindowTitle);
            Console.WriteLine($"[Atmos2D] Initializing window: {WindowTitle} ({WindowWidth}x{WindowHeight})");

            Initialize(); // Initialisation spécifique au jeu

            _isRunning = true;
            GameLoop();

            // Libération des ressources (via WindowManager/Raylib)
            WindowManager.CloseWindow();
            Console.WriteLine("[Atmos2D] Shutting down.");
        }

        /// <summary>
        /// Main game loop.
        /// </summary>
        private void GameLoop()
        {
            while (_isRunning && !WindowManager.WindowShouldClose())
            {
                float deltaTime = WindowManager.GetFrameTime(); // Get actual delta time from Raylib

                // Update game logic
                Update(deltaTime);
                SystemManager.Update(deltaTime); // Update all registered systems

                // Render
                WindowManager.BeginDrawing();
                WindowManager.ClearBackground(Color.BLACK); // Clear background to black
                Draw(); // Game-specific drawing
                SystemManager.Draw(); // Ask systems to draw
                WindowManager.EndDrawing();

                // Quit game logic (ex: ESC key or window close)
                if (WindowManager.IsKeyPressed(KeyboardKey.KEY_ESCAPE))
                {
                    _isRunning = false;
                }
            }
        }

        protected float GetFPS() => WindowManager.GetFPS();

        /// <summary>
        /// Stops the game loop.
        /// </summary>
        public void Stop()
        {
            _isRunning = false;
        }

        /// <summary>
        /// Disposes of managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // No resource management needed here
            }
        }
    }
}