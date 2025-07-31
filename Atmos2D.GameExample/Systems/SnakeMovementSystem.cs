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
    /// Manages the grid-based movement of the snake, including its head and body segments.
    /// Handles segment following, snake growth, and controls movement speed using a timer.
    /// </summary>
    public class SnakeMovementSystem : ISystem
    {
        private readonly EntityManager _entityManager;
        private readonly int _gridSize; // Size of each grid cell (e.g., 32 pixels)
        private readonly int _worldWidth; // World width in pixels
        private readonly int _worldHeight; // World height in pixels

        /// <summary>
        /// Initializes a new instance of the SnakeMovementSystem.
        /// </summary>
        /// <param name="entityManager">The central EntityManager instance.</param>
        /// <param name="gridSize">The size of one grid cell in pixels.</param>
        public SnakeMovementSystem(EntityManager entityManager, int gridSize, int worldWidth, int worldHeight)
        {
            _entityManager = entityManager ?? throw new ArgumentNullException(nameof(entityManager));
            _gridSize = gridSize;
            _worldWidth = worldWidth;
            _worldHeight = worldHeight;
        }

        /// <summary>
        /// Updates the position of the snake's head and makes body segments follow.
        /// Handles snake growth when food is eaten, and controls movement based on a timer.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        public void Update(float deltaTime)
        {
            // Find the snake head
            var snakeHeadEntity = _entityManager.GetEntitiesWithComponent<SnakeHeadComponent>()
                                                .FirstOrDefault();

            if (snakeHeadEntity == null) return;

            var headTransform = snakeHeadEntity.GetComponent<TransformComponent>();
            var headDirection = snakeHeadEntity.GetComponent<DirectionComponent>();
            var snakeHeadComponent = snakeHeadEntity.GetComponent<SnakeHeadComponent>(); // Get the SnakeHeadComponent itself
            var snakeComponent = snakeHeadEntity.GetComponent<SnakeComponent>(); // Get the SnakeHeadComponent itself

            if (headTransform == null || headDirection == null || snakeHeadComponent == null || snakeComponent == null)
            {
                Console.WriteLine("[SnakeMovementSystem] Snake head missing required components.");
                return;
            }

            // If the snake is not alive, stop movement.
            if (!snakeComponent.IsAlive)
            {
                // Optionally reset timer or indicate game over state
                return;
            }

            // --- Control Movement with Timer ---
            snakeHeadComponent.MoveTimer += deltaTime;

            if (snakeHeadComponent.MoveTimer < snakeHeadComponent.MoveInterval)
            {
                return; // Not enough time has passed for the next move
            }

            // Time to move! Reset the timer.
            // Subtracting MoveInterval ensures that any excess deltaTime is carried over
            // for more precise timing.
            snakeHeadComponent.MoveTimer -= snakeHeadComponent.MoveInterval;

            // --- Store Head's Current Position Before Moving ---
            Vector2 previousHeadPosition = headTransform.Position;

            // --- Move the Snake Head ---
            headTransform.Position += headDirection.Direction * _gridSize;


            // --- Move the Snake Body Segments ---
            // Get all body segments, ordered correctly
            var bodySegments = _entityManager.GetEntitiesWithComponent<SnakeBodySegmentComponent>()
                                             .OrderBy(e => e.GetComponent<SnakeBodySegmentComponent>()?.Order)
                                             .ToList();

            Vector2 positionToFollow = previousHeadPosition; // The first segment follows the head's previous position

            foreach (var segmentEntity in bodySegments)
            {
                var segmentTransform = segmentEntity.GetComponent<TransformComponent>();
                var segmentComponent = segmentEntity.GetComponent<SnakeBodySegmentComponent>();

                if (segmentTransform == null || segmentComponent == null) continue;

                // Store current segment's position before moving it, this will be the "positionToFollow" for the next segment
                Vector2 tempCurrentSegmentPosition = segmentTransform.Position;

                // Move this segment to the position the previous segment (or head) was in
                segmentTransform.Position = positionToFollow;

                // Update 'positionToFollow' for the next segment in the chain
                positionToFollow = tempCurrentSegmentPosition;
            }

            // --- Handle Snake Growth ---
            // If the snake component indicates the snake needs to grow, add a new segment.
            // This is typically triggered by eating food.
            // SnakeComponent.Length includes the head.
            if (bodySegments.Count < snakeComponent.Length - 1)
            {
                // The new segment is added at the position of the *last* segment (or head if no segments)
                // before it moved.
                Vector2 newSegmentPosition = bodySegments.LastOrDefault()?.GetComponent<TransformComponent>()?.Position ?? previousHeadPosition;

                var newSegment = _entityManager.CreateEntity();
                newSegment.AddComponent(new TransformComponent(newSegmentPosition));
                newSegment.AddComponent(new CollisionComponent(new Vector2(_gridSize, _gridSize), "SnakeBody"));
                newSegment.AddComponent(new SnakeBodySegmentComponent(snakeComponent.Length - 1, newSegmentPosition)); // Assign order and previous position

                _entityManager.ProcessPendingChanges(); // Ensure new entity is added immediately
                Console.WriteLine($"[SnakeMovementSystem] Snake grew! New length: {snakeComponent.Length}");
            }

            // --- Handle Wrap-Around (Optional, or handled by CollisionSystem with walls) ---
            // Example wrap-around for head
            if (headTransform.Position.X < 0) headTransform.Position = new Vector2(_worldWidth - _gridSize, headTransform.Position.Y);
            if (headTransform.Position.X >= _worldWidth) headTransform.Position = new Vector2(0, headTransform.Position.Y);
            if (headTransform.Position.Y < 0) headTransform.Position = new Vector2(headTransform.Position.X, _worldHeight - _gridSize);
            if (headTransform.Position.Y >= _worldHeight) headTransform.Position = new Vector2(headTransform.Position.X, 0);
        }

        public void Draw() { /* No drawing operations for movement systems */ }
    }
}