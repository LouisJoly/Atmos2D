using Atmos2D.Core;
using Atmos2D.ECS;
using Atmos2D.Core.Components; 
using Atmos2D.Core.Systems; 
using Atmos2D.GameExample.Components; 
using Atmos2D.GameExample.Systems;
using System;
using System.Linq; 
using System.Numerics;
using System.IO;

namespace Atmos2D.GameExample
{
    public class SnakeExample : Game
    {
        private const int GridSize = 32; // Define your grid size

        public SnakeExample(int width, int height, string title) : base(width, height, title) { }

        protected override void Initialize()
        {
            Console.WriteLine("[SnakeExample] Initializing game elements.");

            // Registering Systems
            SystemManager.RegisterSystem(new SnakeMovementSystem(EntityManager, GridSize, WindowWidth, WindowHeight));
            SystemManager.RegisterSystem(new SnakeInputSystem(EntityManager));
            SystemManager.RegisterSystem(new CollisionSystem(EntityManager));
            SystemManager.RegisterSystem(new FoodSpawnSystem(EntityManager, GridSize, WindowWidth, WindowHeight));
            SystemManager.RegisterSystem(new EntityCleanupSystem(EntityManager)); // IMPORTANT: This should run LAST or after systems that mark for removal

            // Create the snake head entity
            var snakeHead = EntityManager.CreateEntity();
            var snakeComponent = new SnakeComponent { Length = 3 };
            var transformComponent = new TransformComponent(new Vector2(GridSize * 5, GridSize * 5), scale: new Vector2(1, 1));
            snakeHead.AddComponent(snakeComponent);
            snakeHead.AddComponent(transformComponent);
            snakeHead.AddComponent(new SnakeInputComponent()); // To control direction
            snakeHead.AddComponent(new DirectionComponent(new Vector2(1, 0))); // Start moving right
            snakeHead.AddComponent(new CollisionComponent(new Vector2(GridSize, GridSize), "SnakeHead"));
            snakeHead.AddComponent(new SnakeHeadComponent());

            // Create initial snake body segments
            // These will be moved by SnakeMovementSystem
            for (int i = 1; i < snakeComponent.Length; i++)
            {
                var bodySegment = EntityManager.CreateEntity();
                var bodytransformComponent = new TransformComponent((transformComponent.Position - new Vector2(GridSize * i, 0)), scale: new Vector2(1, 1));
                bodySegment.AddComponent(bodytransformComponent);
                bodySegment.AddComponent(new CollisionComponent(new Vector2(GridSize, GridSize), "SnakeBody"));
                bodySegment.AddComponent(new SnakeBodySegmentComponent(i, transformComponent.Position)); // Store initial position
            }

            // Create a food entity
            var apple = EntityManager.CreateEntity();
            apple.AddComponent(new TransformComponent(new Vector2(GridSize * 10, GridSize * 10), scale: new Vector2(1, 1))); // Place at (10,10) grid cell
            apple.AddComponent(new CollisionComponent(new Vector2(GridSize, GridSize) * 0.8f, "Food", isTrigger: true));
            apple.AddComponent(new FoodComponent());

            EntityManager.ProcessPendingChanges(); // Ensure all entities are added before first update

            Console.WriteLine("[SnakeExample] Setup complete. Ready to run!");            
        }

        protected override void Update(float deltaTime)
        {
            // Handle player input to change snake direction
            var snakeHead = EntityManager.GetEntitiesWithComponent<SnakeHeadComponent>().FirstOrDefault();            

            if (snakeHead != null)
            {
                var input = snakeHead.GetComponent<SnakeInputComponent>();
                var direction = snakeHead.GetComponent<DirectionComponent>();

                if (input != null && direction != null)
                {
                    Vector2 newDirection = direction.Direction;
                    if ((input.WasActionJustPressed.GetValueOrDefault("MoveUp")||input.IsActionPressed.GetValueOrDefault("MoveUp") ) && direction.Direction.Y == 0)
                        newDirection = new Vector2(0, -1);
                    else if (input.WasActionJustPressed.GetValueOrDefault("MoveDown")||input.IsActionPressed.GetValueOrDefault("MoveDown") && direction.Direction.Y == 0)
                        newDirection = new Vector2(0, 1);
                    else if (input.WasActionJustPressed.GetValueOrDefault("MoveLeft")||input.IsActionPressed.GetValueOrDefault("MoveLeft") && direction.Direction.X == 0)
                        newDirection = new Vector2(-1, 0);
                    else if (input.WasActionJustPressed.GetValueOrDefault("MoveRight")||input.IsActionPressed.GetValueOrDefault("MoveRight") && direction.Direction.X == 0)
                        newDirection = new Vector2(1, 0);

                    // Prevent 180-degree turns
                    if (newDirection != -direction.PreviousDirection) // Check against previous direction to prevent immediate reverse
                    {
                        direction.Direction = newDirection;
                        direction.PreviousDirection = newDirection; // Update previous direction after a valid turn
                    }
                }
            }

            // Example: Draw score
            if (snakeHead != null)
            {
                GraphicsManager.DrawText($"Score: {snakeHead.GetComponent<SnakeComponent>().Score}", 10, 40, 20, Raylib_cs.Color.WHITE);
                GraphicsManager.DrawText($"Length: {snakeHead.GetComponent<SnakeComponent>().Length}", 10, 70, 20, Raylib_cs.Color.WHITE);
                if (!snakeHead.GetComponent<SnakeComponent>().IsAlive)
                {
                    GraphicsManager.DrawText("GAME OVER!", WindowWidth / 2 - 100, WindowHeight / 2 - 20, 40, Raylib_cs.Color.RED);
                }
            }
        }

        protected override void Draw()
        {
            GraphicsManager.DrawText("Atmos2D Snake Demo - Press ESC to exit", WindowWidth - 300, WindowHeight - 30, 15, Raylib_cs.Color.GRAY);
            GraphicsManager.DrawText($"FPS: {GetFPS()}", 10, 10, 20, Raylib_cs.Color.LIME);
            DrawDebugSnake();
            DrawDebugApple();
        }

        protected void DrawDebugSnake()
        {
            var snakeHead = EntityManager.GetEntitiesWithComponent<SnakeHeadComponent>().FirstOrDefault();
            if (snakeHead != null)
            {
                var transform = snakeHead.GetComponent<TransformComponent>();
                var collider = snakeHead.GetComponent<CollisionComponent>();

                if (transform != null)
                {
                    GraphicsManager.DrawWireRectangle(transform.Position.X, transform.Position.Y, collider.Size.X, collider.Size.Y, Raylib_cs.Color.RED);
                }
            }
            var snakeBody = EntityManager.GetEntitiesWithComponent<SnakeBodySegmentComponent>();
            foreach (var bodySegment in snakeBody)
            {
                var transform = bodySegment.GetComponent<TransformComponent>();
                var collider = bodySegment.GetComponent<CollisionComponent>();
                if (transform != null)
                {
                    GraphicsManager.DrawWireRectangle(transform.Position.X, transform.Position.Y, collider.Size.X, collider.Size.Y, Raylib_cs.Color.GREEN);
                }
            }
        }

        protected void DrawDebugApple()
        {
            var apple = EntityManager.GetEntitiesWithComponent<FoodComponent>().FirstOrDefault();
            if (apple != null)
            {
                var transform = apple.GetComponent<TransformComponent>();
                var collider = apple.GetComponent<CollisionComponent>();
                if (transform != null)
                {
                    GraphicsManager.DrawWireRectangle(transform.Position.X, transform.Position.Y, collider.Size.X, collider.Size.Y, Raylib_cs.Color.YELLOW);
                }
            }
        }
    }
}