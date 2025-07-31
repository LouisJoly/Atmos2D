using Atmos2D.Core.Components;
using Atmos2D.ECS;
using System;

namespace Atmos2D.GameExample.Components
{
    /// <summary>
    /// Marks an entity as the player and handles player-specific collision reactions.
    /// </summary>
    public class SnakeComponent : IComponent, ICollidable
    {
        public int Score { get; set; } = 0;
        public int Length { get; set; } = 1;
        public bool IsAlive { get; set; } = true;

        public SnakeComponent() { }

        /// <summary>
        /// Reacts to a collision with another entity.
        /// </summary>
        /// <param name="otherEntity">The entity that collided with the player.</param>
        public void OnCollisionEnter(Entity otherEntity)
        {
            // Get the CollisionComponent of the other entity to check its tag
            if (otherEntity.HasComponent<CollisionComponent>())
            {
                var otherCollision = otherEntity.GetComponent<CollisionComponent>();

                if (otherCollision.Tag == "Food")
                {
                    Score += 10;
                    Length += 1;
                }
                else if (otherCollision.Tag == "Wall" || otherCollision.Tag == "SnakeBody")
                {
                    IsAlive = false;
                    // Console.WriteLine($"[SnakeComponent] Snake hit a wall or self! Game Over!");
                }
            }
        }
    }
}