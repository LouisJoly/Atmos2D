using Atmos2D.ECS;
using Atmos2D.Core.Components;
using Atmos2D.GameExample.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Atmos2D.GameExample.Systems
{
    /// <summary>
    /// Manages the spawning of food items in the game world.
    /// It detects when existing food is eaten and creates new food at unoccupied grid positions.
    /// </summary>
    public class FoodSpawnSystem : ISystem
    {
        private readonly EntityManager _entityManager;
        private readonly int _gridSize;
        private readonly int _playAreaWidth;
        private readonly int _playAreaHeight;
        private readonly Random _random;

        /// <summary>
        /// Initializes a new instance of the FoodSpawnSystem.
        /// </summary>
        /// <param name="entityManager">The central EntityManager instance.</param>
        /// <param name="gridSize">The size of one grid cell in pixels.</param>
        /// <param name="playAreaWidth">The width of the playable area in pixels.</param>
        /// <param name="playAreaHeight">The height of the playable area in pixels.</param>
        public FoodSpawnSystem(EntityManager entityManager, int gridSize, int playAreaWidth, int playAreaHeight)
        {
            _entityManager = entityManager ?? throw new ArgumentNullException(nameof(entityManager));
            _gridSize = gridSize;
            _playAreaWidth = playAreaWidth;
            _playAreaHeight = playAreaHeight;
            _random = new Random();
        }

        /// <summary>
        /// Checks for eaten food and spawns new food if necessary.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        public void Update(float deltaTime)
        {
            var eatenFood = _entityManager.GetEntitiesWithComponent<FoodComponent>()
                                          .Where(e => e.GetComponent<FoodComponent>().IsEaten)
                                          .FirstOrDefault();

            if (eatenFood != null)
            {
                // Mark the eaten food for removal
                if (!eatenFood.HasComponent<RemovableComponent>())
                {
                    eatenFood.AddComponent(new RemovableComponent());
                }

                Console.WriteLine("[FoodSpawnSystem] Old food eaten, spawning new food...");
                SpawnNewFood();
            }
        }

        /// <summary>
        /// Spawns a new food entity at a random, unoccupied grid position.
        /// </summary>
        private void SpawnNewFood()
        {
            var occupiedPositions = new HashSet<Vector2>();

            // Get all snake segments' positions
            var snakeEntities = _entityManager.GetEntitiesWithComponent<SnakeHeadComponent>()
                                              .Concat(_entityManager.GetEntitiesWithComponent<SnakeBodySegmentComponent>())
                                              .ToList();

            foreach (var snakePart in snakeEntities)
            {
                var transform = snakePart.GetComponent<TransformComponent>();
                if (transform != null)
                {
                    // Snap to grid for checking
                    occupiedPositions.Add(new Vector2(
                        (float)Math.Round(transform.Position.X / _gridSize) * _gridSize,
                        (float)Math.Round(transform.Position.Y / _gridSize) * _gridSize
                    ));
                }
            }

            // Find all possible grid positions
            var possiblePositions = new List<Vector2>();
            for (int x = 0; x < _playAreaWidth / _gridSize; x++)
            {
                for (int y = 0; y < _playAreaHeight / _gridSize; y++)
                {
                    possiblePositions.Add(new Vector2(x * _gridSize, y * _gridSize));
                }
            }

            // Filter out occupied positions
            var availablePositions = possiblePositions.Except(occupiedPositions).ToList();

            if (availablePositions.Any())
            {
                // Choose a random available position
                Vector2 newFoodPosition = availablePositions[_random.Next(availablePositions.Count)];

                // Create new food entity
                var newFood = _entityManager.CreateEntity();
                newFood.AddComponent(new TransformComponent(newFoodPosition, scale: new Vector2(0.8f, 0.8f)));
                newFood.AddComponent(new CollisionComponent(new Vector2(_gridSize * 0.8f, _gridSize * 0.8f), "Food", isTrigger: true));
                newFood.AddComponent(new FoodComponent()); // New food is not yet eaten

                _entityManager.ProcessPendingChanges(); // Add new food entity immediately
                Console.WriteLine($"[FoodSpawnSystem] New food spawned at: {newFoodPosition}");
            }
            else
            {
                Console.WriteLine("[FoodSpawnSystem] No available positions to spawn food! (Game might be full)");
            }
        }

        /// <summary>
        /// Food spawning systems do not perform any drawing.
        /// </summary>
        public void Draw() { /* No drawing operations */ }
    }
}